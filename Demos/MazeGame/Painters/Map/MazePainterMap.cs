// <copyright file="MazePainterMap.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Map
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.FullScreen;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.CharacterProviders;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Paints a map representation of a maze to the console window.
    /// </summary>
    public class MazePainterMap(
        IConsole console,
        IBorderPainter borderPainter,
        IWallCharacterProvider wallCharacterProvider,
        IPlayerCharacterProvider playerCharacterProvider)
        : Painter(console, borderPainter),
        IMazePainterMap
    {
        private readonly char pathChar = ' ';
        private readonly char fogChar = '░';

        /// <inheritdoc/>
        public void Paint(Maze maze, Player player)
        {
            PaintMaze(maze);
            PaintPlayer(player);
        }

        /// <inheritdoc/>
        public void ErasePlayer(Player player)
        {
            WriteToScreenBuffer(player.Position.X, player.Position.Y, pathChar, ConsoleOutputType.Default);
            Paint();
        }

        /// <inheritdoc/>
        public void PaintPlayer(Player player)
        {
            var playerChar = playerCharacterProvider.GetPlayerChar(player);
            WriteToScreenBuffer(player.Position.X, player.Position.Y, playerChar, ConsoleOutputType.Prompt);
            Paint();
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
                                WriteToScreenBuffer(mazeX, mazeY, pathChar, ConsoleOutputType.Default);
                                break;
                            case MazePointType.Wall:
                                WriteToScreenBuffer(
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
                        WriteToScreenBuffer(mazeX, mazeY, fogChar, ConsoleOutputType.Default);
                    }
                }

                Paint();
            }
        }
    }
}
