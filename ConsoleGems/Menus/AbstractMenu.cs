// <copyright file="AbstractMenu.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <summary>
    /// Abstract base class for a menu.
    /// </summary>
    public abstract class AbstractMenu(
        IAutoCompleter autoCompleter,
        IMenuWriter menuWriter,
        IConsoleErrorWriter consoleErrorWriter,
        ApplicationState applicationState)
        : IMenu
    {
        /// <inheritdoc/>
        public abstract string Title { get; }

        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <inheritdoc/>
        public abstract List<MenuItem> MenuItems { get; }

        /// <inheritdoc/>
        public virtual ShowMenuCommand ShowCommand => new (
            this,
            autoCompleter,
            menuWriter,
            consoleErrorWriter,
            applicationState);
    }
}
