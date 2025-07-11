// <copyright file="Program.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.GamesDemo
{
    using Microsoft.Extensions.DependencyInjection;
    using Sde.MazeGame;
    using Sde.SnakeGame;

    /// <summary>
    /// Class containing the main entry point into the program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entry point into the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var services = new ServiceCollection();
            var options = new ConsoleGemsOptions() { UseColours = true }
                .UseMainMenu<MainMenu>()
                .UseAutoComplete();
            services.AddConsoleGems(options);
            services.AddMazeGame();
            services.AddSnakeGame();

            var provider = services.BuildServiceProvider();
            var mainMenu = provider.GetRequiredService<MainMenu>();
            mainMenu.ShowCommand.Execute();
        }
    }
}
