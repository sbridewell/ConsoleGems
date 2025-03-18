// <copyright file="ExitCurrentMenuCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Commands
{
    /// <summary>
    /// Unit tests for the <see cref="ExitCurrentMenuCommand"/> class.
    /// </summary>
    public class ExitCurrentMenuCommandTest
    {
        /// <summary>
        /// Tests that the Execute method sets the following properties of
        /// <see cref="ApplicationState" />:
        /// <list type="bullet">
        /// <item>Sets ExitCurrentMenu to true.</item>
        /// <item>Decrements MenuDepth by 1.</item>
        /// </list>
        /// </summary>
        [Fact]
        public void Execute_SetsExitCurrentMenuToTrueAndDecrementsMenuDepth()
        {
            // Arrange
            var applicationState = new ApplicationState();
            applicationState.MenuDepth = 3;
            applicationState.ExitCurrentMenu = false;
            applicationState.ExitProgram = false;
            var command = new ExitCurrentMenuCommand(applicationState);

            // Act
            command.Execute();

            // Assert
            applicationState.MenuDepth.Should().Be(2);
            applicationState.ExitCurrentMenu.Should().BeTrue();
            applicationState.ExitProgram.Should().BeFalse();
        }
    }
}
