// <copyright file="FilePrompterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Prompters
{
    using Xunit.Abstractions;

    /// <summary>
    /// Unit tests for the <see cref="FilePrompter"/> class.
    /// </summary>
    public class FilePrompterTest(ITestOutputHelper output)
        : FileSystemTestBase(output)
    {
        /// <summary>
        /// Tests that the correct filename is returned when the name of
        /// a file which exists is entered.
        /// </summary>
        [Fact]
        public void Prompt_FileExists_ReturnsCorrectFilename()
        {
            // Arrange
            var filename = "file.txt";
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var mockConsole = new Mock<IConsole>();
            var prompt = "Enter a filename";
            mockAutoCompleter.Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), prompt)).Returns(filename);
            var prompter = new FilePrompter(mockAutoCompleter.Object, mockConsole.Object);

            // Act
            var result = prompter.Prompt(this.WorkingDirectory, prompt, mustAlreadyExist: true);

            // Assert
            result.Name.Should().Be(filename);
            result.FullName.Should().Be(Path.Combine(this.WorkingDirectory.FullName, filename));
            mockAutoCompleter.Verify(ac => ac.ReadLine(new List<string> { "file.bmp", "file.log", "file.txt", "file2.txt" }, prompt), Times.Once);
            mockConsole.Verify(c => c.WriteLine(It.IsAny<string>(), ConsoleOutputType.Error), Times.Never);
        }

        /// <summary>
        /// Test that when the user enters a filename which does not exist,
        /// the prompter displays an error message and prompts again until
        /// the name of a file which exists is entered.
        /// </summary>
        [Fact]
        public void Prompt_FileDoesNotExist_WritesErrorAndPromptsAgain()
        {
            // Arrange
            var badFilename = "file3.txt";
            var goodFilename = "file.txt";
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var mockConsole = new Mock<IConsole>();
            var prompt = "Enter a filename";
            mockAutoCompleter
                .SetupSequence(ac => ac.ReadLine(It.IsAny<List<string>>(), prompt))
                .Returns(badFilename)
                .Returns(goodFilename);
            var prompter = new FilePrompter(mockAutoCompleter.Object, mockConsole.Object);

            // Act
            var result = prompter.Prompt(this.WorkingDirectory, prompt, mustAlreadyExist: true);

            // Assert
            result.Name.Should().Be(goodFilename);
            result.FullName.Should().Be(Path.Combine(this.WorkingDirectory.FullName, goodFilename));
            mockAutoCompleter.Verify(ac => ac.ReadLine(new List<string> { "file.bmp", "file.log", "file.txt", "file2.txt" }, prompt), Times.Exactly(2));
            mockConsole.Verify(
                c => c.WriteLine(
                    $"The file '{badFilename}' does not exist in the directory '{this.WorkingDirectory.FullName}'.",
                    ConsoleOutputType.Error),
                Times.Once);
        }

        /// <summary>
        /// Tests that the prompter can return the name of a file which does not exist
        /// if the mustAlreadyExist parameter is set to false.
        /// </summary>
        [Fact]
        public void Prompt_FileDoesNotExistAndMustAlreadyExistIsFalse_DoesNotWritError()
        {
            // Arrange
            var badFilename = "file3.txt";
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var mockConsole = new Mock<IConsole>();
            var prompt = "Enter a filename";
            mockAutoCompleter.Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), prompt)).Returns(badFilename);
            var prompter = new FilePrompter(mockAutoCompleter.Object, mockConsole.Object);

            // Act
            var result = prompter.Prompt(this.WorkingDirectory, prompt, mustAlreadyExist: false);

            // Assert
            result.Name.Should().Be(badFilename);
            result.FullName.Should().Be(Path.Combine(this.WorkingDirectory.FullName, badFilename));
            mockAutoCompleter.Verify(ac => ac.ReadLine(new List<string> { "file.bmp", "file.log", "file.txt", "file2.txt" }, prompt), Times.Once);
            mockConsole.Verify(c => c.WriteLine(It.IsAny<string>(), ConsoleOutputType.Error), Times.Never);
        }

        /// <summary>
        /// Tests that when a filename pattern is supplied to the prompter, only
        /// filenames which match the pattern are passed as suggestions to the
        /// <see cref="AutoCompleter"/>.
        /// </summary>
        [Fact]
        public void Prompt_FilterSupplied_FilteredFilenamesArePassedToAutoCompleter()
        {
            // Arrange
            var mockAutoCompleter = new Mock<IAutoCompleter>();
            var mockConsole = new Mock<IConsole>();
            var prompt = "Enter a filename";
            var filter = "*.log";
            mockAutoCompleter.Setup(ac => ac.ReadLine(It.IsAny<List<string>>(), prompt)).Returns("file.log");
            var prompter = new FilePrompter(mockAutoCompleter.Object, mockConsole.Object);

            // Act
            _ = prompter.Prompt(this.WorkingDirectory, prompt, mustAlreadyExist: true, filter);

            // Assert
            mockAutoCompleter.Verify(ac => ac.ReadLine(new List<string> { "file.log" }, prompt), Times.Once);
            mockConsole.Verify(c => c.WriteLine(It.IsAny<string>(), ConsoleOutputType.Error), Times.Never);
        }
    }
}
