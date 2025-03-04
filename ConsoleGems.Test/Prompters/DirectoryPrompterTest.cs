// <copyright file="DirectoryPrompterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Prompters
{
    using Xunit.Abstractions;

    /// <summary>
    /// Unit tests for the <see cref="DirectoryPrompter"/> class.
    /// </summary>
    public class DirectoryPrompterTest(ITestOutputHelper output)
        : FileSystemTestBase(output)
    {
        /// <summary>
        /// Tests that the correct directory name is returned when the
        /// name of a directory which exists is entered.
        /// </summary>
        [Fact]
        public void Prompt_DirectoryExists_ReturnsCorrectDirectoryName()
        {
            // Arrange
            var directoryName = "subdir";
            var autoCompleter = new Mock<IAutoCompleter>();
            var consoleErrorWriter = new Mock<IConsoleErrorWriter>();
            var prompt = "Enter a filename";
            autoCompleter.Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), prompt)).Returns(directoryName);
            var prompter = new DirectoryPrompter(autoCompleter.Object, consoleErrorWriter.Object);

            // Act
            var result = prompter.Prompt(this.WorkingDirectory, prompt, mustAlreadyExist: true);

            // Assert
            result.Name.Should().Be(directoryName);
            result.FullName.Should().Be(Path.Combine(this.WorkingDirectory.FullName, directoryName));
            autoCompleter.Verify(ac => ac.ReadLine(new List<string> { "..", "subdir", "subdir2" }, prompt), Times.Once);
            consoleErrorWriter.Verify(cew => cew.WriteError(It.IsAny<string>()), Times.Never);
            consoleErrorWriter.Verify(cew => cew.WriteException(It.IsAny<Exception>()), Times.Never);
        }

        /// <summary>
        /// Tests that when the user enters a directory name which does not
        /// exist, the prompter displays an error message and prompts again
        /// until the name of a directory which exists is entered.
        /// </summary>
        [Fact]
        public void Prompt_DirectoryDoesNotExist_WritesErrorAndPromptsAgain()
        {
            // Arrange
            var badDirectoryName = "notexist";
            var goodDirectoryName = "subdir";
            var autoCompleter = new Mock<IAutoCompleter>();
            var consoleErrorWriter = new Mock<IConsoleErrorWriter>();
            var prompt = "Enter a filename";
            autoCompleter
                .SetupSequence(ac => ac.ReadLine(It.IsAny<List<string>>(), prompt))
                .Returns(badDirectoryName)
                .Returns(goodDirectoryName);
            var prompter = new DirectoryPrompter(autoCompleter.Object, consoleErrorWriter.Object);

            // Act
            var result = prompter.Prompt(this.WorkingDirectory, prompt, mustAlreadyExist: true);

            // Assert
            result.Name.Should().Be(goodDirectoryName);
            result.FullName.Should().Be(Path.Combine(this.WorkingDirectory.FullName, goodDirectoryName));
            autoCompleter.Verify(ac => ac.ReadLine(new List<string> { "..", "subdir", "subdir2" }, prompt), Times.Exactly(2));
            consoleErrorWriter.Verify(cew => cew.WriteError($"The directory '{badDirectoryName}' does not exist in the directory '{this.WorkingDirectory.FullName}'."), Times.Once);
            consoleErrorWriter.Verify(cew => cew.WriteException(It.IsAny<Exception>()), Times.Never);
        }

        /// <summary>
        /// Tests that the prompter can return the name of a directory which
        /// does not exist if the mustAlreadyExist parameter is set to false.
        /// </summary>
        [Fact]
        public void Prompt_DirectoryDoesNotExistAndMustAlreadyExistIsFalse_DoesNotWriteError()
        {
            // Arrange
            var badDirectoryName = "notexist";
            var autoCompleter = new Mock<IAutoCompleter>();
            var consoleErrorWriter = new Mock<IConsoleErrorWriter>();
            var prompt = "Enter a directory name";
            autoCompleter
                .Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), prompt))
                .Returns(badDirectoryName);
            var prompter = new DirectoryPrompter(autoCompleter.Object, consoleErrorWriter.Object);

            // Act
            var result = prompter.Prompt(this.WorkingDirectory, prompt, mustAlreadyExist: false);

            // Assert
            result.Name.Should().Be(badDirectoryName);
            result.FullName.Should().Be(Path.Combine(this.WorkingDirectory.FullName, badDirectoryName));
            autoCompleter.Verify(ac => ac.ReadLine(new List<string> { "..", "subdir", "subdir2" }, prompt), Times.Once);
            consoleErrorWriter.Verify(cew => cew.WriteError(It.IsAny<string>()), Times.Never);
            consoleErrorWriter.Verify(cew => cew.WriteException(It.IsAny<Exception>()), Times.Never);
        }
    }
}
