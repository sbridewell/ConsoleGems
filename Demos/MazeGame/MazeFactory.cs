// <copyright file="MazeFactory.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame
{
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    /// <summary>
    /// Creates <see cref="Maze"/> instances.
    /// </summary>
    public class MazeFactory
    {
        /// <summary>
        /// Creates a <see cref="Maze"/> from the supplied text file.
        /// </summary>
        /// <param name="filename">Name of the file.</param>
        /// <returns>The maze represented by the file.</returns>
        public Maze CreateFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var maze = this.CreateFromStringArray(lines);
            return maze;
        }

        /// <summary>
        /// Creates a <see cref="Maze"/> from the supplied string array.
        /// </summary>
        /// <param name="lines">
        /// Lines of text representing the maze.
        /// # characters represent walls. Space characters represent corridors.
        /// </param>
        /// <returns>The maze represented by the supplied string array.</returns>
        public Maze CreateFromStringArray(string[] lines)
        {
            // TODO: validate that all lines are the same length
            var width = lines[0].Length;
            var height = lines.Length;
            var maze = new Maze(width, height);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var point = new MazePoint
                    {
                        Explored = false,
                        PointType = lines[y][x] == '#' ? MazePointType.Wall : MazePointType.Path,
                    };
                    maze.SetMazePoint(new ConsolePoint(x, y), point);
                }
            }

            return maze;
        }
    }
}
