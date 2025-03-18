// <copyright file="SelectAFileCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Commands.Demo
{
    /// <summary>
    /// Command which prompts the user to select a file in the current
    /// working directory.
    /// </summary>
    public class SelectAFileCommand(
        IFilePrompter filePrompter,
        IConsole console,
        ApplicationState applicationState)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            var filename = filePrompter.Prompt(
                applicationState.WorkingDirectory,
                "Enter filename: ",
                mustAlreadyExist: true);
            console.WriteLine($"Selected file: '{filename}'");
        }
    }
}
