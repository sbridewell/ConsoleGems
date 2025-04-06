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
            // Arramge
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

        /// <summary>
        /// Tests that the correct exception is thrown when the Justify
        /// method is passed null text.
        /// </summary>
        [Fact]
        public void Justify_NullText_Throws()
        {
            // Arrange

            // Act
            var action = () => MenuWriterForTesting.CallJustify(null!, MenuWriter.TextJustification.None, 100);

            // Assert
            var ex = action.Should().ThrowExactly<ArgumentNullException>().Which;
            ex.ParamName.Should().Be("text");
        }

        /// <summary>
        /// Tests that when the supplied justification is none, leading and
        /// trailing whitespace is trimmed and the resulting text is left
        /// justified with no trailing whitespace.
        /// </summary>
        [Fact]
        public void Justify_NoJustification_TrimsAndDoesNotJustify()
        {
            // Arrange
            const string text = "  foo ";
            const int availableWidth = 20;

            // Act
            var justifiedText = MenuWriterForTesting.CallJustify(text, MenuWriter.TextJustification.None, availableWidth);

            // Assert
            justifiedText.Should().Be("foo");
        }

        /// <summary>
        /// Tests that when the supplied justification is left, leading and
        /// trailing whitespace is trimmed and the resulting text is left
        /// justified and right padded with spaces to the required width.
        /// </summary>
        [Fact]
        public void Justify_LeftJustification_TrimsAndJustifiesLeft()
        {
            // Arrange
            const string text = "  foo ";
            const int availableWidth = 20;

            // Act
            var justifiedText = MenuWriterForTesting.CallJustify(text, MenuWriter.TextJustification.Left, availableWidth);

            // Assert
            justifiedText.Should().Be("foo                 ");
        }

        /// <summary>
        /// Tests that when the supplied justification is centre, leading
        /// and trailing whitespace is trimmed and the resulting text is
        /// centre justified and right padded with spaces to the required
        /// width.
        /// </summary>
        [Fact]
        public void Justify_CentreJustification_TrimsAndJustifiesCentre()
        {
            // Arrange
            const string text = "  foo ";
            const int availableWidth = 20;

            // Act
            var justifiedText = MenuWriterForTesting.CallJustify(text, MenuWriter.TextJustification.Centre, availableWidth);

            // Assert
            justifiedText.Should().Be("        foo         ");
        }

        /// <summary>
        /// Tests that when the supplied justification is right, leading and
        /// trailing whitespace is trimmed and the resulting text is right
        /// justified.
        /// </summary>
        [Fact]
        public void Justify_RightJustification_TrimsAndJustifiesRight()
        {
            // Arrange
            const string text = "  foo ";
            const int availableWidth = 20;

            // Act
            var justifiedText = MenuWriterForTesting.CallJustify(text, MenuWriter.TextJustification.Right, availableWidth);

            // Assert
            justifiedText.Should().Be("                 foo");
        }

        /// <summary>
        /// Tests that when the supplied justification is not a member of the
        /// <see cref="AbstractMenuWriter.TextJustification"/> enum, the correct
        /// exception is thrown.
        /// </summary>
        [Fact]
        public void Justify_InvalidJustification_Throws()
        {
            // Arrange
            const string text = "  foo ";
            const int availableWidth = 20;

            // Act
            var action = () => MenuWriterForTesting.CallJustify(text, (MenuWriter.TextJustification)(-1), availableWidth);

            // Assert
            var ex = action.Should().ThrowExactly<ArgumentOutOfRangeException>().Which;
            ex.Message.Should().Contain($"The supplied value is not a member of the {nameof(MenuWriter.TextJustification)} enum.");
            ex.ParamName.Should().Be("justification");
        }

        private MenuWriterForTesting CreateWriter()
        {
            this.mockSharedMenuItemsProvider.Setup(x => x.MenuItems).Returns([]);
            this.mockGlobalMenuItemsProvider.Setup(x => x.MenuItems).Returns([]);
            this.mockConsole.Setup(c => c.WindowWidth).Returns(120);
            var exitCurrentMenuCommand = new ExitCurrentMenuCommand(this.applicationState);
            return new MenuWriterForTesting(
                this.mockSharedMenuItemsProvider.Object,
                this.mockGlobalMenuItemsProvider.Object,
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

        // TODO: #10 move text justification into a separate class and remove the need for MenuWriterForTesting

        /// <summary>
        /// Class which allows unit tests to call the
        /// <see cref="AbstractMenuWriter.Justify(string, MenuWriter.TextJustification, int)"/>
        /// method.
        /// </summary>
        private class MenuWriterForTesting(
            ISharedMenuItemsProvider sharedMenuItemsProvider,
            IGlobalMenuItemsProvider globalMenuItemsProvider,
            IConsole console,
            ExitCurrentMenuCommand exitCurrentMenuCommand,
            ApplicationState applicationState)
            : MenuWriter(
                sharedMenuItemsProvider,
                globalMenuItemsProvider,
                console,
                exitCurrentMenuCommand,
                applicationState)
        {
            /// <summary>
            /// Calls the Justify method.
            /// </summary>
            /// <param name="text">The text to justify.</param>
            /// <param name="justification">The justification to apply.</param>
            /// <param name="availableWidth">The width to justify the text within.</param>
            /// <returns>The justified text.</returns>
            public static string CallJustify(string text, TextJustification justification, int availableWidth)
            {
                return Justify(text, justification, availableWidth);
            }
        }
    }
}
