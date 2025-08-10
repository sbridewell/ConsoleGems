// <copyright file="CreateAsciiArtCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.GamesDemo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Versioning;
    using Sde.AsciiArt;
    using Sde.AsciiArt.CellMappers;
    using Sde.AsciiArt.CharacterBlenders;
    using Sde.AsciiArt.ColourMappers;
    using Sde.AsciiArt.Generators;
    using Sde.ConsoleGems.Commands;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Prompters;

    /// <summary>
    /// Command to create ASCII art from an image file and render it to the console.
    /// </summary>
    [SupportedOSPlatform("Windows")]
    public class CreateAsciiArtCommand(
        IConsole console,
        IFilePrompter prompter,
        IColourMapperPrompter colourMapperPrompter,
        IAsciiArtGenerator asciiArtGenerator)
        : ICommand
    {
        /// <summary>
        /// Gets the console to render output to.
        /// </summary>
        protected IConsole Console { get; } = console;

        /// <summary>
        /// Gets the file prompter used to select image files.
        /// </summary>
        protected IFilePrompter Prompter { get; } = prompter;

        /// <summary>
        /// Gets the colour mapper prompter used to select a colour mapping strategy.
        /// </summary>
        protected IColourMapperPrompter ColourMapperPrompter { get; } = colourMapperPrompter;

        private readonly IAsciiArtGenerator asciiArtGenerator = asciiArtGenerator;

        /// <inheritdoc/>
        [SupportedOSPlatform("windows")]
        public void Execute()
        {
            var imagePath = Prompter.Prompt(
                new DirectoryInfo(Environment.CurrentDirectory),
                "Select an image file to render as ASCII art: ",
                true);

            // Prompt for quality mode
            Console.WriteLine("Select quality mode:");
            Console.WriteLine("  1. Fast (uses selected strategies)");
            Console.WriteLine("  2. Best quality (tries all combinations)");
            int qualityMode = 0;
            while (qualityMode != 1 && qualityMode != 2)
            {
                Console.Write("Enter the number of your choice: ");
                var input = Console.ReadLine();
                int.TryParse(input, out qualityMode);
                if (qualityMode != 1 && qualityMode != 2)
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            }

            // Discover all IConsoleColourMapper implementations
            var mapperType = typeof(IConsoleColourMapper);
            var mappers = Assembly.GetAssembly(mapperType)!
                .GetTypes()
                .Where(t => mapperType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();
            var colourOptions = new Dictionary<string, IConsoleColourMapper>();
            foreach (var type in mappers)
            {
                try
                {
                    var instance = (IConsoleColourMapper)Activator.CreateInstance(type)!;
                    colourOptions[type.Name] = instance;
                }
                catch
                {
                    // Ignore types that can't be instantiated
                }
            }

            // Discover all ICharacterBlender implementations
            var blenderType = typeof(ICharacterBlender);
            var blenders = Assembly.GetAssembly(blenderType)!
                .GetTypes()
                .Where(t => blenderType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();
            var blenderOptions = new Dictionary<string, ICharacterBlender>();
            foreach (var type in blenders)
            {
                try
                {
                    var instance = (ICharacterBlender)Activator.CreateInstance(type)!;
                    blenderOptions[type.Name] = instance;
                }
                catch
                {
                    // Ignore types that can't be instantiated
                }
            }

            // Ensure MultiCharacterBlender is available
            if (!blenderOptions.ContainsKey("MultiCharacterBlender"))
            {
                blenderOptions["MultiCharacterBlender"] = new MultiCharacterBlender();
            }

            IAsciiArtCellMapper cellMapper;
            if (qualityMode == 1)
            {
                // Prompt once for colour mapping strategy
                var selectedMapper = ColourMapperPrompter.Prompt(colourOptions);
                var characterBlenderPrompter = new CharacterBlenderPrompter(Console);
                var selectedCharacterBlender = characterBlenderPrompter.Prompt(blenderOptions);
                cellMapper = new SimpleCellMapper(selectedMapper, selectedMapper, selectedCharacterBlender);
            }
            else
            {
                // Prompt for character blender when best quality mode is selected
                var characterBlenderPrompter = new CharacterBlenderPrompter(Console);
                var selectedCharacterBlender = characterBlenderPrompter.Prompt(blenderOptions);
                cellMapper = new BlendingCellMapper(selectedCharacterBlender);
            }

            asciiArtGenerator.RenderImageAsAsciiArt(imagePath.FullName, Console, cellMapper);
        }
    }
}
