// <copyright file="MultiCharacterBlender.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.CharacterBlenders
{
    /// <summary>
    /// Maps foreground/background ratios to a large set of characters for ASCII art rendering,
    /// providing smoother gradients and more visual detail than block characters alone.
    /// </summary>
    public class MultiCharacterBlender : ICharacterBlender
    {
        /// <summary>
        /// The ordered set of characters, from least to most filled, used for blending.
        /// </summary>
        public static readonly char[] Characters = new[]
        {
            ' ', '.', ',', ':', '-', '~', '+', '*', '=', '%', '@', '#',
            '░', '▒', '▓', '▁', '▂', '▃', '▄', '▅', '▆', '▇', '█',
        };

        /// <summary>
        /// Gets a character that best represents the given foreground-to-background ratio.
        /// </summary>
        /// <param name="ratio">
        /// The ratio of foreground to background, in the range [0.0, 1.0].
        /// 0.0 means all background, 1.0 means all foreground.
        /// </param>
        /// <returns>
        /// A character that visually approximates the given ratio.
        /// </returns>
        public char GetCharacterForRatio(double ratio)
        {
            if (ratio <= 0.0)
            {
                return Characters[0];
            }

            if (ratio >= 1.0)
            {
                return Characters[^1];
            }

            int index = (int)(ratio * (Characters.Length - 1));
            return Characters[index];
        }
    }
}
