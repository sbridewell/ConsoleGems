// <copyright file="MazeGameOptions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame
{
    using System.Diagnostics.CodeAnalysis;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Options which control the behaviour of the maze game.
    /// </summary>
    public class MazeGameOptions
    {
        // TODO: IFluentOptions with Validate() method to check all properties are initialised?

        /// <summary>
        /// Gets the top left corner of the maze within the console window.
        /// </summary>
        public ConsolePoint MapViewOrigin { get; private set; }

        /// <summary>
        /// Gets the top left corner of the point of view (POV) view of the maze within the console window.
        /// </summary>
        public ConsolePoint PovViewOrigin { get; private set; }

        /// <summary>
        /// Gets the top left corner of the status area within the console window.
        /// </summary>
        public ConsolePoint StatusOrigin { get; private set; }

        /// <summary>
        /// Gets the path to a file containing the data from which the maze is constructed.
        /// </summary>
        public string MazeDataFile { get; private set; } = string.Empty;

        /// <summary>
        /// Sets the top left corner of the map view of the maze within the console window.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        /// <returns>The updated options.</returns>
        public MazeGameOptions WithMapViewOrigin(int x, int y)
        {
            this.MapViewOrigin = new ConsolePoint(x, y);
            return this;
        }

        /// <summary>
        /// Sets the top left corner of the point-of-view view of the maze within the console window.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        /// <returns>The updated options.</returns>
        public MazeGameOptions WithPovViewOrigin(int x, int y)
        {
            this.PovViewOrigin = new ConsolePoint(x, y);
            return this;
        }

        /// <summary>
        /// Sets the top left corner of the status area within the console window.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        /// <returns>The updated options.</returns>
        public MazeGameOptions WithStatusOrigin(int x, int y)
        {
            this.StatusOrigin = new ConsolePoint(x, y);
            return this;
        }

        /// <summary>
        /// Sets the path to a file containing the data from which the maze is constructed.
        /// </summary>
        /// <param name="path">Path to the maze data file.</param>
        /// <returns>The updated options.</returns>
        public MazeGameOptions WithMazeDataFile(string path)
        {
            this.MazeDataFile = path;
            return this;
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"MapViewOrigin: {this.MapViewOrigin}, " +
                   $"PovViewOrigin: {this.PovViewOrigin}, " +
                   $"StatusOrigin: {this.StatusOrigin}, " +
                   $"MazeDataFile: {this.MazeDataFile}";
        }
    }
}
