// <copyright file="SelectAFolderCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Commands
{
    /// <summary>
    /// Command to prompt the user to select a subdirectory
    /// of the current working directory.
    /// </summary>
    public class SelectAFolderCommand(
        IDirectoryPrompter directoryPrompter,
        ApplicationState applicationState,
        IConsole console)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            var directory = directoryPrompter.Prompt(
                applicationState.WorkingDirectory,
                "Select a folder: ",
                mustAlreadyExist: true);
            console.WriteLine($"Selected folder: '{directory}'");
        }
    }
}
