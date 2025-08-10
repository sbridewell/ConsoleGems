// <copyright file="ICharacterBlender.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.CharacterBlenders
{
    /// <summary>
    /// Defines a contract for mapping a foreground/background ratio to a suitable character.
    /// </summary>
    public interface ICharacterBlender
    {
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
        char GetCharacterForRatio(double ratio);
    }
}
