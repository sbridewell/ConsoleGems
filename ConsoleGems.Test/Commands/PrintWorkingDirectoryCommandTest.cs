// <copyright file="PrintWorkingDirectoryCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Commands
{
    /// <summary>
    /// Unit tests for the <see cref="PrintWorkingDirectoryCommand"/> class.
    /// </summary>
    public class PrintWorkingDirectoryCommandTest
    {
        /// <summary>
        /// Tests that the Execute method prints the value of the
        /// <see cref="ApplicationState.WorkingDirectory"/> property.
        /// </summary>
        [Fact]
        public void Execute_PrintsWorkingDirectory()
        {
            // Arrange
            var applicationState = new ApplicationState();
            var mockConsole = new Mock<IConsole>();
            var command = new PrintWorkingDirectoryCommand(mockConsole.Object, applicationState);
            var workingDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            applicationState.WorkingDirectory = workingDirectory;

            // Act
            command.Execute();

            // Assert
            mockConsole.Verify(c => c.WriteLine(workingDirectory.FullName, ConsoleOutputType.Default));
        }
    }
}
