// <copyright file="ShowMenuCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Commands
{
    /// <summary>
    /// Unit tests for the <see cref="ShowMenuCommand"/> class.
    /// </summary>
    public class ShowMenuCommandTest
    {
        private readonly Mock<IAutoCompleter> mockAutoCompleter = new ();
        private readonly Mock<IMenuWriter> mockConsoleMenuWriter = new ();
        private readonly Mock<IConsole> mockConsole = new ();
        private readonly Mock<ICommand> mockCommand1 = new ();
        private readonly ApplicationState applicationState = new ();
        private readonly ThrowExceptionCommand throwExceptionCommand = new ();
        private ExitCurrentMenuCommand? exitCurrentMenuCommand;
        private MenuForTesting? menu;

        /// <summary>
        /// Happy path test for the Execute method.
        /// </summary>
        [Fact]
        public void Execute_WritesMenuCallsAutoCompleterAndExecutesSelectedCommand()
        {
            // Arrange
            this.Setup();
            this.mockAutoCompleter
                .SetupSequence(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns("mock1")
                .Returns("back");
            var command = this.InstantiateCommand();
            var expectedSuggestions = this.menu!.MenuItems.Select(item => item.Key).ToList();
            var expectedPrompt = "Choose an option: ";

            // Act
            command.Execute();

            // Assert
            this.applicationState.MenuDepth.Should().Be(0);
            this.mockConsoleMenuWriter.Verify(writer => writer.WriteMenu(this.menu), Times.Exactly(2));
            this.mockAutoCompleter.Verify(ac => ac.ReadLine(expectedSuggestions, expectedPrompt), Times.Exactly(2));
            this.mockConsole.Verify(m => m.WriteLine(It.IsAny<string>(), ConsoleOutputType.Error), Times.Never);
            this.mockCommand1.Verify(command => command.Execute(), Times.Once);
        }

        /// <summary>
        /// Tests that if an invalid menu option is chosen the first time, an error message
        /// is displayed and the menu is displayed again.
        /// </summary>
        [Fact]
        public void Execute_InvalidOptionSelected_WritesErrorAndRepeatsMenu()
        {
            // Arrange
            this.Setup();
            this.mockAutoCompleter
                .SetupSequence(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns("invalid")
                .Returns("back");
            var command = this.InstantiateCommand();
            var expectedSuggestions = this.menu!.MenuItems.Select(item => item.Key).ToList();
            var expectedPrompt = "Choose an option: ";
            var expectedError = "Sorry, 'invalid' is not an option in this menu, try again!";

            // Act
            command.Execute();

            // Assert
            this.applicationState.MenuDepth.Should().Be(0);
            this.mockConsoleMenuWriter.Verify(writer => writer.WriteMenu(this.menu), Times.Exactly(2));
            this.mockAutoCompleter.Verify(ac => ac.ReadLine(expectedSuggestions, expectedPrompt), Times.Exactly(2));
            this.mockConsole.Verify(m => m.WriteLine(expectedError, ConsoleOutputType.Error), Times.Once);
            this.mockCommand1.Verify(command => command.Execute(), Times.Never);
        }

        /// <summary>
        /// Tests that the ExitCurrentMenuCommand exits the menu.
        /// </summary>
        [Fact]
        public void Execute_CallExitCurrentMenuCommand_ExitsMenu()
        {
            // Arrange
            this.Setup();
            this.mockAutoCompleter
                .SetupSequence(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns("back");
            var command = this.InstantiateCommand();
            var expectedSuggestions = this.menu!.MenuItems.Select(item => item.Key).ToList();
            var expectedPrompt = "Choose an option: ";

            // Act
            command.Execute();

            // Assert
            this.applicationState.MenuDepth.Should().Be(0);
            this.mockConsoleMenuWriter.Verify(writer => writer.WriteMenu(this.menu), Times.Once);
            this.mockAutoCompleter.Verify(ac => ac.ReadLine(expectedSuggestions, expectedPrompt), Times.Exactly(1));
            this.mockConsole.Verify(m => m.WriteLine(It.IsAny<string>(), ConsoleOutputType.Error), Times.Never);
            this.mockCommand1.Verify(command => command.Execute(), Times.Never);
        }

        /// <summary>
        /// Tests that when the command executed as a result of the user's selection
        /// throws an unhandled exception, the exception is handled by writing the
        /// details to the console.
        /// </summary>
        [Fact]
        public void Execute_CommandThrowsException_ExceptionMessageIsDisplayed()
        {
            // Arrange
            this.Setup();
            this.mockAutoCompleter
                .SetupSequence(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns("throw")
                .Returns("back");
            var command = this.InstantiateCommand();
            var expectedSuggestions = this.menu!.MenuItems.Select(item => item.Key).ToList();
            var expectedPrompt = "Choose an option: ";

            // Act
            command.Execute();

            // Assert
            this.applicationState.MenuDepth.Should().Be(0);
            this.mockConsoleMenuWriter.Verify(writer => writer.WriteMenu(this.menu), Times.Exactly(2));
            this.mockAutoCompleter.Verify(ac => ac.ReadLine(expectedSuggestions, expectedPrompt), Times.Exactly(2));
            this.mockConsole.Verify(m => m.WriteLine(It.IsAny<string>(), ConsoleOutputType.Error), Times.Once);
            this.mockCommand1.Verify(command => command.Execute(), Times.Never);
        }

        /// <summary>
        /// Tests that the Execute method does nothing if the application state's
        /// ExitProgram property is set to true.
        /// </summary>
        [Fact]
        public void Execute_ExitProgramTrue_ReturnsWithoutDoingAnything()
        {
            // Arrange
            this.Setup();
            this.applicationState.ExitProgram = true;
            var command = this.InstantiateCommand();

            // Act
            command.Execute();

            // Assert
            this.mockConsoleMenuWriter.Verify(writer => writer.WriteMenu(It.IsAny<IMenu>()), Times.Never);
            this.mockAutoCompleter.Verify(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()), Times.Never);
            this.mockCommand1.Verify(command => command.Execute(), Times.Never);
        }

        private void Setup()
        {
            this.applicationState.ExitCurrentMenu = false;
            this.applicationState.MenuDepth = 0;
        }

        private ShowMenuCommand InstantiateCommand()
        {
            this.exitCurrentMenuCommand = new ExitCurrentMenuCommand(this.applicationState);
            this.menu = new MenuForTesting(
                this.mockAutoCompleter.Object,
                this.mockConsoleMenuWriter.Object,
                this.mockConsole.Object,
                this.mockCommand1.Object,
                this.exitCurrentMenuCommand,
                this.throwExceptionCommand,
                this.applicationState);
            this.mockConsoleMenuWriter.Setup(writer => writer.GetAllMenuItems(It.IsAny<IMenu>())).Returns(this.menu.MenuItems);
            var command = new ShowMenuCommand(
                this.menu,
                this.mockAutoCompleter.Object,
                this.mockConsoleMenuWriter.Object,
                this.mockConsole.Object,
                this.applicationState);
            return command;
        }

        private class MenuForTesting(
            IAutoCompleter autoCompleter,
            IMenuWriter consoleMenuWriter,
            IConsole console,
            ICommand command1,
            ICommand exitCurrentMenuCommand,
            ICommand throwExceptionCommand,
            ApplicationState applicationState)
            : IMenu
        {
            public string Title => "Menu for testing";

            public string Description => "A menu which is implemented for the purpose of a unit test.";

            public List<MenuItem> MenuItems =>
            [
                new () { Key = "mock1", Description = "Mock command 1", Command = command1 },
                new () { Key = "throw", Description = "Throw an exception", Command = throwExceptionCommand },
                new () { Key = "back", Description = "Return to previous menu", Command = exitCurrentMenuCommand },
            ];

            public ShowMenuCommand ShowCommand => new (
                this,
                autoCompleter,
                consoleMenuWriter,
                console,
                applicationState);
        }
    }
}
