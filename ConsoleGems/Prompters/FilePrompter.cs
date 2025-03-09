// <copyright file="FilePrompter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Prompters
{
    /// <summary>
    /// Prompter which asks the user to enter the name of an existing file.
    /// </summary>
    public class FilePrompter(
        IAutoCompleter autoCompleter,
        IConsole console)
        : IFilePrompter
    {
        /// <inheritdoc/>
        public FileInfo Prompt(DirectoryInfo rootDirectory, string prompt, bool mustAlreadyExist)
        {
            return this.Prompt(rootDirectory, prompt, mustAlreadyExist, string.Empty);
        }

        /// <inheritdoc/>
        public FileInfo Prompt(
            DirectoryInfo rootDirectory,
            string prompt,
            bool mustAlreadyExist,
            string pattern)
        {
            FileInfo returnValue;
            while (true)
            {
                var filenames = Directory.GetFiles(rootDirectory.FullName, pattern).ToList();
                for (var i = 0; i < filenames.Count; i++)
                {
                    // Get just the file name, not the full path, because
                    // what we're making here is a list of suggested file
                    // names that the user can choose from.
                    filenames[i] = new FileInfo(filenames[i]).Name;
                }

                var filename = autoCompleter.ReadLine(filenames, prompt);
                returnValue = new FileInfo(Path.Combine(rootDirectory.FullName, filename));
                if (File.Exists(returnValue.FullName) || !mustAlreadyExist)
                {
                    break;
                }

                console.WriteLine(
                    $"The file '{filename}' does not exist in the directory '{rootDirectory.FullName}'.",
                    ConsoleOutputType.Error);
            }

            return returnValue;
        }
    }

#pragma warning disable SA1201 // Elements should appear in the correct order
    /// <summary>
    /// Interface for a prompter which asks the user to enter the name of an existing file.
    /// </summary>
    public interface IFilePrompter : IPrompter
    {
        /// <summary>
        /// Prompt the user to enter the name of an existing file.
        /// </summary>
        /// <param name="rootDirectory">The folder from which the user should select a file.</param>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <param name="mustAlreadyExist">
        /// True to repeatedly prompt until the user has entered the name
        /// of a file which already exists.
        /// False to accept any filename, regardless of whether it exists.
        /// </param>
        /// <returns>The name of the selected file.</returns>
        public FileInfo Prompt(DirectoryInfo rootDirectory, string prompt, bool mustAlreadyExist);

        /// <summary>
        /// Prompt the user to enter the name of an existing file.
        /// </summary>
        /// <param name="rootDirectory">The folder from which the user should select a file.</param>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <param name="mustAlreadyExist">
        /// True to repeatedly prompt until the user has entered the name
        /// of a file which already exists.
        /// False to accept any filename, regardless of whether it exists.
        /// </param>
        /// <param name="pattern">
        /// Pattern for filenames to match. If empty, all files are accepted.
        /// </param>
        /// <returns>The name of the selected file.</returns>
        public FileInfo Prompt(DirectoryInfo rootDirectory, string prompt, bool mustAlreadyExist, string pattern);
    }
#pragma warning restore SA1201 // Elements should appear in the correct order
}
