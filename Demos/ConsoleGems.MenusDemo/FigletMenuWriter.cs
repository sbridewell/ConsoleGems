// <copyright file="FigletMenuWriter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.MenusDemo
{
    using System.Text;
    using Sde.ConsoleGems.Commands;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Menus;
    using WenceyWang.FIGlet;

    /// <summary>
    /// A demonstration implementation of <see cref="IMenuWriter"/>
    /// which uses the
    /// <see href="http://nuget.org/packages/FIGlet.Net/">FIGlet.Net</see>
    /// package to render the menu title as ASCII art.
    /// </summary>
    public class FigletMenuWriter(
        IConsole console,
        ISharedMenuItemsProvider sharedMenuItemsProvider,
        IGlobalMenuItemsProvider globalMenuItemsProvider,
        ExitCurrentMenuCommand exitCurrentMenuCommand,
        AsciiArtSettings asciiArtSettings,
        ApplicationState applicationState)
        : MenuWriter(
            sharedMenuItemsProvider,
            globalMenuItemsProvider,
            textJustifier,
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
            console,
            asciiArtSettings,
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
            exitCurrentMenuCommand,
            applicationState)
    {
        /// <inheritdoc/>
        public override void WriteTitleRow(IMenu menu)
        {
            var title = new AsciiArt(menu.Title);
            var titleLines = title.ToString().Split(Environment.NewLine);
            foreach (var titleLine in titleLines)
            {
                console.Write(asciiArtSettings.OuterBorderVertical, ConsoleOutputType.MenuBody);
                console.Write(titleLine, ConsoleOutputType.MenuHeader);
                console.Write(new string(' ', console.WindowWidth - 2 - titleLine.Length), ConsoleOutputType.MenuHeader);
                console.WriteLine(asciiArtSettings.OuterBorderVertical, ConsoleOutputType.MenuBody);
            }
        }
    }
}
