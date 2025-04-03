// <copyright file="SaveAsciiArtSettingsCommand.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using System.IO;
    using System.Text.Json;
    using Sde.ConsoleGems.Commands;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Menus;

    /// <summary>
    /// Serializes the current ASCII art settings to JSON, writes it
    /// to a file and displays the full path to the file.
    /// </summary>
    public class SaveAsciiArtSettingsCommand(
        IConsole console,
        AsciiArtSettings asciiArtSettings)
        : ICommand
    {
        /// <inheritdoc/>
        public void Execute()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var json = JsonSerializer.Serialize(asciiArtSettings, options);
            var fullPath = Path.GetFullPath("AsciiArtSettings.json");
            File.WriteAllText(fullPath, json);
            console.WriteLine($"ASCII art settings written to {fullPath}");
        }
    }
}
