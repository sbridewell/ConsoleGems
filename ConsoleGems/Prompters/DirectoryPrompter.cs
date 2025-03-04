// <copyright file="DirectoryPrompter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Prompters
{
    /// <summary>
    /// A prompter which asks the user to enter the name of an existing directory.
    /// </summary>
    public class DirectoryPrompter(
        IAutoCompleter autoCompleter,
        IConsoleErrorWriter consoleErrorWriter)
        : IDirectoryPrompter
    {
        /// <inheritdoc/>
        public DirectoryInfo Prompt(
            DirectoryInfo rootDirectory,
            string prompt,
            bool mustAlreadyExist)
        {
            DirectoryInfo returnValue;
            while (true)
            {
                var directories = Directory.GetDirectories(rootDirectory.FullName)
                    .Prepend("..") // allow navigation to parent directory
                    .ToList();
                for (var i = 1; i < directories.Count; i++)
                {
                    // Get just the directory name, not the full path, because
                    // what we're making here is a list of suggested directory
                    // names that the user can choose from.
                    directories[i] = new DirectoryInfo(directories[i]).Name;
                }

                var directoryName = autoCompleter.ReadLine(directories, prompt);
                returnValue = new DirectoryInfo(Path.Combine(rootDirectory.FullName, directoryName));
                if (Directory.Exists(returnValue.FullName) || !mustAlreadyExist)
                {
                    break;
                }

                consoleErrorWriter.WriteError($"The directory '{directoryName}' does not exist in the directory '{rootDirectory.FullName}'.");
            }

            return returnValue;
        }
    }

#pragma warning disable SA1201 // Elements should appear in the correct order
    /// <summary>
    /// Interface for a prompter which asks the user to enter the name of an existing directory.
    /// </summary>
    public interface IDirectoryPrompter
    {
        /// <summary>
        /// Prompts the user to enter the name of an existing directory.
        /// </summary>
        /// <param name="rootDirectory">
        /// The directory from which the user should select a child directory.
        /// </param>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <param name="mustAlreadyExist">
        /// True to repeatedly prompt until the user has entered the name
        /// of a directory which already exists.
        /// False to accept any directory name, regardless of whether it exists.
        /// </param>
        /// <returns>The name of the selected directory.</returns>
        public DirectoryInfo Prompt(DirectoryInfo rootDirectory, string prompt, bool mustAlreadyExist);
    }
#pragma warning restore SA1201 // Elements should appear in the correct order
}
