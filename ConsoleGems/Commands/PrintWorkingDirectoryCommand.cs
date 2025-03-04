// <copyright file="PrintWorkingDirectoryCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Commands
{
    /// <summary>
    /// Command which prints the current working directory.
    /// </summary>
    public class PrintWorkingDirectoryCommand(
        IConsole console,
        ApplicationState applicationState)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            console.WriteLine(applicationState.WorkingDirectory.FullName);
        }
    }
}
