// <copyright file="MazeGameRandomiser.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame
{
    using System.Diagnostics.CodeAnalysis;
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Generates random values for the maze game.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MazeGameRandomiser : IMazeGameRandomiser
    {
        /// <inheritdoc/>
        public Direction GetDirection()
        {
            var random = new Random();
            var direction = (Direction)random.Next(0, 4);
            return direction;
        }

        /// <inheritdoc/>
        public ConsolePoint GetPosition(Maze maze)
        {
            var x = new Random().Next(0, maze.Width);
            var y = new Random().Next(0, maze.Height);
            if (maze.GetMazePoint(x, y).PointType == MazePointType.Path)
            {
                return new ConsolePoint(x, y);
            }
            else
            {
                return this.GetPosition(maze);
            }
        }
    }
}
