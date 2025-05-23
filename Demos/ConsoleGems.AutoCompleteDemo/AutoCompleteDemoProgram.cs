﻿// <copyright file="AutoCompleteDemoProgram.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoCompleteDemo
{
    using Microsoft.Extensions.DependencyInjection;
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.AutoComplete.Matchers;
    using Sde.ConsoleGems.Prompters;

    /// <summary>
    /// Class containing the main entry point into the program.
    /// </summary>
    public static class AutoCompleteDemoProgram
    {
        /// <summary>
        /// Main entry point into the application.
        /// </summary>
        /// <param name="args">
        /// Command-line arguments.
        /// Supply at least one argument with any value to use colours.
        /// Supply no arguments to use monochrome.
        /// </param>
        public static void Main(string[] args)
        {
            System.Console.Title = "ConsoleGems auto-complete demo";

            var options = new ConsoleGemsOptions();
            if (args.Length > 0)
            {
                options.UseColours = true;
            }

            // This demo shows 3 types of autocomplete.
            // The first is a simple list of strings, the second auto-
            // completes names of files in the My Documents folder, and
            // the third auto-completes names of directories in the My
            // Documents folder.
            options.UseAutoComplete(
                autoCompleteOptions => autoCompleteOptions

                    // if you don't call them then ConsoleGems will use the
                    // default implementations.
                    .UseKeyPressMappings<CustomAutoCompleteKeyPressMappings>()
                    .UseMatcher<StartsWithMatcher>())
                .UseBuiltInPrompters();

            var serviceCollection = new ServiceCollection()
                .AddConsoleGems(options)
                .AddSingleton<AutoCompleteDemo>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var demo = serviceProvider.GetRequiredService<AutoCompleteDemo>();
            demo.ChooseADrink();
            Console.WriteLine("Press any key to close the window");
            Console.ReadKey();
        }
    }
}
