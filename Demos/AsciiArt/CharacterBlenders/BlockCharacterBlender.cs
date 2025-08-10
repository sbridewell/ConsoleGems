// <copyright file="BlockCharacterBlender.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.CharacterBlenders
{
    /// <summary>
    /// Maps foreground/background ratios to block characters for ASCII art rendering,
    /// using 5 characters which provide ratios of 0%, 25%, 50%, 75%, and 100% foreground.
    /// </summary>
    public class BlockCharacterBlender : ICharacterBlender
    {
        /// <inheritdoc/>
        public char GetCharacterForRatio(double ratio)
        {
            return ratio switch
            {
                <= 0.0 => ' ',
                < 0.25 => '░', // U+2591
                < 0.5 => '▒',  // U+2592
                < 0.75 => '▓', // U+2593
                < 1.0 => '█',  // U+2588
                _ => '█',
            };
        }
    }
}
