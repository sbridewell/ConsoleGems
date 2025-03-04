// <copyright file="ThrowExceptionCommandTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Commands.Demo
{
    /// <summary>
    /// Unit tests for the <see cref="ThrowExceptionCommand"/> class.
    /// </summary>
    public class ThrowExceptionCommandTest
    {
        /// <summary>
        /// Tests that the Execute command throws the correct exception.
        /// </summary>
        [Fact]
        public void Execute_ThrowsCorrectException()
        {
            // Arrange
            var command = new ThrowExceptionCommand();

            // Act
            var action = () => command.Execute();

            // Assert
            var ex = action.Should().Throw<InvalidOperationException>().Which;
            ex.Message.Should().Be("You wanted to throw an exception, so here it is :-)");
        }
    }
}
