// <copyright file="MazePoint.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Models
{
    /// <summary>
    /// Represents a single location in a two-dimensional maze.
    /// </summary>
    public class MazePoint
    {
        /// <summary>
        /// Gets or sets a value indicating whether this point is known
        /// to the user.
        /// </summary>
        public bool Explored { get; set; }

        /// <summary>
        /// Gets or sets the type of this point, e.g. is it a wall or a corridor.
        /// </summary>
        public MazePointType PointType { get; set; }
    }
}
