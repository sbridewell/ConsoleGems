// <copyright file="ExitCurrentMenuCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Commands
{
    /// <summary>
    /// Command to exit from the current menu and return to the previous menu.
    /// </summary>
    public class ExitCurrentMenuCommand(ApplicationState applicationState)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            applicationState.ExitCurrentMenu = true;
            applicationState.MenuDepth--;
        }
    }
}
