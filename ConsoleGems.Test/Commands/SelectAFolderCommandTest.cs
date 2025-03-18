// <copyright file="SelectAFolderCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Commands
{
    /// <summary>
    /// Unit tests for the <see cref="SelectAFolderCommand"/> class.
    /// </summary>
    public class SelectAFolderCommandTest
    {
        /// <summary>
        /// Tests that the Execute command passes the correct values to the directory prompter
        /// and writes the selected directory name to the console.
        /// </summary>
        [Fact]
        public void Execute_PassesCorrectValuesToFilePrompterAndWritesResultToConsole()
        {
            // Arrange
            var applicationState = new ApplicationState();
            var userSelection = new DirectoryInfo("myfolder");
            var expectedPrompt = "Select a folder: ";
            var expectedOutput = $"Selected folder: '{userSelection}'";
            var mockConsole = new Mock<IConsole>();
            var mockDirectoryPrompter = new Mock<IDirectoryPrompter>();
            mockDirectoryPrompter
                .Setup(fp => fp.Prompt(It.IsAny<DirectoryInfo>(), It.IsAny<string>(), true))
                .Returns(userSelection);
            var command = new SelectAFolderCommand(
                mockDirectoryPrompter.Object,
                applicationState,
                mockConsole.Object);

            // Act
            command.Execute();

            // Assert
            mockDirectoryPrompter.Verify(fp => fp.Prompt(applicationState.WorkingDirectory, expectedPrompt, true), Times.Once);
            mockConsole.Verify(c => c.WriteLine(expectedOutput, ConsoleOutputType.Default), Times.Once);
        }
    }
}
