﻿// <copyright file="MenuWriter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <summary>
    /// Writes a menu to the console in verbose format.
    /// </summary>
    public class MenuWriter(
        ISharedMenuItemsProvider sharedMenuItemsProvider,
        IGlobalMenuItemsProvider globalMenuItemsProvider,
        ITextJustifier textJustifier,
        IConsole console,
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
            textJustifier.Justify(menu.Title, TextJustification.Centre, console.WindowWidth);
            var title = textJustifier.JustifiedText;
            console.WriteLine(title, ConsoleOutputType.MenuHeader);
            var items = this.GetAllMenuItems(menu);
            var maxKeyWidth = items.Max(i => i.Key.Length);
            foreach (var menuItem in items)
            {
                textJustifier.Justify(menuItem.Key, TextJustification.Right, maxKeyWidth);
                var keyDisplay = textJustifier.JustifiedText;
                textJustifier.Justify(menuItem.Description, TextJustification.Left, console.WindowWidth - maxKeyWidth - 3);
                var descriptionDisplay = textJustifier.JustifiedText;
                console.WriteLine($"{keyDisplay} - {descriptionDisplay}", ConsoleOutputType.MenuBody);
            }

            console.Write(sb.ToString());
        }
    }
}
