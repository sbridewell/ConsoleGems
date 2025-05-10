// <copyright file="DistanceApproximation.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.RayTracing
{
    public enum DistanceApproximation
    {
        /// <summary>
        /// The distance is close to the player.
        /// </summary>
        Close,

        /// <summary>
        /// The distance is a moderate distance from the player.
        /// </summary>
        Medium,

        /// <summary>
        /// The distance is a long way from the player.
        /// </summary>
        Far,

        /// <summary>
        /// The distance is beyond the player's range of visibility.
        /// </summary>
        Invisible,
    }
}
