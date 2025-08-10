// <copyright file="ColourMapperPrompter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sde.AsciiArt.ColourMappers;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Prompters;

    /// <summary>
    /// Prompter for selecting a colour mapping strategy at runtime.
    /// </summary>
    public class ColourMapperPrompter : IColourMapperPrompter
    {
        /// <summary>
        /// The console to use for user interaction.
        /// </summary>
        private readonly IConsole console;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColourMapperPrompter"/> class.
        /// </summary>
        /// <param name="console">The console to use for user interaction.</param>
        public ColourMapperPrompter(IConsole console)
        {
            this.console = console;
        }

        /// <inheritdoc/>
        public IConsoleColourMapper Prompt(Dictionary<string, IConsoleColourMapper> options)
        {
            if (options == null || options.Count == 0)
            {
                throw new ArgumentException("No colour mapping strategies available.", nameof(options));
            }

            this.console.WriteLine("Select a colour mapping strategy:");
            int i = 1;
            foreach (var key in options.Keys)
            {
                this.console.WriteLine($"  {i}. {key}");
                i++;
            }

            int selected = -1;
            while (selected < 1 || selected > options.Count)
            {
                this.console.Write("Enter the number of your choice: ");
                var input = this.console.ReadLine();
                if (!int.TryParse(input, out selected) || selected < 1 || selected > options.Count)
                {
                    this.console.WriteLine("Invalid selection. Please try again.");
                }
            }

            string selectedKey = options.Keys.ElementAt(selected - 1);
            return options[selectedKey];
        }
    }
}
