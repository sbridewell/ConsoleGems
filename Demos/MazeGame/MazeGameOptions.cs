// <copyright file="MazeGameOptions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame
{
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

        // TODO: WithPovOrigin method? And rename WithMazeOrigin to WithMapOrigin?
    }
}
