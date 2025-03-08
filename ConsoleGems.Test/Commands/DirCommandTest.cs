// <copyright file="DirCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Commands
{
    using System.Text;
    using Sde.ConsoleGems.Consoles;
    using Xunit.Abstractions;

    /// <summary>
    /// Tests for the <see cref="DirCommand"/> class.
    /// </summary>
    public class DirCommandTest(ITestOutputHelper output)
        : FileSystemTestBase(output)
    {
        /// <summary>
        /// Tests that the <see cref="DirCommand.Execute"/> method
        /// displays the correct directory listing.
        /// </summary>
        [Fact]
        public void Execute_DisplaysCorrectDirectoryListing()
        {
            // Arrange
            var applicationState = new ApplicationState();
            applicationState.WorkingDirectory = this.WorkingDirectory;
            var mockConsole = new Mock<IConsole>();
            var command = new DirCommand(mockConsole.Object, applicationState);
            var sb = new StringBuilder();
            sb.AppendLine($"Directory of {applicationState.WorkingDirectory}");
            sb.AppendLine();
            sb.AppendLine("      subdir");
            sb.AppendLine("      subdir2");
            sb.AppendLine("65535 file.bmp");
            sb.AppendLine("  127 file.log");
            sb.AppendLine("    1 file.txt");
            sb.AppendLine("   42 file2.txt");
            var expectedListing = sb.ToString();

            // Act
            command.Execute();

            // Assert
            mockConsole.Verify(c => c.WriteLine(expectedListing, ConsoleOutputType.Default), Times.Once);
        }
    }
}
