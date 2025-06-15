// <copyright file="MazePointType.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Models
{
    /// <summary>
    /// The type of a point in a two-dimensional maze.
    /// </summary>
    public enum MazePointType
    {
        /// <summary>
        /// The point is one which can be occupied by a player.
        /// </summary>
        Path,

        /// <summary>
        /// The point is a wall.
        /// Players cannot walk through walls.
        /// </summary>
        Wall,

        /// <summary>
        /// The point is outside the bounds of the maze.
        /// </summary>
        OutsideMaze,
    }
}
