// <copyright file="DirectoryPrompterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Prompters
{
    using Sde.ConsoleGems.Consoles;
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
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var mockConsole = new Mock<IConsole>();
            var prompt = "Enter a filename";
            mockAutoCompleter.Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), prompt)).Returns(directoryName);
            var prompter = new DirectoryPrompter(mockAutoCompleter.Object, mockConsole.Object);

            // Act
            var result = prompter.Prompt(this.WorkingDirectory, prompt, mustAlreadyExist: true);

            // Assert
            result.Name.Should().Be(directoryName);
            result.FullName.Should().Be(Path.Combine(this.WorkingDirectory.FullName, directoryName));
            mockAutoCompleter.Verify(ac => ac.ReadLine(new List<string> { "..", "subdir", "subdir2" }, prompt), Times.Once);
            mockConsole.Verify(c => c.WriteLine(It.IsAny<string>(), ConsoleOutputType.Error), Times.Never);
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
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var mockConsole = new Mock<IConsole>();
            var prompt = "Enter a filename";
            mockAutoCompleter
                .SetupSequence(ac => ac.ReadLine(It.IsAny<List<string>>(), prompt))
                .Returns(badDirectoryName)
                .Returns(goodDirectoryName);
            var prompter = new DirectoryPrompter(mockAutoCompleter.Object, mockConsole.Object);

            // Act
            var result = prompter.Prompt(this.WorkingDirectory, prompt, mustAlreadyExist: true);

            // Assert
            result.Name.Should().Be(goodDirectoryName);
            result.FullName.Should().Be(Path.Combine(this.WorkingDirectory.FullName, goodDirectoryName));
            mockAutoCompleter.Verify(ac => ac.ReadLine(new List<string> { "..", "subdir", "subdir2" }, prompt), Times.Exactly(2));
            mockConsole.Verify(
                c => c.WriteLine(
                    $"The directory '{badDirectoryName}' does not exist in the directory '{this.WorkingDirectory.FullName}'.",
                    ConsoleOutputType.Error),
                Times.Once);
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
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var mockConsole = new Mock<IConsole>();
            var prompt = "Enter a directory name";
            mockAutoCompleter
                .Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), prompt))
                .Returns(badDirectoryName);
            var prompter = new DirectoryPrompter(mockAutoCompleter.Object, mockConsole.Object);

            // Act
            var result = prompter.Prompt(this.WorkingDirectory, prompt, mustAlreadyExist: false);

            // Assert
            result.Name.Should().Be(badDirectoryName);
            result.FullName.Should().Be(Path.Combine(this.WorkingDirectory.FullName, badDirectoryName));
            mockAutoCompleter.Verify(ac => ac.ReadLine(new List<string> { "..", "subdir", "subdir2" }, prompt), Times.Once);
            mockConsole.Verify(c => c.WriteLine(It.IsAny<string>(), ConsoleOutputType.Error), Times.Never);
        }
    }
}
