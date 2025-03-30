// <copyright file="MenusDemoProgram.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using Microsoft.Extensions.DependencyInjection;
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.AutoComplete.Matchers;
    using Sde.ConsoleGems.Menus;

    /// <summary>
    /// A demonstration of menu functionality in ConsoleGems.
    /// </summary>
    public static class MenusDemoProgram
    {
        /// <summary>
        /// Main entry point into the program.
        /// </summary>
        public static void Main()
        {
            System.Console.Title = "ConsoleGems menus demo";

            var autoCompleter = new AutoCompleter(
                new AutoCompleteKeyPressDefaultMappings(),
                new StartsWithMatcher(),
                new Consoles.Console());
            var suggestions = new List<string> { nameof(MenuWriter), nameof(FigletMenuWriter), };
            var menuWriterTypeName = autoCompleter.ReadLine(suggestions, "Select a menu writer: ");

            // Each item in a menu is a command.
            // Some of those commands could show a child menu.
            // You don't need to explicitly specify all the child
            // menus and all the commands in all the menus, becase
            // the AddConsoleGems method will auto-discover them.
            // If any of the commands have other dependencies,
            // you will need to explicitly register them with the
            // service collection.
            var options = new ConsoleGemsOptions { UseColours = true }
                .UseBuiltInPrompters() // because some of the built-in commands need prompters

                // It's not necessary to use a SharedMenuItemsProvider,
                // but if you do then all the items in it will be added
                // to all the menus in the application.
                .UseSharedMenuItemsProvider<SharedMenuItemsProvider>()

                // It's not necessary to supply a MenuWriter, but if you
                // want to use a different IMenuWriter implementation then
                // you can specify it here.
                ////.UseMenuWriter<FigletMenuWriter>()

                // You do need to specify a main menu though, otherwise
                // ConsoleGems won't auto-discover and register the child
                // menus and their commands.
                .UseMainMenu<MainMenu>();

            // Normally we'd specify the type of menu writer in the fluent code
            // above, but for the purposes of this demo, we need to specify it
            // conditionally based on user input.
            if (menuWriterTypeName == nameof(FigletMenuWriter))
            {
                options.UseMenuWriter<FigletMenuWriter>();
            }

            var services = new ServiceCollection().AddConsoleGems(options);
            var provider = services.BuildServiceProvider();
            var mainMenu = provider.GetRequiredService<MainMenu>();
            mainMenu.ShowCommand.Execute();
            Console.WriteLine("Press any key to close the window");
            Console.ReadKey();
        }
    }
}
