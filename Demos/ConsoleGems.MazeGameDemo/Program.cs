// <copyright file="Program.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MazeGameDemo
{
    using Microsoft.Extensions.DependencyInjection;
    using Sde.MazeGame;
    using Sde.MazeGame.Menus;

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
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            var services = new ServiceCollection();
            var options = new ConsoleGemsOptions() { UseColours = true }
                .UseMainMenu<MainMenu>()
                .UseAutoComplete();
            services.AddConsoleGems(options);
            services.AddMazeGame();

            var provider = services.BuildServiceProvider();
            //var gameController = provider.GetRequiredService<IGameController>();
            //gameController.Play();
            var mainMenu = provider.GetRequiredService<MainMenu>();
            mainMenu.ShowCommand.Execute();
        }
    }
}
