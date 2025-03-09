// <copyright file="ShowMenuCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Commands
{
    /// <summary>
    /// Command to pass control to a menu.
    /// </summary>
    public class ShowMenuCommand(
        IMenu menu,
        IAutoCompleter autoCompleter,
        IMenuWriter menuWriter,
        IConsole console,
        ApplicationState applicationState)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            applicationState.MenuDepth++;
            menuWriter.WriteMenu(menu);
            var items = menuWriter.GetAllMenuItems(menu);
            var menuKeys = items.Select(item => item.Key.Trim().ToLower()).ToList();
            while (!applicationState.ExitCurrentMenu)
            {
                var userInput = autoCompleter.ReadLine(menuKeys, "Choose an option: ");

                // TODO: trim and tolower in autoCompleteConsole.ReadLine based on flags parameter?
                var userInputSanitised = userInput.Trim().ToLower();
                var selectedItem = items
                    .SingleOrDefault(item => item.Key.Trim().Equals(
                        userInputSanitised,
                        StringComparison.CurrentCultureIgnoreCase));
                if (selectedItem == null)
                {
                    console.WriteLine($"Sorry, '{userInputSanitised}' is not an option in this menu, try again!", ConsoleOutputType.Error);
                    menuWriter.WriteMenu(menu);
                }
                else
                {
                    try
                    {
                        selectedItem.Command.Execute();
                    }
                    catch (Exception ex)
                    {
                        console.WriteLine($"Unhandled exception: {ex}", ConsoleOutputType.Error);
                    }

                    if (!applicationState.ExitCurrentMenu)
                    {
                        menuWriter.WriteMenu(menu);
                    }
                }
            }

            applicationState.ExitCurrentMenu = false;
        }
    }
}
