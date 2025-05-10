// <copyright file="ILimitOfViewProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.FogOfWar
{
    using Sde.MazeGame.Models;

    /// <summary>
    /// Interface for providing the set of points at the maximum limit
    /// of the player's field of view, relative to the player's position,
    /// and also the maximum distance that the player can see.
    /// Does not take into account whether or not there is a wall in the way.
    /// </summary>
    public interface ILimitOfViewProvider
    {
        /// <summary>
        /// Gets the distance the player can see.
        /// </summary>
        public int VisibleDistance { get; }

        /// <summary>
        /// Gets all the points at the limit of the field of view.
        /// </summary>
        public List<ConsolePointOffset> LimitOfView { get; }
    }
}
