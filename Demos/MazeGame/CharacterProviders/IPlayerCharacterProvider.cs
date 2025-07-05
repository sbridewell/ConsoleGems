// <copyright file="IPlayerCharacterProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.CharacterProviders
{
    using Sde.MazeGame.Models;

    /// <summary>
    /// Interface for getting the character to use to represent a player
    /// in a two-dimensional representation of a maze.
    /// </summary>
    public interface IPlayerCharacterProvider
    {
        /// <summary>
        /// Gets the character to use to represent the player.
        /// </summary>
        /// <param name="player">The player to represent.</param>
        /// <returns>A character to represent the player.</returns>
        public char GetPlayerChar(Player player);
    }
}
