// <copyright file="ChangeWorkingDirectoryCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Commands
{
    /// <summary>
    /// Command to change the current working directory.
    /// </summary>
    public class ChangeWorkingDirectoryCommand(
        IDirectoryPrompter directoryPrompter,
        IConsole console,
        ApplicationState applicationState)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            console.WriteLine($"Working directory is '{applicationState.WorkingDirectory.FullName}'");
            applicationState.WorkingDirectory = directoryPrompter.Prompt(
                applicationState.WorkingDirectory,
                "Enter new working directory: ",
                mustAlreadyExist: true);
            console.WriteLine($"Working directory is now '{applicationState.WorkingDirectory.FullName}'");
        }
    }
}
