// <copyright file="SharedMenuItemsProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using System.Collections.Generic;
    using Sde.ConsoleGems.Menus;

    /// <inheritdoc/>
    public class SharedMenuItemsProvider(GreetingCommand greetingCommand)
        : ISharedMenuItemsProvider
    {
        /// <inheritdoc/>
        public List<MenuItem> MenuItems =>
        [
            new () { Key = "greet", Description = "Display a greeting (this is a shared command)", Command = greetingCommand },
        ];
    }
}
