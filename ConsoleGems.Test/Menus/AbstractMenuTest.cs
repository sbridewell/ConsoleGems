// <copyright file="AbstractMenuTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Menus
{
    /// <summary>
    /// Tests for the <see cref="AbstractMenu"/> class.
    /// </summary>
    public class AbstractMenuTest
    {
        private readonly Mock<IAutoCompleter> mockAutoCompleter = new ();
        private readonly Mock<IConsole> mockConsole = new ();
        private readonly Mock<IMenuWriter> mockMenuWriter = new ();
        private readonly Mock<ICommand> mockCommand = new ();
        private readonly ApplicationState applicationState = new ();

        /// <summary>
        /// Tests that the ShowCommand property returns a
        /// <see cref="ShowMenuCommand"/>.
        /// </summary>
        [Fact]
        public void ShowCommand_ReturnsShowMenuCommand()
        {
            // Arrange
            var menu = new MenuForTesting(
                this.mockAutoCompleter.Object,
                this.mockMenuWriter.Object,
                this.mockConsole.Object,
                this.mockCommand.Object,
                this.mockCommand.Object,
                this.applicationState);

            // Act
            var showCommand = menu.ShowCommand;

            // Assert
            showCommand.Should().BeOfType<ShowMenuCommand>();
        }
    }
}
