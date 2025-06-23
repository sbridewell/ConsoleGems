// <copyright file="GameController.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.FogOfWar;
    using Sde.MazeGame.KeyPressHandlers;
    using Sde.MazeGame.Models;
    using Sde.MazeGame.Painters;
    using Sde.MazeGame.Painters.Pov;
    using Sde.MazeGame.Painters.Status;

    /// <summary>
    /// The controlling module for the maze game.
    /// </summary>
    public class GameController(
        IConsole console,
        IStatusPainter statusPainter,
        IMazePainter mazePainterMap,
        IMazePainterPov mazePainterPov,
        MazeVisibilityUpdater mazeVisibilityUpdater,
        MazeGameKeyPressMappings keyPressMappings)
        : IGameController
    {
        /// <summary>
        /// Gets the instance of the game which is currently being played.
        /// </summary>
        public Game? CurrentGame { get; private set; }

        /// <inheritdoc/>
        public void Play(string mazeFile)
        {
            console.Clear();
            var maze = new MazeFactory().CreateFromFile(mazeFile);
            var player = new Player
            {
                FacingDirection = GetPlayersStartingDirection(),
                Position = this.GetPlayersStartingPosition(maze),
            };
            this.CurrentGame = new Game(maze, player);
            var options = new MazeGameOptions()
                .WithMazeOrigin(41, 3)
                .WithStatusOrigin(0, 0);

            mazePainterMap.Origin = options.MazeOrigin;
            mazePainterMap.InnerSize = new ConsoleSize(maze.Width, maze.Height);
            mazePainterMap.HasBorder = true;

            mazePainterPov.Origin = new ConsolePoint(0, 3);
            mazePainterPov.HasBorder = true;

            statusPainter.Origin = options.StatusOrigin;
            statusPainter.InnerSize = new ConsoleSize(console.WindowWidth - options.StatusOrigin.X - 2, 1);
            statusPainter.HasBorder = true;

            this.UpdateVisibility(maze, player);
            mazePainterMap.Paint(maze, player);
            mazePainterPov.Render(maze, player);
            this.WritePositionStatusMessage(player);
            console.CursorVisible = false;
            while (this.CurrentGame.ContinuePlaying && !this.CurrentGame.PlayerHasWon)
            {
                var keyInfo = console.ReadKey(intercept: true);
                keyPressMappings.Mappings.TryGetValue(keyInfo.Key, out var keyPressHandler);
                if (keyPressHandler != null)
                {
                    keyPressHandler.Handle(keyInfo, this);
                }
                else
                {
                    var msg = "Use left and right arrows to turn, up arrow to move forward and Q to quit.";
                    statusPainter.Paint(msg, ConsoleOutputType.Error);
                }
            }

            console.CursorTop = maze.Height + 1;
            console.CursorLeft = 0;
            console.CursorVisible = true;
        }

        /// <inheritdoc/>
        public void TurnPlayerLeft()
        {
            // TODO: PlayerManager.TurnLeft and TurnRight methods?
            if (this.CurrentGame == null)
            {
                // TOOD: ThrowIfGameNotStarted method?
                throw new InvalidOperationException("The game has not been started.");
            }

            var maze = this.CurrentGame.Maze;
            var player = this.CurrentGame.Player;
            player.FacingDirection = player.FacingDirection switch
            {
                Direction.North => Direction.West,
                Direction.East => Direction.North,
                Direction.South => Direction.East,
                Direction.West => Direction.South,
                _ => player.FacingDirection,
            };

            this.UpdateVisibility(maze, player);
            this.WritePositionStatusMessage(player);
            mazePainterPov.Render(maze, player);
        }

        /// <inheritdoc/>
        public void TurnPlayerRight()
        {
            if (this.CurrentGame == null)
            {
                throw new InvalidOperationException("The game has not been started.");
            }

            var maze = this.CurrentGame.Maze;
            var player = this.CurrentGame.Player;
            player.FacingDirection = player.FacingDirection switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => player.FacingDirection,
            };

            this.UpdateVisibility(maze, player);
            this.WritePositionStatusMessage(player);
            mazePainterPov.Render(maze, player);
        }

        /// <inheritdoc/>
        public void TryToMovePlayerForward()
        {
            if (this.CurrentGame == null)
            {
                throw new InvalidOperationException("The game has not been started.");
            }

            var player = this.CurrentGame.Player;
            var maze = this.CurrentGame.Maze;
            var newPosition = player.FacingDirection switch
            {
                Direction.North => new ConsolePoint(player.Position.X, player.Position.Y - 1),
                Direction.East => new ConsolePoint(player.Position.X + 1, player.Position.Y),
                Direction.South => new ConsolePoint(player.Position.X, player.Position.Y + 1),
                Direction.West => new ConsolePoint(player.Position.X - 1, player.Position.Y),
                _ => player.Position,
            };
            if (!maze.PositionIsWithinMaze(newPosition))
            {
                var msg = "Congratulations, you have escaped the maze! Press space to continue.";
                statusPainter.Paint(msg, ConsoleOutputType.Prompt);
                this.CurrentGame.PlayerHasWon = true;
                char spaceKey = '\0';
                mazePainterMap.Reset();
                mazePainterPov.Reset();
                statusPainter.Reset();
                while (spaceKey != ' ')
                {
                    spaceKey = console.ReadKey().KeyChar;
                }

                return;
            }

            if (maze.GetMazePoint(newPosition).PointType == MazePointType.Path)
            {
                mazePainterMap.ErasePlayer(player);
                player.Position = newPosition;
                this.UpdateVisibility(maze, player);
                this.WritePositionStatusMessage(player);
                mazePainterPov.Render(maze, player);
            }
            else
            {
                var msg = "You can't move forward, there's a wall in the way.";
                statusPainter.Paint(msg, ConsoleOutputType.Error);
            }
        }

        /// <inheritdoc/>
        public void Quit()
        {
            if (this.CurrentGame == null)
            {
                throw new InvalidOperationException("The game has not been started.");
            }

            console.CursorTop = this.CurrentGame.Maze.Height + 1;
            statusPainter.Paint("Thank you for playing!");
            this.CurrentGame.ContinuePlaying = false;
            mazePainterMap.Reset();
            mazePainterPov.Reset();
            statusPainter.Reset();
        }

        private static Direction GetPlayersStartingDirection()
        {
            var random = new Random();
            var direction = (Direction)random.Next(0, 4);
            return direction;
        }

        private ConsolePoint GetPlayersStartingPosition(Maze maze)
        {
            var x = new Random().Next(0, maze.Width);
            var y = new Random().Next(0, maze.Height);
            if (maze.GetMazePoint(x, y).PointType == MazePointType.Path)
            {
                return new ConsolePoint(x, y);
            }
            else
            {
                return this.GetPlayersStartingPosition(maze);
            }
        }

        private void WritePositionStatusMessage(Player player)
        {
            var facingString = player.FacingDirection.ToString();
            statusPainter.Paint($"Find your way out of the maze! You are facing {facingString}");
        }

        private void UpdateVisibility(Maze maze, Player player)
        {
            var updateRequired = mazeVisibilityUpdater.UpdateVisibility(maze, player);
            if (updateRequired)
            {
                mazePainterMap.Paint(maze, player);
            }
            else
            {
                mazePainterMap.ErasePlayer(player);
                mazePainterMap.PaintPlayer(player);
            }
        }
    }
}
