// <copyright file="CharacterBlenderPrompter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sde.AsciiArt.CharacterBlenders;
    using Sde.ConsoleGems.Consoles;

    /// <summary>
    /// Prompter for selecting a character blender strategy at runtime.
    /// </summary>
    public class CharacterBlenderPrompter(IConsole console)
        : ICharacterBlenderPrompter
    {
        /// <inheritdoc/>
        public ICharacterBlender Prompt(Dictionary<string, ICharacterBlender> options)
        {
            if (options == null || options.Count == 0)
            {
                throw new ArgumentException("No character blender strategies available.", nameof(options));
            }

            console.WriteLine("Select a character blender strategy:");
            int i = 1;
            foreach (var key in options.Keys)
            {
                console.WriteLine($"  {i}. {key}");
                i++;
            }

            int selected = -1;
            while (selected < 1 || selected > options.Count)
            {
                console.Write("Enter the number of your choice: ");
                var input = console.ReadLine();
                if (!int.TryParse(input, out selected) || selected < 1 || selected > options.Count)
                {
                    console.WriteLine("Invalid selection. Please try again.", ConsoleOutputType.Error);
                }
            }

            string selectedKey = options.Keys.ElementAt(selected - 1);
            return options[selectedKey];
        }
    }
}
