// <copyright file="IMenuItemsProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <summary>
    /// Provider for the menu items which appear in a single menu.
    /// </summary>
    public interface IMenuItemsProvider
    {
        /// <summary>
        /// Gets the menu items..
        /// </summary>
        List<MenuItem> MenuItems { get; }
    }
}
