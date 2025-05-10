// <copyright file="ConsolePointOffset.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Models
{
    /// <summary>
    /// Represents a point relative to the player's current position.
    /// </summary>
    public class ConsolePointOffset(int dx, int dy)
    {
        /// <summary>
        /// Gets the vertical or north/south component of the offset.
        /// Negative numbers represent points above or to the north of the player.
        /// Positive numbers represent points below or to the south of the player.
        /// </summary>
        public int DX => dx;

        /// <summary>
        /// Gets the horizontal or east/west component of the offset.
        /// Negative numbers represent points to the left or west of the player.
        /// Positive numbers represent points to the right or east of the player.
        /// </summary>
        public int DY => dy;

        /// <summary>
        /// Gets a vector representing one step in the direction of the offset.
        /// </summary>
        public (double dx, double dy) Direction
        {
            get
            {
                var length = Math.Sqrt((dx * dx) + (dy * dy));
                return (dx / length, dy / length);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{dx},{dy}]";
        }
    }
}
