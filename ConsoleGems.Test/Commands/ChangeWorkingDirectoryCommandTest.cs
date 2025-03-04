// <copyright file="ChangeWorkingDirectoryCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Commands
{
    /// <summary>
    /// Unit tests for the <see cref="ChangeWorkingDirectoryCommand"/> class.
    /// </summary>
    public class ChangeWorkingDirectoryCommandTest
    {
        /// <summary>
        /// Tests that the Execute method sets the ApplicationState.WorkingDirectory
        /// property to the value entered by the user.
        /// </summary>
        [Fact]
        public void Execute_PromptsForAndSetsNewWorkingDirectory()
        {
            // Arrange
            var applicationState = new ApplicationState();
            var mockDirectoryPrompter = new Mock<IDirectoryPrompter>();
            var mockConsole = new Mock<IConsole>();
            var oldWorkingDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            var newWorkingDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            applicationState.WorkingDirectory = oldWorkingDirectory;
            var command = new ChangeWorkingDirectoryCommand(mockDirectoryPrompter.Object, mockConsole.Object, applicationState);
            mockDirectoryPrompter.Setup(dp => dp.Prompt(It.IsAny<DirectoryInfo>(), It.IsAny<string>(), true))
                .Returns(newWorkingDirectory);

            // Act
            command.Execute();

            // Assert
            applicationState.WorkingDirectory.Should().Be(newWorkingDirectory);
            mockConsole.Verify(
                c => c.WriteLine(
                    $"Working directory is '{oldWorkingDirectory.FullName}'",
                    ConsoleOutputType.Default),
                Times.Once);
            mockConsole.Verify(
                c => c.WriteLine(
                    $"Working directory is now '{newWorkingDirectory.FullName}'",
                    ConsoleOutputType.Default),
                Times.Once);
            mockDirectoryPrompter.Verify(
                dp => dp.Prompt(oldWorkingDirectory, "Enter new working directory: ", true),
                Times.Once);
        }
    }
}
