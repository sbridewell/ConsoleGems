// <copyright file="RayTracingOutcome.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Painters.RayTracing
{
    /// <summary>
    /// Encapsulates the result of tracing a single ray.
    /// </summary>
    public class Ray
    {
        /// <summary>
        /// Gets or sets the direction of the ray.
        /// </summary>
        public float Direction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ray has hit a wall.
        /// </summary>
        public bool HasHitAWall { get; set; }

        /// <summary>
        /// Gets or sets the distance that the ray has travelled.
        /// </summary>
        public float Distance { get; set; }

        public override string ToString()
        {
            return $"Direction: {this.Direction}, HasHitWall: {this.HasHitAWall}, Distance: {this.Distance}";
        }
    }
}
