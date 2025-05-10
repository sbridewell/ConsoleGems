// <copyright file="MazePainter3D.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.FlattenedPerspective
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;
    using System.Text;

    public class MazePainter3D(IConsole console) : IMazePainter3D
    {
        private readonly int screenWidth = 24;
        private readonly int screenHeight = 24;
        private readonly char perpendicularWallChar = '━';
        private readonly char parallelWallChar = '▓';
        private readonly char nothingChar = '░';
        private ConsolePoint origin = default;

        /// <inheritdoc/>
        public void SetOrigin(ConsolePoint origin)
        {
            this.origin = origin;
        }

        /// <inheritdoc/>
        public void Render(Maze maze, Player player)
        {
            var screenBuffer = new char[this.screenWidth, this.screenHeight];
            var forwardView = this.GetForwardView(maze, player);
            //this.PaintSection0Left(maze, player, screenBuffer);
            this.PaintSection0(forwardView, screenBuffer);
            for (var screenY = 0; screenY < screenBuffer.GetLength(0); screenY++)
            {
                var render = new StringBuilder();
                for (var screenX = 0; screenX < screenBuffer.GetLength(1); screenX++)
                {
                    render.Append(screenBuffer[screenX, screenY]);
                }

                console.CursorLeft = this.origin.X;
                console.CursorTop = this.origin.Y;
                console.Write(render.ToString());
            }
        }

        // TODO: ForwardViewProvider?
        private MazePointType[,] GetForwardView(Maze maze, Player player)
        {
            var visibleDistance = 0;
            var leftWall = new List<MazePointType>();
            var forward = new List<MazePointType>();
            var rightWall = new List<MazePointType>();
            var mazeX = player.Position.X;
            var mazeY = player.Position.Y;
            while (maze.GetMazePoint(mazeX, mazeY).PointType != MazePointType.Wall)
            {
                visibleDistance++;
                switch (player.FacingDirection)
                {
                    case Direction.North:
                        leftWall.Add(maze.GetMazePoint(mazeX - 1, mazeY).PointType);
                        rightWall.Add(maze.GetMazePoint(mazeX + 1, mazeY).PointType);
                        mazeY--;
                        forward.Add(maze.GetMazePoint(mazeX, mazeY).PointType);
                        break;

                    case Direction.East:
                        leftWall.Add(maze.GetMazePoint(mazeX, mazeY - 1).PointType);
                        rightWall.Add(maze.GetMazePoint(mazeX, mazeY + 1).PointType);
                        mazeX++;
                        forward.Add(maze.GetMazePoint(mazeX, mazeY).PointType);
                        break;

                    case Direction.South:
                        leftWall.Add(maze.GetMazePoint(mazeX + 1, mazeY).PointType);
                        rightWall.Add(maze.GetMazePoint(mazeX - 1, mazeY).PointType);
                        mazeY++;
                        forward.Add(maze.GetMazePoint(mazeX, mazeY).PointType);
                        break;

                    case Direction.West:
                        leftWall.Add(maze.GetMazePoint(mazeX, mazeY + 1).PointType);
                        rightWall.Add(maze.GetMazePoint(mazeX, mazeY - 1).PointType);
                        mazeX--;
                        forward.Add(maze.GetMazePoint(mazeX, mazeY).PointType);
                        break;
                }
            }

            var forwardView = new MazePointType[visibleDistance, 3];
            for (var i = 0; i < visibleDistance; i++)
            {
                forwardView[i, 0] = leftWall[i];
                forwardView[i, 1] = forward[i];
                forwardView[i, 2] = rightWall[i];
            }

            return forwardView;
        }

        private void PaintSection0(MazePointType[,] forwardView, char[,] screenBuffer)
        {
            var screenX = 0;
            if (forwardView[1, 2] == MazePointType.Wall)
            {
                for (var screenY = 0; screenY < 24; screenY++)
                {
                    screenBuffer[screenX, screenY] = this.parallelWallChar;
                }
            }
            else
            {
                screenBuffer[screenX, 0] = this.nothingChar;
                screenBuffer[screenX, 23] = this.nothingChar;
                for (var screenY = 1; screenY < 23; screenY++)
                {
                    screenBuffer[screenX, screenY] = this.perpendicularWallChar;
                }
            }

            // TODO: looks like some duplication here
            screenX = 23;
            if (forwardView[1, 0] == MazePointType.Wall)
            {
                for (var screenY = 0; screenY < 24; screenY++)
                {
                    screenBuffer[screenX, screenY] = this.parallelWallChar;
                }
            }
            else
            {
                screenBuffer[screenX, 0] = this.nothingChar;
                screenBuffer[screenX, 23] = this.nothingChar;
                for (var screenY = 1; screenY < 23; screenY++)
                {
                    screenBuffer[screenX, screenY] = this.perpendicularWallChar;
                }
            }
        }

        private void PaintSection0(Maze maze, Player player, char[,] screenBuffer, int screenX)
        {
            var pointToTest = maze.GetMazePoint(player.Position.X, player.Position.Y + 1);
            var atPoint = pointToTest.PointType;
            if (atPoint == MazePointType.Wall)
            {
                screenBuffer[screenX, 0] = this.parallelWallChar;
                screenBuffer[screenX, 23] = this.parallelWallChar;
                for (var y = 1; y < 23; y++)
                {
                    screenBuffer[screenX, y] = this.parallelWallChar;
                }
            }
            else
            {
                screenBuffer[screenX, 0] = this.nothingChar;
                screenBuffer[screenX, 23] = this.nothingChar;
                for (var y = 1; y < 23; y++)
                {
                    screenBuffer[screenX, y] = this.perpendicularWallChar;
                }
            }
        }
    }
}
