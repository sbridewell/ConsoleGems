// <copyright file="MenuItem.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <summary>
    /// A single item in a menu.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MenuItem
    {
        /// <summary>
        /// Gets or sets the key entered by the user to select the menu item.
        /// </summary>
        required public string Key { get; set; }

        /// <summary>
        /// Gets or sets the description of the menu item.
        /// </summary>
        required public string Description { get; set; }

        /// <summary>
        /// Gets or  sets the command to execute when the menu item is selected.
        /// </summary>
        required public ICommand Command { get; set; }
    }
}
