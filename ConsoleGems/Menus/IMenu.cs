// <copyright file="IMenu.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <summary>
    /// Interface for an interactive menu for use in a console application.
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// Gets the menu's title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets a description of the menu.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the menu's items.
        /// </summary>
        List<MenuItem> MenuItems { get; }

        /// <summary>
        /// Gets the command to show the menu.
        /// </summary>
        ShowMenuCommand ShowCommand { get; }
    }
}
