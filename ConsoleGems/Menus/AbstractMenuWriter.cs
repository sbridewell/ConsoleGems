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
        public void WriteMenu(IMenu menu)
        {
            this.WriteTopBorder(menu);
            this.WriteTitleRow(menu);
            this.WriteMenuDescription(menu);
            this.WriteSeparatorLine(menu);
            this.WriteMenuItems(menu);
            this.WriteBottomBorder(menu);
        }

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

        /// <inheritdoc/>
        public abstract void WriteTopBorder(IMenu menu);

        /// <inheritdoc/>
        public abstract void WriteTitleRow(IMenu menu);

        /// <inheritdoc/>
        public abstract void WriteMenuDescription(IMenu menu);

        /// <inheritdoc/>
        public abstract void WriteSeparatorLine(IMenu menu);

        /// <inheritdoc/>
        public abstract void WriteMenuItems(IMenu menu);

        /// <inheritdoc/>
        public abstract void WriteBottomBorder(IMenu menu);
    }
}
