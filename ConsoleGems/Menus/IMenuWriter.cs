// <copyright file="IMenuWriter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <summary>
    /// Interface for writing a menu to the console.
    /// </summary>
    public interface IMenuWriter
    {
        /// <summary>
        /// Writes the menu to the console.
        /// </summary>
        /// <param name="menu">The menu to write.</param>
        void WriteMenu(IMenu menu);

        /// <summary>
        /// Gets all the items in the menu.
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// <returns>
        /// All the items in the menu, including those shared between
        /// all menus in the application and those global to all
        /// applications.
        /// </returns>
        List<MenuItem> GetAllMenuItems(IMenu menu);

        /// <summary>
        /// Writes the top border of the menu.
        /// </summary>
        /// <param name="menu">The menu.</param>
        void WriteTopBorder(IMenu menu);

        /// <summary>
        /// Writes the title row of the menu.
        /// </summary>
        /// <param name="menu">The menu.</param>
        void WriteTitleRow(IMenu menu);

        /// <summary>
        /// Writes the menu's description.
        /// </summary>
        /// <param name="menu">The menu.</param>
        void WriteMenuDescription(IMenu menu);

        /// <summary>
        /// Writes a line to separate the menu title and description
        /// from the menu items.
        /// </summary>
        /// <param name="menu">The menu.</param>
        void WriteSeparatorLine(IMenu menu);

        /// <summary>
        /// Writes the menu items.
        /// </summary>
        /// <param name="menu">The menu.</param>
        void WriteMenuItems(IMenu menu);

        /// <summary>
        /// Writes the bottom border of the menu.
        /// </summary>
        /// <param name="menu">The menu.</param>
        void WriteBottomBorder(IMenu menu);
    }
}
