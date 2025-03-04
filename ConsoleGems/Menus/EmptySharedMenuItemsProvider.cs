// <copyright file="EmptySharedMenuItemsProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <summary>
    /// <see cref="ISharedMenuItemsProvider"/> implementation which
    /// returns no menu items.
    /// This is the default implementation, for use by consumers who
    /// do not require menu items which are shared across all menus.
    /// </summary>
    public class EmptySharedMenuItemsProvider : ISharedMenuItemsProvider
    {
        /// <inheritdoc/>
        public List<MenuItem> MenuItems => new ();
    }
}
