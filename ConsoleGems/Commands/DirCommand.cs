// <copyright file="DirCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Commands
{
    /// <summary>
    /// Command to display the contents of the current directory.
    /// </summary>
    public class DirCommand(
        IConsole console,
        ApplicationState applicationState)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            var sb = new StringBuilder();
            var workingDirectory = applicationState.WorkingDirectory;
            sb.AppendLine($"Directory of {workingDirectory}");
            sb.AppendLine();
            var directories = Directory.GetDirectories(workingDirectory.FullName);
            var files = Directory.GetFiles(workingDirectory.FullName);
            long maxFileSize = 0;
            var contents = new List<(long, string)>();
            foreach (var directory in directories)
            {
                contents.Add((0, new DirectoryInfo(directory).Name));
            }

            foreach (var file in files)
            {
                var fileSize = new FileInfo(file).Length;
                maxFileSize = Math.Max(maxFileSize, fileSize);
                contents.Add((fileSize, new FileInfo(file).Name));
            }

            var sizeColumnWidth = maxFileSize.ToString().Length;
            foreach (var (size, name) in contents)
            {
                sb.Append((size == 0 ? new string(' ', sizeColumnWidth) : size.ToString()).PadLeft(sizeColumnWidth));
                sb.Append(' ');
                sb.Append(name);
                sb.AppendLine();
            }

            console.WriteLine(sb.ToString());
        }
    }
}
