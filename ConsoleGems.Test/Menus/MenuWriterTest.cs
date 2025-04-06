// <copyright file="MenuWriterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Menus
{
    /// <summary>
    /// Tests for the <see cref="MenuWriter"/> class.
    /// </summary>
    public class MenuWriterTest
    {
        private readonly Mock<ISharedMenuItemsProvider> mockSharedMenuItemsProvider = new ();
        private readonly Mock<IGlobalMenuItemsProvider> mockGlobalMenuItemsProvider = new ();
        private readonly Mock<ITextJustifier> mockTextJustifier = new ();
        private readonly Mock<IConsole> mockConsole = new ();
        private readonly ApplicationState applicationState = new ();
        private readonly AsciiArtSettings asciiArtSettings = new ();
        private readonly List<MenuItem> menuItems = new ()
        {
            new () { Key = "1", Description = "Item 1", Command = new Mock<ICommand>().Object },
            new () { Key = "2abc", Description = "Item 2", Command = new Mock<ICommand>().Object },
        };

        /// <summary>
        /// Tests that the WriteTopBorder method writes the correct output
        /// to the console.
        /// </summary>
        [Fact]
        public void WriteTopBorder_WritesCorrectOutput()
        {
            // Arrange
            this.applicationState.MenuDepth = 0;
            var menu = new MenuForTesting(this.menuItems);
            var menuWriter = this.CreateWriter();

            // Act
            menuWriter.WriteTopBorder(menu);

            // Assert
            this.mockConsole.Verify(m => m.Write(this.asciiArtSettings.OuterBorderTopLeft, ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.Write(new string(this.asciiArtSettings.OuterBorderHorizontal, 118), ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.WriteLine(this.asciiArtSettings.OuterBorderTopRight, ConsoleOutputType.MenuBody), Times.Once);
        }

        /// <summary>
        /// Tests that the WriteTitleRow method writes the correct output
        /// to the console.
        /// </summary>
        [Fact]
        public void WriteTitleRow_WritesCorrectOutput()
        {
            // Arrange
            this.applicationState.MenuDepth = 0;
            var menu = new MenuForTesting(this.menuItems);
            var menuWriter = this.CreateWriter();
            this.mockTextJustifier.Setup(m => m.JustifiedText).Returns("Test menu");

            // Act
            menuWriter.WriteTitleRow(menu);

            // Assert
            this.mockConsole.Verify(m => m.Write(this.asciiArtSettings.OuterBorderVertical, ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.Write("Test menu", ConsoleOutputType.MenuHeader), Times.Once);
            this.mockConsole.Verify(m => m.WriteLine(this.asciiArtSettings.OuterBorderVertical, ConsoleOutputType.MenuBody), Times.Once);
        }

        /// <summary>
        /// Tests that the WriteMenuDescription method writes the correct
        /// output to the console.
        /// </summary>
        [Fact]
        public void WriteMenuDescription_WritesCorrectOutput()
        {
            // Arrange
            this.applicationState.MenuDepth = 0;
            var menu = new MenuForTesting(this.menuItems);
            var menuWriter = this.CreateWriter();
            var textBlock = new TextBlock(10);
            textBlock.InsertText("1234567890abcdefghij");
            this.mockTextJustifier.Setup(m => m.JustifiedTextBlock).Returns(textBlock);

            // Act
            menuWriter.WriteMenuDescription(menu);

            // Assert
            this.mockTextJustifier.Verify(m => m.Justify("A menu for use in unit tests", TextJustification.Centre, 118, ' '), Times.Once);
            this.mockConsole.Verify(m => m.Write(this.asciiArtSettings.OuterBorderVertical, ConsoleOutputType.MenuBody), Times.Exactly(2));
            this.mockConsole.Verify(m => m.Write("1234567890", ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.Write("abcdefghij", ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.WriteLine(this.asciiArtSettings.OuterBorderVertical, ConsoleOutputType.MenuBody), Times.Exactly(2));
        }

        /// <summary>
        /// Tests that the WriteSeparatorLine method writes the correct
        /// output to the console.
        /// </summary>
        [Fact]
        public void WriteSeparatorLine_WritesCorrectOutput()
        {
            // Arrange
            this.applicationState.MenuDepth = 0;
            var menu = new MenuForTesting(this.menuItems);
            var menuWriter = this.CreateWriter();

            // Act
            menuWriter.WriteSeparatorLine(menu);

            // Assert
            this.mockConsole.Verify(m => m.Write(this.asciiArtSettings.OuterInnerJoinLeft, ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.Write(new string(this.asciiArtSettings.InnerBorderHorizontal, 4), ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.Write(this.asciiArtSettings.InnerBorderJoinTop, ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.Write(new string(this.asciiArtSettings.InnerBorderHorizontal, 113), ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.WriteLine(this.asciiArtSettings.OuterInnerJoinRight, ConsoleOutputType.MenuBody), Times.Once);
        }

        /// <summary>
        /// Tests that the WriteMenuItems method writes the correct
        /// output to the console.
        /// </summary>
        [Fact]
        public void WriteMenuItems_WritesCorrectOutput()
        {
            // Arrange
            this.applicationState.MenuDepth = 0;
            var menu = new MenuForTesting(this.menuItems);
            var menuWriter = this.CreateWriter();
            var textBlock1 = new TextBlock(2);
            textBlock1.InsertText(" 1");
            var textBlock2 = new TextBlock(10);
            textBlock2.InsertText("1234567890");
            var textBlock3 = new TextBlock(2);
            textBlock3.InsertText(" 2  ");
            var textBlock4 = new TextBlock(10);
            textBlock4.InsertText("1234567890abcdefghij");
            this.mockTextJustifier.SetupSequence(m => m.JustifiedTextBlock)
                .Returns(textBlock1)
                .Returns(textBlock2)
                .Returns(textBlock3)
                .Returns(textBlock4);

            // Act
            menuWriter.WriteMenuItems(menu);

            // Assert
            this.mockConsole.Verify(
                m => m.WriteLine(
                    "║ 1  │1234567890                                                                                                       ║",
                    ConsoleOutputType.MenuBody),
                Times.Once);
            this.mockConsole.Verify(
                m => m.WriteLine(
                    "║ 2  │1234567890                                                                                                       ║",
                    ConsoleOutputType.MenuBody),
                Times.Once);
            this.mockConsole.Verify(
                m => m.WriteLine(
                    "║    │abcdefghij                                                                                                       ║",
                    ConsoleOutputType.MenuBody),
                Times.Once);
        }

        /// <summary>
        /// Tests that the WriteBottomBorder method writes the correct
        /// output to the console.
        /// </summary>
        [Fact]
        public void WriteBottomBorder_WritesCorrectContent()
        {
            // Arrange
            this.applicationState.MenuDepth = 0;
            var menu = new MenuForTesting(this.menuItems);
            var menuWriter = this.CreateWriter();

            // Act
            menuWriter.WriteBottomBorder(menu);

            // Assert
            this.mockConsole.Verify(m => m.Write(this.asciiArtSettings.OuterBorderBottomLeft, ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.Write(new string(this.asciiArtSettings.OuterBorderHorizontal, 4), ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.Write(this.asciiArtSettings.OuterInnerJoinBottom, ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.Write(new string(this.asciiArtSettings.OuterBorderHorizontal, 113), ConsoleOutputType.MenuBody), Times.Once);
            this.mockConsole.Verify(m => m.WriteLine(this.asciiArtSettings.OuterBorderBottomRight, ConsoleOutputType.MenuBody), Times.Once);
        }

        /// <summary>
        /// Tests that the <see cref="AbstractMenuWriter.GetAllMenuItems(IMenu)"/>
        /// method returns the correct menu items.
        /// </summary>
        [Fact]
        public void GetAllMenuItems_ReturnsMenuItems()
        {
            // Arrange
            this.applicationState.MenuDepth = 0;
            var menu = new MenuForTesting(this.menuItems);
            var consoleMenuWriter = this.CreateWriter();

            // Act
            var actualMenuItems = consoleMenuWriter.GetAllMenuItems(menu);

            // Assert
            actualMenuItems.Should().BeEquivalentTo(this.menuItems);
        }

        /// <summary>
        /// Tests that when the menu depth is greater than zero, the menu
        /// includes a "back" option and the menu is displayed.
        /// </summary>
        [Fact]
        public void WriteMenu_MenuDepthGreaterThanZero_DisplaysMenu()
        {
            // Arrange
            this.applicationState.MenuDepth = 1;
            var menu = new MenuForTesting(this.menuItems);
            var menuWriter = this.CreateWriter();
            this.mockTextJustifier.Setup(m => m.JustifiedTextBlock).Returns(new TextBlock(10));

            // Act
            menuWriter.WriteMenu(menu);

            // Assert
            this.mockTextJustifier.Verify(m => m.Justify("back", TextJustification.Right, 4, ' '), Times.Once);
            this.mockTextJustifier.Verify(m => m.Justify("Go back to the previous menu", TextJustification.Left, 113, ' '), Times.Once);
        }

        /// <summary>
        /// Tests that when the menu depth is greater than zero, the menu
        /// items include a "back" option.
        /// </summary>
        [Fact]
        public void GetAllMenuItems_MenuDepthGreaterThanZero_ReturnsMenuItems()
        {
            // Arrange
            this.applicationState.MenuDepth = 1;
            var expectedMenuItems = this.menuItems.Append(new MenuItem
            {
                Key = "back",
                Description = "Go back to the previous menu",
                Command = new ExitCurrentMenuCommand(this.applicationState),
            });
            var menu = new MenuForTesting(this.menuItems);
            var menuWriter = this.CreateWriter();

            // Act
            var actualMenuItems = menuWriter.GetAllMenuItems(menu);

            // Assert
            actualMenuItems.Should().BeEquivalentTo(expectedMenuItems);
        }

        private MenuWriter CreateWriter()
        {
            this.mockSharedMenuItemsProvider.Setup(x => x.MenuItems).Returns([]);
            this.mockGlobalMenuItemsProvider.Setup(x => x.MenuItems).Returns([]);
            this.mockConsole.Setup(c => c.WindowWidth).Returns(120);
            var exitCurrentMenuCommand = new ExitCurrentMenuCommand(this.applicationState);
            return new MenuWriter(
                this.mockSharedMenuItemsProvider.Object,
                this.mockGlobalMenuItemsProvider.Object,
                this.mockTextJustifier.Object,
                this.mockConsole.Object,
                this.asciiArtSettings,
                exitCurrentMenuCommand,
                this.applicationState);
        }

        private class MenuForTesting(List<MenuItem> menuItems)
            : IMenu
        {
            public string Title => "Test menu";

            public string Description => "A menu for use in unit tests";

            public List<MenuItem> MenuItems => menuItems;

            public ShowMenuCommand ShowCommand => throw new NotImplementedException();
        }
    }
}
