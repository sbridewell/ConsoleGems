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
        AsciiArtSettings asciiArtSettings,
        ExitCurrentMenuCommand exitCurrentMenuCommand,
        ApplicationState applicationState)
        : AbstractMenuWriter(
            sharedMenuItemsProvider,
            globalMenuItemsProvider,
            exitCurrentMenuCommand,
            applicationState)
    {
        /// <inheritdoc/>
        public override void WriteTopBorder(IMenu menu)
        {
            console.Write(asciiArtSettings.OuterBorderTopLeft, ConsoleOutputType.MenuBody);
            console.Write(
                new string(asciiArtSettings.OuterBorderHorizontal, console.WindowWidth - 2),
                ConsoleOutputType.MenuBody);
            console.WriteLine(asciiArtSettings.OuterBorderTopRight, ConsoleOutputType.MenuBody);
        }

        /// <inheritdoc/>
        public override void WriteTitleRow(IMenu menu)
        {
            textJustifier.Justify(
                menu.Title,
                TextJustification.Centre,
                console.WindowWidth - 2);
            console.Write(asciiArtSettings.OuterBorderVertical, ConsoleOutputType.MenuBody);
            console.Write(textJustifier.JustifiedText, ConsoleOutputType.MenuHeader);
            console.WriteLine(asciiArtSettings.OuterBorderVertical, ConsoleOutputType.MenuBody);
        }

        /// <inheritdoc/>
        public override void WriteMenuDescription(IMenu menu)
        {
            textJustifier.Justify(menu.Description, TextJustification.Centre, console.WindowWidth - 2);
            var descriptionBlock = textJustifier.JustifiedTextBlock;
            foreach (var descriptionLine in descriptionBlock.Lines)
            {
                console.Write(asciiArtSettings.OuterBorderVertical, ConsoleOutputType.MenuBody);
                console.Write(descriptionLine, ConsoleOutputType.MenuBody);
                console.WriteLine(asciiArtSettings.OuterBorderVertical, ConsoleOutputType.MenuBody);
            }
        }

        /// <inheritdoc/>
        public override void WriteSeparatorLine(IMenu menu)
        {
            var maxKeyLength = this.GetMaxKeyLength(menu);
            console.Write(asciiArtSettings.OuterInnerJoinLeft, ConsoleOutputType.MenuBody);
            console.Write(
                new string(asciiArtSettings.InnerBorderHorizontal, maxKeyLength),
                ConsoleOutputType.MenuBody);
            console.Write(asciiArtSettings.InnerBorderJoinTop, ConsoleOutputType.MenuBody);
            console.Write(
                new string(asciiArtSettings.InnerBorderHorizontal, console.WindowWidth - maxKeyLength - 3),
                ConsoleOutputType.MenuBody);
            console.WriteLine(asciiArtSettings.OuterInnerJoinRight, ConsoleOutputType.MenuBody);
        }

        /// <inheritdoc/>
        public override void WriteMenuItems(IMenu menu)
        {
            var items = this.GetAllMenuItems(menu);
            var maxKeyLength = this.GetMaxKeyLength(menu);
            foreach (var menuItem in items)
            {
                textJustifier.Justify(menuItem.Key, TextJustification.Right, maxKeyLength);
                var keyBlock = textJustifier.JustifiedTextBlock;
                textJustifier.Justify(
                    menuItem.Description,
                    TextJustification.Left,
                    console.WindowWidth - maxKeyLength - 3);
                var descriptionBlock = textJustifier.JustifiedTextBlock;
                var menuItemHeight = Math.Max(keyBlock.Height, descriptionBlock.Height);
                var separatorBlock = new TextBlock(1);
                separatorBlock.InsertText(new string(asciiArtSettings.InnerBorderVertical, menuItemHeight));
                var outerBorderBlock = new TextBlock(1);
                outerBorderBlock.InsertText(new string(asciiArtSettings.OuterBorderVertical, menuItemHeight));
                var menuItemBlock = new TextBlock(console.WindowWidth);
                menuItemBlock.InsertBlock(outerBorderBlock, new ConsolePoint(0, 0));
                menuItemBlock.InsertBlock(keyBlock, new ConsolePoint(1, 0));
                menuItemBlock.InsertBlock(separatorBlock, new ConsolePoint(maxKeyLength + 1, 0));
                menuItemBlock.InsertBlock(descriptionBlock, new ConsolePoint(maxKeyLength + 2, 0));
                menuItemBlock.InsertBlock(outerBorderBlock, new ConsolePoint(console.WindowWidth - 1, 0));
                foreach (var menuItemLine in menuItemBlock.Lines)
                {
                    console.WriteLine(menuItemLine, ConsoleOutputType.MenuBody);
                }
            }
        }

        /// <inheritdoc/>
        public override void WriteBottomBorder(IMenu menu)
        {
            var maxKeyLength = this.GetMaxKeyLength(menu);
            console.Write(asciiArtSettings.OuterBorderBottomLeft, ConsoleOutputType.MenuBody);
            console.Write(
                new string(asciiArtSettings.OuterBorderHorizontal, maxKeyLength),
                ConsoleOutputType.MenuBody);
            console.Write(asciiArtSettings.OuterInnerJoinBottom, ConsoleOutputType.MenuBody);
            console.Write(
                new string(asciiArtSettings.OuterBorderHorizontal, console.WindowWidth - maxKeyLength - 3),
                ConsoleOutputType.MenuBody);
            console.WriteLine(asciiArtSettings.OuterBorderBottomRight, ConsoleOutputType.MenuBody);
        }

        private int GetMaxKeyLength(IMenu menu)
        {
            return this.GetAllMenuItems(menu).Max(i => i.Key.Length);
        }
    }
}
