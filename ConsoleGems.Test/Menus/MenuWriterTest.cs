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
        private readonly List<MenuItem> menuItems = new ()
        {
            new () { Key = "1", Description = "Item 1", Command = new Mock<ICommand>().Object },
            new () { Key = "2abc", Description = "Item 2", Command = new Mock<ICommand>().Object },
        };

        /// <summary>
        /// Tests that the <see cref="MenuWriter.WriteMenu(IMenu)"/>
        /// method writes the menu correctly.
        /// </summary>
        [Fact]
        public void WriteMenu_DisplaysMenu()
        {
            // Arrange
            this.applicationState.MenuDepth = 0;
            var menu = new MenuForTesting(this.menuItems);
            var consoleMenuWriter = this.CreateWriter();
            this.mockTextJustifier.Setup(m => m.JustifiedTextBlock).Returns(new TextBlock(10));

            // Act
            consoleMenuWriter.WriteMenu(menu);

            // Assert
            this.mockTextJustifier.Verify(m => m.Justify("Test menu", TextJustification.Centre, 120, ' '), Times.Once);
            this.mockTextJustifier.Verify(m => m.Justify("1", TextJustification.Right, 4, ' '), Times.Once);
            this.mockTextJustifier.Verify(m => m.Justify("Item 1", TextJustification.Left, 113, ' '), Times.Once);
            this.mockTextJustifier.Verify(m => m.Justify("2abc", TextJustification.Right, 4, ' '), Times.Once);
            this.mockTextJustifier.Verify(m => m.Justify("Item 2", TextJustification.Left, 113, ' '), Times.Once);
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
            this.mockTextJustifier.Verify(m => m.Justify("Test menu", TextJustification.Centre, 120, ' '), Times.Once);
            this.mockTextJustifier.Verify(m => m.Justify("1", TextJustification.Right, 4, ' '), Times.Once);
            this.mockTextJustifier.Verify(m => m.Justify("Item 1", TextJustification.Left, 113, ' '), Times.Once);
            this.mockTextJustifier.Verify(m => m.Justify("2abc", TextJustification.Right, 4, ' '), Times.Once);
            this.mockTextJustifier.Verify(m => m.Justify("Item 2", TextJustification.Left, 113, ' '), Times.Once);
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
