// <copyright file="Player.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.Models
{
    using System.Diagnostics.CodeAnalysis;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Represents a player who is exploring the maze.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Player
    {
        /// <summary>
        /// Gets or sets the player's position relative to the top-left corner of the maze.
        /// </summary>
        public ConsolePoint Position { get; set; }

        /// <summary>
        /// Gets or sets the direction that the player is fasing.
        /// </summary>
        public Direction FacingDirection { get; set; }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"Player Facing:{this.FacingDirection}, Position:{this.Position}";
        }
    }
}
