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
        public ConsolePoint MazeOrigin { get; private set; } = new ConsolePoint(10, 10);

        /// <summary>
        /// Gets the top left corner of the status area within the console window.
        /// </summary>
        public ConsolePoint StatusOrigin { get; private set; } = new ConsolePoint(0, 0);

        /// <summary>
        /// Sets the top left corner of the maze within the console window.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        /// <returns>The updated options.</returns>
        public MazeGameOptions WithMazeOrigin(int x, int y)
        {
            this.MazeOrigin = new ConsolePoint(x, y);
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
