// <copyright file="DiagonalCrossWallCharacterProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.MazeGame.CharacterProviders
{
    using Sde.ConsoleGems.Text;
    using Sde.MazeGame.Models;

    /// <summary>
    /// A very simple implementation which represents all wall positions as a diagonal cross.
    /// </summary>
    public class DiagonalCrossWallCharacterProvider : IWallCharacterProvider
    {
        /// <inheritdoc/>
        public char GetWallChar(Maze maze, ConsolePoint position)
        {
            return '╳';
        }
    }
}
