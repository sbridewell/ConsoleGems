// <copyright file="SelectAFileCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Commands.Demo
{
    /// <summary>
    /// Unit tests for the <see cref="SelectAFileCommand"/> class.
    /// </summary>
    public class SelectAFileCommandTest
    {
        /// <summary>
        /// Tests that the Execute command passes the correct values to the file prompter
        /// and writes the selected filename to the console.
        /// </summary>
        [Fact]
        public void Execute_PassesCorrectValuesToFilePrompterAndWritesResultToConsole()
        {
            // Arrange
            var applicationState = new ApplicationState();
            var userSelection = new FileInfo("foo.txt");
            var expectedPrompt = "Enter filename: ";
            var expectedOutput = $"Selected file: '{userSelection}'";
            var mockConsole = new Mock<IConsole>();
            var mockFilePrompter = new Mock<IFilePrompter>();
            mockFilePrompter
                .Setup(fp => fp.Prompt(It.IsAny<DirectoryInfo>(), It.IsAny<string>(), true))
                .Returns(userSelection);
            var command = new SelectAFileCommand(mockFilePrompter.Object, mockConsole.Object, applicationState);

            // Act
            command.Execute();

            // Assert
            mockFilePrompter.Verify(fp => fp.Prompt(applicationState.WorkingDirectory, expectedPrompt, true), Times.Once);
            mockConsole.Verify(c => c.WriteLine(expectedOutput, ConsoleOutputType.Default), Times.Once);
        }
    }
}
