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
    /// <see href="http://nuget.org/packages/GIFlet.Net/">FIGlet.Net</see>
    /// package to render the menu title as ASCII art.
    /// </summary>
    public class FigletMenuWriter(
        IConsole console,
        ISharedMenuItemsProvider sharedMenuItemsProvider,
        IGlobalMenuItemsProvider globalMenuItemsProvider,
        ExitCurrentMenuCommand exitCurrentMenuCommand,
        ApplicationState applicationState)
        : AbstractMenuWriter(
            sharedMenuItemsProvider,
            globalMenuItemsProvider,
            exitCurrentMenuCommand,
            applicationState)
    {
        /// <inheritdoc/>
        public override void WriteMenu(IMenu menu)
        {
            var sb = new StringBuilder();
            var title = new AsciiArt(menu.Title);
            console.WriteLine(title.ToString(), ConsoleOutputType.MenuHeader);
            var items = this.GetAllMenuItems(menu);
            var maxKeyWidth = items.Max(i => i.Key.Length);
            foreach (var menuItem in items)
            {
                var keyDisplay = Justify(menuItem.Key, TextJustification.Right, maxKeyWidth);
                var descriptionDisplay = Justify(menuItem.Description, TextJustification.Left, console.WindowWidth - maxKeyWidth - 3);
                console.WriteLine($"{keyDisplay} - {descriptionDisplay}", ConsoleOutputType.MenuBody);
            }

            console.Write(sb.ToString());
        }
    }
}
