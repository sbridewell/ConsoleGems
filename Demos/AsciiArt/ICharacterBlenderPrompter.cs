// <copyright file="ICharacterBlenderPrompter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt
{
    using System.Collections.Generic;
    using Sde.AsciiArt.CharacterBlenders;

    /// <summary>
    /// Interface for prompting the user to select a character blender strategy.
    /// </summary>
    public interface ICharacterBlenderPrompter
    {
        /// <summary>
        /// Prompts the user to select a character blender strategy from the available options.
        /// </summary>
        /// <param name="options">Dictionary of available strategies (key: name, value: instance).</param>
        /// <returns>The selected ICharacterBlender instance.</returns>
        ICharacterBlender Prompt(Dictionary<string, ICharacterBlender> options);
    }
}
