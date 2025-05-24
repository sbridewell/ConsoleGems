// <copyright file="TrianglePlayerCharacterProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.CharacterProviders
{
    using Sde.MazeGame.Models;

    /// <summary>
    /// This implementation represents the player using a triangle pointing in
    /// the direction that the player is facing.
    /// </summary>
    public class TrianglePlayerCharacterProvider : IPlayerCharacterProvider
    {
        private readonly char playerFacingNorthChar = '▲';
        private readonly char playerFacingEastChar = '►';
        private readonly char playerFacingSouthChar = '▼';
        private readonly char playerFacingWestChar = '◄';

        /// <inheritdoc/>
        public char GetPlayerChar(Player player)
        {
            return player.FacingDirection switch
            {
                Direction.North => this.playerFacingNorthChar,
                Direction.East => this.playerFacingEastChar,
                Direction.South => this.playerFacingSouthChar,
                Direction.West => this.playerFacingWestChar,
                _ => ' '
            };
        }
    }
}
