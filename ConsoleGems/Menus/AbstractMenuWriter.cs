// <copyright file="AbstractMenuWriter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <summary>
    /// Abstract base class for <see cref="IMenuWriter"/> implementations.
    /// </summary>
    public abstract class AbstractMenuWriter(
        ISharedMenuItemsProvider sharedMenuItemsProvider,
        IGlobalMenuItemsProvider globalMenuItemsProvider,
        ExitCurrentMenuCommand exitCurrentMenuCommand,
        ApplicationState applicationState)
        : IMenuWriter
    {
        /// <inheritdoc/>
        public abstract void WriteMenu(IMenu menu);

        /// <inheritdoc/>
        public List<MenuItem> GetAllMenuItems(IMenu menu)
        {
            List<MenuItem> items =
            [
                .. menu.MenuItems,
                .. sharedMenuItemsProvider.MenuItems,
                .. globalMenuItemsProvider.MenuItems,
            ];

            if (applicationState.MenuDepth > 0)
            {
                items.Add(
                    new MenuItem
                    {
                        Key = "back",
                        Description = "Go back to the previous menu",
                        Command = exitCurrentMenuCommand,
                    });
            }

            return items;
        }
    }
}
