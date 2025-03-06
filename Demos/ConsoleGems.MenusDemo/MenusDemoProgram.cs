// <copyright file="MenusDemoProgram.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A demonstration of menu functionality in ConsoleGems.
    /// </summary>
    public static class MenusDemoProgram
    {
        /// <summary>
        /// Main entry point into the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            System.Console.Title = "ConsoleGems menus demo";

            // Each item in a menu is a command.
            // Some of those commands could show a child menu.
            // You don't need to explicitly specify all the child
            // menus and all the commands in all the menus, becase
            // the AddConsoleGems method will auto-discover them.
            // If any of the commands have other dependencies,
            // you will need to explicitly register them with the
            // service collection.
            // It's not necessary to use a SharedMenuItemsProvider,
            // but if you do then all the items in it will be added
            // to all the menus in the application.
            var options = new ConsoleGemsOptions { UseColours = true }
                .UseBuiltInPrompters() // because some of the built-in commands need prompters
                .UseSharedMenuItemsProvider<SharedMenuItemsProvider>()
                .UseMainMenu<MainMenu>();

            var services = new ServiceCollection().AddConsoleGems(options);

            var provider = services.BuildServiceProvider();
            var mainMenu = provider.GetRequiredService<MainMenu>();
            mainMenu.ShowCommand.Execute();
        }
    }
}
