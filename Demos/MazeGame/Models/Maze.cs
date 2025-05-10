// <copyright file="Maze.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Models
{
    using Sde.ConsoleGems.Text;

    // TODO: Maze isn't really a model if it has methods - think of another approriate class to put them in

    /// <summary>
    /// Represents a two-dimensional maze.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Maze"/> class.
    /// </remarks>
    /// <param name="width">Width of the maze.</param>
    /// <param name="height">Height of the maze.</param>
    public class Maze(int width, int height)
    {
        /// <summary>
        /// Gets the points which make up the maze.
        /// </summary>
        private readonly MazePoint[,] mazePoints = new MazePoint[width, height];

        /// <summary>
        /// Gets the width of the maze.
        /// </summary>
        public int Width => width;

        /// <summary>
        /// Gets the height of the maze.
        /// </summary>
        public int Height => height;

        /// <summary>
        /// Gets the position at the supplied point within the maze.
        /// </summary>
        /// <param name="position">The position within the maze.</param>
        /// <returns>The entity at the supplied position.</returns>
        public MazePoint GetMazePoint(ConsolePoint position)
        {
            return this.GetMazePoint(position.X, position.Y);
        }

        /// <summary>
        /// Gets the point at the supplied coordinates within the maze.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        /// <returns>The point at the supplied coordeinates.</returns>
        public MazePoint GetMazePoint(int x, int y)
        {
            if (x < 0 || x >= this.Width)
            {
                var msg = $"X coordinate {x} is out of range. Valid range is 0 to {this.Width - 1}.";
                throw new ArgumentOutOfRangeException(nameof(x), msg);
            }

            if (y < 0 || y >= this.Height)
            {
                var msg = $"Y coordinate {y} is out of range. Valid range is 0 to {this.Height - 1}.";
                throw new ArgumentOutOfRangeException(nameof(y), msg);
            }

            return this.mazePoints[x, y];
        }

        /// <summary>
        /// Sets the type of point at the supplied position within the maze.
        /// </summary>
        /// <param name="position">The position within the maze.</param>
        /// <param name="mazePoint">The point to add to the maze.</param>
        public void SetMazePoint(ConsolePoint position, MazePoint mazePoint)
        {
            this.mazePoints[position.X, position.Y] = mazePoint;
        }

        /// <summary>
        /// Determines whether or not there is a wall immediately to the
        /// north of the supplied position.
        /// </summary>
        /// <param name="position">The position within the maze.</param>
        /// <returns>
        /// True if there is a wall immediately to the north of the supplied
        /// position.
        /// </returns>
        public bool ThereIsAWallToTheNorthOf(ConsolePoint position)
        {
            // TODO: use <= rather than ==? causes wrong characters to be rendered
            if (position.Y == 0)
            {
                return false;
            }

            var mazePoint = this.GetMazePoint(position.X, position.Y - 1);
            return mazePoint.PointType == MazePointType.Wall && mazePoint.Explored;
        }

        /// <summary>
        /// Determines whether or not there is a wall immediately to the
        /// south of the supplied position.
        /// </summary>
        /// <param name="position">The position within the maze.</param>
        /// <returns>
        /// True if there is a wall immediately to the south of the supplied
        /// position.
        /// </returns>
        public bool ThereIsAWallToTheSouthOf(ConsolePoint position)
        {
            if (position.Y >= this.Height - 1)
            {
                return false;
            }

            var mazePoint = this.GetMazePoint(position.X, position.Y + 1);
            return mazePoint.PointType == MazePointType.Wall && mazePoint.Explored;
        }

        /// <summary>
        /// Determines whether or not there is a wall immediately to the
        /// west of the supplied position.
        /// </summary>
        /// <param name="position">The position within the maze.</param>
        /// <returns>
        /// True if there is a wall immediately to the west of the supplied
        /// position.
        /// </returns>
        public bool ThereIsAWallToTheWestOf(ConsolePoint position)
        {
            if (position.X <= 0)
            {
                return false;
            }

            var mazePoint = this.GetMazePoint(position.X - 1, position.Y);
            return mazePoint.PointType == MazePointType.Wall && mazePoint.Explored;
        }

        /// <summary>
        /// Determines whether or not there is a wall immediately to the
        /// east of the supplied position.
        /// </summary>
        /// <param name="position">The position within the maze.</param>
        /// <returns>
        /// True if there is a wall immediately to the east of the supplied
        /// position.
        /// </returns>
        public bool ThereIsAWallToTheEastOf(ConsolePoint position)
        {
            if (position.X >= this.Width - 1)
            {
                return false;
            }

            var mazePoint = this.GetMazePoint(position.X + 1, position.Y);
            return mazePoint.PointType == MazePointType.Wall && mazePoint.Explored;
        }

        /// <summary>
        /// Determines whether or not the supplied position is within the
        /// maze.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>
        /// True if the supplied position is within the maze, otherwise false.
        /// </returns>
        public bool PositionIsWithinMaze(ConsolePoint position)
        {
            //if (position.X < 0 || position.X >= this.Width)
            //{
            //    return false;
            //}

            //if (position.Y < 0 || position.Y >= this.Height)
            //{
            //    return false;
            //}

            //return true;
            return this.PositionIsWithinMaze(position.X, position.Y);
        }

        public bool PositionIsWithinMaze(int x, int y)
        {
            if (x < 0 || x >= this.Width)
            {
                return false;
            }

            if (y < 0 || y >= this.Height)
            {
                return false;
            }

            return true;
        }
    }
}
