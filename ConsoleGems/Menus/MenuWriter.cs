// <copyright file="MenuWriter.cs" company="Simon Bridewell">
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
            textJustifier.Justify(menu.Title, TextJustification.Centre, console.WindowWidth);
            var title = textJustifier.JustifiedText;
            console.WriteLine(title, ConsoleOutputType.MenuHeader);
            var items = this.GetAllMenuItems(menu);
            var maxKeyWidth = items.Max(i => i.Key.Length);
            var menuBlock = new TextBlock(console.WindowWidth);
            foreach (var menuItem in items)
            {
                textJustifier.Justify(menuItem.Key, TextJustification.Right, maxKeyWidth);
                var keyBlock = textJustifier.JustifiedTextBlock;
                var separatorBlock = new TextBlock(3);
                separatorBlock.InsertText(" - ");
                textJustifier.Justify(menuItem.Description, TextJustification.Left, console.WindowWidth - maxKeyWidth - 3);
                var descriptionBlock = textJustifier.JustifiedTextBlock;
                var nextYPos = menuBlock.Height;
                menuBlock.InsertBlock(keyBlock, new ConsolePoint(0, nextYPos));
                menuBlock.InsertBlock(separatorBlock, new ConsolePoint(maxKeyWidth, nextYPos));
                menuBlock.InsertBlock(descriptionBlock, new ConsolePoint(maxKeyWidth + 3, nextYPos));
            }

            console.Write(menuBlock.ToString(), ConsoleOutputType.MenuBody);
        }
    }
}
