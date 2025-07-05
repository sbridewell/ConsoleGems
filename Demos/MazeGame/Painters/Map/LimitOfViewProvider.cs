// <copyright file="LimitOfViewProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.Map
{
    using Sde.MazeGame.Models;

    /// <summary>
    /// Provides the limit of the player's field of view, assuming a 360
    /// degree field of view, without taking into account whether or not
    /// there is a wall in the way.
    /// </summary>
    public class LimitOfViewProvider : ILimitOfViewProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LimitOfViewProvider"/> class.
        /// </summary>
        /// <param name="visibleDistance">How far the player can see.</param>
        public LimitOfViewProvider(int visibleDistance)
        {
            this.VisibleDistance = visibleDistance;
            this.LimitOfView = new ();
            for (var dy = -visibleDistance; dy <= visibleDistance; dy++)
            {
                for (var dx = -visibleDistance; dx <= visibleDistance; dx++)
                {
                    if ((dx * dx) + (dy * dy) <= visibleDistance * visibleDistance)
                    {
                        this.LimitOfView.Add(new ConsolePointOffset(dx, dy));
                    }
                }
            }
        }

        /// <inheritdoc/>
        public int VisibleDistance { get; }

        /// <inheritdoc/>
        public List<ConsolePointOffset> LimitOfView { get; }
    }
}
