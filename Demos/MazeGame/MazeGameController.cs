// <copyright file="MazeGameController.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame
{
    using System.Diagnostics.CodeAnalysis;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.KeyPressHandlers;
    using Sde.MazeGame.Models;
    using Sde.MazeGame.Painters.Map;
    using Sde.MazeGame.Painters.Pov;
    using Sde.MazeGame.Painters.Status;

    /// <summary>
    /// The controlling module for the maze game.
    /// </summary>
    public class MazeGameController(
        IConsole console,
        IStatusPainter statusPainter,
        IMazePainterMap mazePainterMap,
        IMazePainterPov mazePainterPov,
        MazeVisibilityUpdater mazeVisibilityUpdater,
        MazeGameKeyPressMappings keyPressMappings,
        IMazeGameRandomiser randomiser)
        : IMazeGameController
    {
        /// <summary>
        /// Gets the instance of the game which is currently being played.
        /// </summary>
        public Game? CurrentGame { get; private set; }

        /// <inheritdoc/>
        public void Initialise(MazeGameOptions options)
        {
            console.Clear();

            var maze = new MazeFactory().CreateFromFile(options.MazeDataFile);
            var player = new Player
            {
                FacingDirection = randomiser.GetDirection(),
                Position = randomiser.GetPosition(maze),
            };
            this.CurrentGame = new Game(maze, player) { Status = MazeGameStatus.NotStarted };

            mazePainterMap.Origin = options.MapViewOrigin;
            mazePainterMap.InnerSize = new ConsoleSize(maze.Width, maze.Height);
            mazePainterMap.HasBorder = true;

            mazePainterPov.Origin = options.PovViewOrigin;
            mazePainterPov.HasBorder = true;

            statusPainter.Origin = options.StatusOrigin;
            statusPainter.InnerSize = new ConsoleSize(console.WindowWidth - options.StatusOrigin.X - 2, 1);
            statusPainter.HasBorder = true;

            this.UpdateVisibility(maze, player);
            mazePainterMap.Paint(maze, player);
            mazePainterPov.Render(maze, player);
            this.WritePositionStatusMessage(player);
            console.CursorVisible = false;
        }

        /// <inheritdoc/>
        public void Play()
        {
            this.ThrowIfNotInitialised();
            this.CurrentGame.Status = MazeGameStatus.InProgress;
            while (this.CurrentGame.Status == MazeGameStatus.InProgress)
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

            console.CursorTop = this.CurrentGame.Maze.Height + 1;
            console.CursorLeft = 0;
            console.CursorVisible = true;
        }

        /// <inheritdoc/>
        public void TurnPlayerLeft()
        {
            this.ThrowIfNotInitialised();
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
            this.ThrowIfNotInitialised();
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
            this.ThrowIfNotInitialised();
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
            //if (!maze.PositionIsWithinMaze(newPosition))
            if (maze.GetMazePoint(newPosition).PointType == MazePointType.Exit)
            {
                var msg = "Congratulations, you have escaped the maze! Press space to continue.";
                statusPainter.Paint(msg, ConsoleOutputType.Prompt);
                this.CurrentGame.Status = MazeGameStatus.Won;
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
            this.ThrowIfNotInitialised();
            console.CursorTop = this.CurrentGame.Maze.Height + 1;
            statusPainter.Paint("Thank you for playing!");
            this.CurrentGame.Status = MazeGameStatus.Lost;
            mazePainterMap.Reset();
            mazePainterPov.Reset();
            statusPainter.Reset();
        }

        [MemberNotNull(nameof(CurrentGame))]
        private void ThrowIfNotInitialised()
        {
            if (this.CurrentGame == null)
            {
                throw new InvalidOperationException("The game has not been initialised. Call Initialise() first.");
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
