// <copyright file="MazePainter2D.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters
{
    using System.Text;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.CharacterProviders;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Paints a two-dimensional representation of a maze to the console window.
    /// </summary>
    public class MazePainter2D(
        IConsole console,
        IWallCharacterProvider wallCharacterProvider,
        IPlayerCharacterProvider playerCharacterProvider)
        : IMazePainter
    {
        private readonly char pathChar = ' ';
        private readonly char fogChar = '░';
        private ConsolePoint origin;

        /// <inheritdoc/>
        public void SetOrigin(ConsolePoint origin)
        {
            this.origin = origin;
        }

        /// <inheritdoc/>
        public void Paint(Maze maze, Player player)
        {
            PaintMaze(maze);
            PaintPlayer(player);
            console.CursorLeft = 0;
            console.CursorTop = console.WindowHeight - 1;
        }

        /// <inheritdoc/>
        public void ErasePlayer(Player player)
        {
            console.CursorLeft = origin.X + player.Position.X;
            console.CursorTop = origin.Y + player.Position.Y;
            console.Write(pathChar);
        }

        /// <inheritdoc/>
        public void PaintPlayer(Player player)
        {
            console.CursorLeft = origin.X + player.Position.X;
            console.CursorTop = origin.Y + player.Position.Y;
            var playerChar = playerCharacterProvider.GetPlayerChar(player);
            console.Write(playerChar, ConsoleOutputType.Prompt);
        }

        private void PaintMaze(Maze maze)
        {
            for (var mazeY = 0; mazeY < maze.Height; mazeY++)
            {
                console.CursorLeft = origin.X;
                console.CursorTop = origin.Y + mazeY;
                var sb = new StringBuilder();
                for (var mazeX = 0; mazeX < maze.Width; mazeX++)
                {
                    var mazePoint = maze.GetMazePoint(mazeX, mazeY);
                    if (mazePoint.Explored)
                    {
                        switch (mazePoint.PointType)
                        {
                            case MazePointType.Path:
                                sb.Append(pathChar);
                                break;
                            case MazePointType.Wall:
                                sb.Append(wallCharacterProvider.GetWallChar(maze, new ConsolePoint(mazeX, mazeY)));
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        sb.Append(fogChar);
                    }
                }

                console.Write(sb.ToString());
            }
        }
    }
}
