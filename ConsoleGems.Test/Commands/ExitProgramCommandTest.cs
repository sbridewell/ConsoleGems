// <copyright file="ExitProgramCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Commands
{
    /// <summary>
    /// Tests for the <see cref="ExitProgramCommand"/> class.
    /// </summary>
    public class ExitProgramCommandTest
    {
        /// <summary>
        /// Tests that the Execute method displays the correct console output
        /// and exits the program.
        /// </summary>
        [Fact]
        public void Execute_DisplaysCorrectConsoleOutputAndExits()
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var applicationState = new ApplicationState();
            var command = new ExitProgramCommand(mockConsole.Object, applicationState);

            // Act
            command.Execute();

            // Assert
            mockConsole.Verify(c => c.WriteLine("Bye!", ConsoleOutputType.Default), Times.Once);
            applicationState.ExitCurrentMenu.Should().BeTrue();
        }
    }
}
