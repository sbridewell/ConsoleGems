// <copyright file="GlobalMenuItemsProvider.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public class GlobalMenuItemsProvider(
        ExitProgramCommand exitProgramCommand)
        : IGlobalMenuItemsProvider
    {
        /// <inheritdoc/>
        public List<MenuItem> MenuItems =>
        [
            new MenuItem { Key = "exit", Description = "Exit the program", Command = exitProgramCommand, },
        ];
    }
}
