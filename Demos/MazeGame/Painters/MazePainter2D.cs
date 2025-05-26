// <copyright file="MazePainter2D.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters
{
    using System.Text;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.CharacterProviders;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Paints a two-dimensional representation of a maze to the console window.
    /// </summary>
    public class MazePainter2D(
        IConsole console,
        IBorderPainter borderPainter,
        IWallCharacterProvider wallCharacterProvider,
        IPlayerCharacterProvider playerCharacterProvider)
        : Painter(console, borderPainter),
        IMazePainter
    {
        private readonly char pathChar = ' ';
        private readonly char fogChar = '░';

        /// <inheritdoc/>
        public void Paint(Maze maze, Player player)
        {
            this.PaintMaze(maze);
            this.PaintPlayer(player);
        }

        /// <inheritdoc/>
        public void ErasePlayer(Player player)
        {
            this.WriteToScreenBuffer(player.Position.X, player.Position.Y, this.pathChar, ConsoleOutputType.Default);
            this.Paint();
        }

        /// <inheritdoc/>
        public void PaintPlayer(Player player)
        {
            var playerChar = playerCharacterProvider.GetPlayerChar(player);
            this.WriteToScreenBuffer(player.Position.X, player.Position.Y, playerChar, ConsoleOutputType.Prompt);
            this.Paint();
        }

        private void PaintMaze(Maze maze)
        {
            for (var mazeY = 0; mazeY < maze.Height; mazeY++)
            {
                for (var mazeX = 0; mazeX < maze.Width; mazeX++)
                {
                    var mazePoint = maze.GetMazePoint(mazeX, mazeY);
                    if (mazePoint.Explored)
                    {
                        switch (mazePoint.PointType)
                        {
                            case MazePointType.Path:
                                this.WriteToScreenBuffer(mazeX, mazeY, this.pathChar, ConsoleOutputType.Default);
                                break;
                            case MazePointType.Wall:
                                this.WriteToScreenBuffer(
                                    mazeX,
                                    mazeY,
                                    wallCharacterProvider.GetWallChar(maze, new ConsolePoint(mazeX, mazeY)),
                                    ConsoleOutputType.Default);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        this.WriteToScreenBuffer(mazeX, mazeY, this.fogChar, ConsoleOutputType.Default);
                    }
                }

                this.Paint();
            }
        }
    }
}
