// <copyright file="ExtendedCharacterBlender.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.CharacterBlenders
{
    /// <summary>
    /// Maps foreground/background ratios to a set of 255 characters for ASCII art rendering,
    /// providing a very smooth gradient and high visual detail.
    /// </summary>
    public class ExtendedCharacterBlender : ICharacterBlender
    {
        /// <summary>
        /// The ordered set of 255 characters, from least to most filled, used for blending.
        /// </summary>
        public static readonly char[] Characters = new char[]
        {
            ' ', '.', '`', '\u00B7', ',', ':', ';', '-', '_', '\u00A2', '~', '!', 'i', 'l', 'I', 'j', 't', 'r', 'c', 'v', 'u', 'Y', 'J', 'f', '1', '(', ')', '[', ']', '{', '}', '<', '>', '/', '\\', '|', 's', 'z', 'x', 'n', 'e', 'a', 'o', '*', '+', '=', '^', '"', '\u00B0', '?', '7', 'T', 'F', 'L', 'C', 'E', 'S', 'Z', 'X', 'N', 'U', 'V', 'A', 'O', 'Q', 'D', 'P', 'G', 'H', 'K', 'B', 'M', 'W', 'm', 'w', 'g', 'h', 'k', 'b', 'd', 'p', 'q', 'y', '2', '3', '4', '5', '6', '8', '9', '0', 'R', 'Y', 'T', 'F', 'L', 'C', 'E', 'S', 'Z', 'X', 'N', 'U', 'V', 'A', 'O', 'Q', 'D', 'P', 'G', 'H', 'K', 'B', 'M', 'W', '@', '#', '$', '%', '&', '\u00A3', '\u00A5', '\u00A7', '\u00A9', '\u00AE', '\u00B1', '\u00B5', '\u00B6', '\u00B8', '\u00BC', '\u00BD', '\u00BE', '\u00BF', '\u00C6', '\u00D8', '\u00DF', '\u00E6', '\u00F8', '\u00FF', '\u2591', '\u2592', '\u2593', '\u2581', '\u2582', '\u2583', '\u2584', '\u2585', '\u2586', '\u2587', '\u2588', '\u25A0', '\u25A1', '\u25A3', '\u25A4', '\u25A5', '\u25A6', '\u25A7', '\u25A8', '\u25A9', '\u25AA', '\u25AB', '\u25AC', '\u25AD', '\u25AE', '\u25AF', '\u25B0', '\u25B1', '\u25B2', '\u25B3', '\u25B4', '\u25B5', '\u25B6', '\u25B7', '\u25B8', '\u25B9', '\u25BA', '\u25BB', '\u25BC', '\u25BD', '\u25BE', '\u25BF', '\u25C0', '\u25C1', '\u25C2', '\u25C3', '\u25C4', '\u25C5', '\u25C6', '\u25C7', '\u25C8', '\u25C9', '\u25CA', '\u25CB', '\u25CC', '\u25CD', '\u25CE', '\u25CF', '\u25D0', '\u25D1', '\u25D2', '\u25D3', '\u25D4', '\u25D5', '\u25D6', '\u25D7', '\u25D8', '\u25D9', '\u25DA', '\u25DB', '\u25DC', '\u25DD', '\u25DE', '\u25DF', '\u25E0', '\u25E1', '\u25E2', '\u25E3', '\u25E4', '\u25E5', '\u25E6', '\u25E7', '\u25E8', '\u25E9', '\u25EA', '\u25EB', '\u25EC', '\u25ED', '\u25EE', '\u25EF', '\u25F0', '\u25F1', '\u25F2', '\u25F3', '\u25F4', '\u25F5', '\u25F6', '\u25F7', '\u25F8', '\u25F9', '\u25FA', '\u25FB', '\u25FC', '\u25FD', '\u25FE', '\u25FF',
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
