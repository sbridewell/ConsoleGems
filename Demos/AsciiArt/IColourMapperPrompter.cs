// <copyright file="IColourMapperPrompter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt
{
    using System.Collections.Generic;
    using Sde.AsciiArt.ColourMappers;
    using Sde.ConsoleGems.Prompters;

    /// <summary>
    /// Interface for prompting the user to select a colour mapping strategy.
    /// </summary>
    public interface IColourMapperPrompter : IPrompter
    {
        /// <summary>
        /// Prompts the user to select a colour mapping strategy from the available options.
        /// </summary>
        /// <param name="options">Dictionary of available strategies (key: name, value: instance).</param>
        /// <returns>The selected IConsoleColourMapper instance.</returns>
        IConsoleColourMapper Prompt(Dictionary<string, IConsoleColourMapper> options);
    }
}
