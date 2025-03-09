// <copyright file="ColoursDemoProgram.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.ColoursDemo
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Demonstration of how the ConsoleGems library
    /// uses colours.
    /// </summary>
    public static class ColoursDemoProgram
    {
        /// <summary>
        /// Main entry point into the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            System.Console.Title = "ConsoleGems colours demo";
            var options = new ConsoleGemsOptions { UseColours = true };
            var services = new ServiceCollection()
                .AddConsoleGems(options)
                .AddSingleton<ColourfulConsoleDemo>();
            var serviceProvider = services.BuildServiceProvider();
            var demo = serviceProvider.GetRequiredService<ColourfulConsoleDemo>();
            demo.DemonstrateColours();
        }
    }
}
