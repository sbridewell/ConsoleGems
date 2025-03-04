// <copyright file="GetADrinkCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Commands.Demo
{
    /// <summary>
    /// Unit tests for the <see cref="GetADrinkCommand"/> class.
    /// </summary>
    public class GetADrinkCommandTest
    {
        /// <summary>
        /// Tests that the Execute method passes the correct values to the
        /// auto completer and writes the result to the console.
        /// </summary>
        [Fact]
        public void Execute_PassesCorrectValuesToAutoCompleterAndWritesResultToConsole()
        {
            // Arrange
            List<string> expectedSuggestions =
            [
                "coffee", "tea", "water", "beer", "wine", "whisky",
            ];
            var expectedPrompt = "What would you like to drink? ";
            var userSelection = "milkshake";
            var expectedOutput = $"Enjoy your {userSelection}!";
            var mockConsole = new Mock<IConsole>();
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            mockAutoCompleter
                .Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns(userSelection);
            var command = new GetADrinkCommand(mockAutoCompleter.Object, mockConsole.Object);

            // Act
            command.Execute();

            // Assert
            mockAutoCompleter.Verify(ac => ac.ReadLine(expectedSuggestions, expectedPrompt), Times.Once);
            mockConsole.Verify(c => c.WriteLine(expectedOutput, ConsoleOutputType.Default), Times.Once);
        }
    }
}
