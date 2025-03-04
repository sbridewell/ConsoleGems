// <copyright file="BooleanPrompterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Prompters
{
    /// <summary>
    /// Unit tests for the <see cref="BooleanPrompter"/> class.
    /// </summary>
    public class BooleanPrompterTest
    {
        private readonly Mock<IConsole> mockConsole = new ();
        private readonly Mock<IConsoleErrorWriter> mockConsoleErrorWriter = new ();
        private readonly Mock<IAutoCompleter> mockAutoCompleter = new ();

        /// <summary>
        /// Tests that the Prompt method returns the correct response when the user enters a valid value
        /// and no default value is provided.
        /// </summary>
        /// <param name="userInput">The string entered by the user into the console.</param>
        /// <param name="expectedResult">The expected return value.</param>
        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("True", true)]
        [InlineData("False", false)]
        [InlineData("TRUE", true)]
        [InlineData("FALSE", false)]
        public void Prompt_HappyPathWithoutDefaultValue_ReturnsCorrectResponse(string userInput, bool expectedResult)
        {
            // Arrange
            var prompter = this.InstantiatePrompter();
            var prompt = "Enter true or false: ";
            this.mockAutoCompleter
                .Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns(userInput);

            // Act
            var result = prompter.Prompt(prompt);

            // Assert
            result.Should().Be(expectedResult);
            this.mockAutoCompleter.Verify(
                ac => ac.ReadLine(new List<string> { "true", "false", string.Empty }, prompt),
                Times.Once);
            this.mockConsoleErrorWriter.Verify(ew => ew.WriteError(It.IsAny<string>()), Times.Never);
            this.mockConsoleErrorWriter.Verify(ew => ew.WriteException(It.IsAny<Exception>()), Times.Never);
        }

        /// <summary>
        /// Tests that the Prompt method returns the correct response when the user enters a valid value
        /// and a default value is provided.
        /// </summary>
        /// <param name="userInput">The string entered by the user into the console.</param>
        /// <param name="defaultValue">The default value provided to the prompter.</param>
        /// <param name="expectedResult">The expected return value.</param>
        [Theory]
        [InlineData("true", true, true)]
        [InlineData("false", true, false)]
        [InlineData("true", false, true)]
        [InlineData("false", false, false)]
        [InlineData("", true, true)]
        [InlineData("", false, false)]
        public void Prompt_HappyPathWithDefaultValue_ReturnsCorrectResult(string userInput, bool defaultValue, bool expectedResult)
        {
            // Arrange
            var prompter = this.InstantiatePrompter();
            var prompt = "Enter true or false: ";
            this.mockAutoCompleter
                .Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns(userInput);

            // Act
            var result = prompter.Prompt(prompt, defaultValue);

            // Assert
            result.Should().Be(expectedResult);
            this.mockAutoCompleter.Verify(
                ac => ac.ReadLine(new List<string> { "true", "false", string.Empty }, prompt),
                Times.Once);
            this.mockConsoleErrorWriter.Verify(ew => ew.WriteError(It.IsAny<string>()), Times.Never);
            this.mockConsoleErrorWriter.Verify(ew => ew.WriteException(It.IsAny<Exception>()), Times.Never);
        }

        /// <summary>
        /// Tests that the Prompt method writes the correct error message to the console
        /// when the first user input is invalid and the second user input is valid, and
        /// no default value is provided.
        /// </summary>
        /// <param name="firstUserInput">The first string entered by the user into the console.</param>
        /// <param name="secondUserInput">The second string entered by the user into the console.</param>
        /// <param name="expectedResult">The expected return value.</param>
        [Theory]
        [InlineData("1", "true", true)]
        [InlineData("1", "false", false)]
        public void Prompt_InvalidInputAndNoDefaultValue(string firstUserInput, string secondUserInput, bool expectedResult)
        {
            // Arrange
            var prompter = this.InstantiatePrompter();
            var prompt = "Enter true or false: ";
            this.mockAutoCompleter
                .SetupSequence(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns(firstUserInput)
                .Returns(secondUserInput);

            // Act
            var result = prompter.Prompt(prompt);

            // Assert
            result.Should().Be(expectedResult);
            this.mockAutoCompleter.Verify(
                ac => ac.ReadLine(new List<string> { "true", "false", string.Empty }, prompt),
                Times.Exactly(2));
            this.mockConsoleErrorWriter.Verify(
                ew => ew.WriteError($"{firstUserInput} is not a valid value. Please enter true or false."),
                Times.Once);
            this.mockConsoleErrorWriter.Verify(ew => ew.WriteException(It.IsAny<Exception>()), Times.Never);
        }

        /// <summary>
        /// Tests that the Prompt method writes the correct error message to the console
        /// when the first user input is invalid and the second user input is valid, and
        /// a default value is provided.
        /// </summary>
        /// <param name="firstUserInput">The first string entered by the user into the console.</param>
        /// <param name="secondUserInput">The second string entered by the user into the console.</param>
        /// <param name="defaultValue">The default value provided to the prompter.</param>
        /// <param name="expectedResult">The expected return value.</param>
        [Theory]
        [InlineData("0", "true", true, true)]
        [InlineData("0", "false", false, false)]
        public void Prompt_InvalidInputAndDefaultValue_DisplaysCorrectError(string firstUserInput, string secondUserInput, bool defaultValue, bool expectedResult)
        {
            // Arrange
            var prompter = this.InstantiatePrompter();
            var prompt = "Enter true or false: ";
            this.mockAutoCompleter
                .SetupSequence(ac => ac.ReadLine(It.IsAny<List<string>>(), It.IsAny<string>()))
                .Returns(firstUserInput)
                .Returns(secondUserInput);

            // Act
            var result = prompter.Prompt(prompt, defaultValue);

            // Assert
            result.Should().Be(expectedResult);
            this.mockAutoCompleter.Verify(
                ac => ac.ReadLine(new List<string> { "true", "false", string.Empty }, prompt),
                Times.Exactly(2));
            this.mockConsoleErrorWriter.Verify(
                ew => ew.WriteError($"{firstUserInput} is not a valid value. Please enter true, false or an empty string."),
                Times.Once);
            this.mockConsoleErrorWriter.Verify(ew => ew.WriteException(It.IsAny<Exception>()), Times.Never);
        }

        private BooleanPrompter InstantiatePrompter()
        {
            return new BooleanPrompter(
                this.mockConsoleErrorWriter.Object,
                this.mockAutoCompleter.Object,
                this.mockConsole.Object);
        }
    }
}
