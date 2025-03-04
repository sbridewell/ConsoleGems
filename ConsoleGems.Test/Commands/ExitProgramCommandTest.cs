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
        [Fact(Skip = "Test doesn't finish because Execute calls Environment.Exit(0)")]
        public void Execute_DisplaysCorrectConsoleOutputAndExits()
        {
            // Arrange
            var mockConsole = new Mock<IConsole>();
            var command = new ExitProgramCommand(mockConsole.Object);

            // Act
            command.Execute();

            // Assert
            mockConsole.Verify(c => c.WriteLine("Bye!", ConsoleOutputType.Default), Times.Once);
        }
    }
}
