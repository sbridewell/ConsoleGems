// <copyright file="DemoMenuTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Menus
{
    /// <summary>
    /// Unit tests for the <see cref="DemoMenu"/> class.
    /// </summary>
    /// <remarks>Also covers the <see cref="AbstractMenu"/> class.</remarks>
    public class DemoMenuTest
    {
        private readonly Mock<IAutoCompleter> mockAutoCompleter = new ();
        private readonly Mock<IConsole> mockConsole = new ();
        private readonly Mock<IMenuWriter> mockConsoleMenuWriter = new ();
        private readonly Mock<IFilePrompter> mockFilePrompter = new ();
        private readonly ApplicationState applicationState = new ();
        private GetADrinkCommand? getADrinkCommand;
        private SelectAFileCommand? selectAFileCommand;
        private ThrowExceptionCommand? throwExceptionCommand;

        /// <summary>
        /// Tests that the constructor sets the menu properties correctly.
        /// </summary>
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            this.InstantiateCommands();

            // Act
            var menu = new DemoMenu(
                this.getADrinkCommand!,
                this.selectAFileCommand!,
                this.throwExceptionCommand!,
                this.mockAutoCompleter.Object,
                this.mockConsoleMenuWriter.Object,
                this.mockConsole.Object,
                this.applicationState);

            // Assert
            menu.Title.Should().Be("Demo menu");
            menu.Description.Should().Be("This menu is to demonstrate how to implement a menu");
            menu.MenuItems.Should().HaveCount(3);
            menu.MenuItems[0].Key.Should().Be("drink");
            menu.MenuItems[0].Description.Should().Be("Demo: Get a drink");
            menu.MenuItems[0].Command.Should().Be(this.getADrinkCommand);
            menu.MenuItems[1].Key.Should().Be("file");
            menu.MenuItems[1].Description.Should().Be("Demo: Select a file");
            menu.MenuItems[1].Command.Should().Be(this.selectAFileCommand);
            menu.MenuItems[2].Key.Should().Be("throw");
            menu.MenuItems[2].Description.Should().Be("Demo: throw an error");
            menu.MenuItems[2].Command.Should().Be(this.throwExceptionCommand);
            menu.ShowCommand.Should().BeOfType<ShowMenuCommand>();
        }

        private void InstantiateCommands()
        {
            this.getADrinkCommand = new (this.mockAutoCompleter.Object, this.mockConsole.Object);
            this.selectAFileCommand = new (this.mockFilePrompter.Object, this.mockConsole.Object, this.applicationState);
            this.throwExceptionCommand = new ();
        }
    }
}
