// <copyright file="MazeFactory.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame
{
    using System.Text;
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
            try
            {
                var maze = this.CreateFromStringArray(lines);
                return maze;
            }
            catch (ArgumentException ex)
            {
                var msg = $"The file '{filename}' does not represent a valid maze. {ex.Message}";
                throw new ArgumentException(msg, nameof(filename), ex);
            }
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
            // Validate that all lines are the same length
            if (!lines.All(l => l.Length == lines[0].Length))
            {
                var sb = new StringBuilder();
                foreach (var line in lines)
                {
                    sb.Append(line.Length + " ");
                }

                var msg = $"All lines must be the same length. "
                    + $"Lengths of supplied lines are {sb.ToString()}.";
                throw new ArgumentException(msg, nameof(lines));
            }

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
                        PointType = lines[y][x] switch
                        {
                            '#' => MazePointType.Wall,
                            ' ' => MazePointType.Path,
                            'E' => MazePointType.Exit,
                            _ => MazePointType.Path,
                        },
                    };
                    maze.SetMazePoint(new ConsolePoint(x, y), point);
                }
            }

            for (var y = 0; y < height - 1; y++)
            {
                for (var x = 0; x < width - 1; x++)
                {
                    if (lines[y][x] == ' ' && lines[y + 1][x] == ' '
                        && lines[y][x + 1] == ' ' && lines[y + 1][x + 1] == ' ')
                    {
                        var msg = "Found a 2x2 square of corridors at "
                            + $"({x}, {y}). This cannot be rendered correctly in the 3D view.";
                        throw new ArgumentException(msg, nameof(lines));
                    }
                }
            }

            return maze;
        }
    }
}
