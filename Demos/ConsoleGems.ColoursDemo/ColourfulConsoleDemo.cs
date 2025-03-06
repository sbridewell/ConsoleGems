// <copyright file="ColourfulConsoleDemo.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.ColoursDemo
{
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Consoles.Interfaces;

    /// <summary>
    /// Class to demonstrate how the ConsoleGems library uses colours.
    /// </summary>
    public class ColourfulConsoleDemo(IConsole console)
    {
        /// <summary>
        /// Demonstrates how the ConsoleGems library uses colours.
        /// </summary>
        public void DemonstrateColours()
        {
            // I don't recommend using these colours in a real application.
            // You don't need to explicitly set the colours as ConsoleGems
            // initialises them to some fairly sensible values, but you can
            // change them if you want.
            ConsoleColours.Error = new ConsoleColours(ConsoleColor.White, ConsoleColor.Red);
            ConsoleColours.Prompt = new ConsoleColours(ConsoleColor.DarkBlue, ConsoleColor.Yellow);
            ConsoleColours.Default = new ConsoleColours(ConsoleColor.Gray, ConsoleColor.Black);
            ConsoleColours.MenuBody = new ConsoleColours(ConsoleColor.White, ConsoleColor.DarkBlue);
            ConsoleColours.MenuHeader = new ConsoleColours(ConsoleColor.White, ConsoleColor.DarkGreen);
            ConsoleColours.UserInput = new ConsoleColours(ConsoleColor.White, ConsoleColor.DarkMagenta);

            // Now that the colour scheme has been set for the whole application,
            // you don't need to think about what the right colours are every
            // time you write to the console, just the correct ConsoleOutputType.
            console.WriteLine("Error messages look like this.", ConsoleOutputType.Error);
            console.WriteLine("Prompts for user input look like this.", ConsoleOutputType.Prompt);
            console.WriteLine("Default text looks like this.", ConsoleOutputType.Default);
            console.WriteLine("Menu body text looks like this.", ConsoleOutputType.MenuBody);
            console.WriteLine("Menu headers look like this.", ConsoleOutputType.MenuHeader);
            console.WriteLine("User input looks like this.", ConsoleOutputType.UserInput);
            console.WriteLine("And because the output type is an optional parameter, the default colours are used if one isn't supplied.");
        }
    }
}
