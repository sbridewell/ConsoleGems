// <copyright file="CharacterBlenderPrompterTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Moq;
    using Sde.AsciiArt;
    using Sde.AsciiArt.CharacterBlenders;
    using Sde.ConsoleGems.Consoles;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="CharacterBlenderPrompter"/> class.
    /// </summary>
    public class CharacterBlenderPrompterTests
    {
        /// <summary>
        /// Verifies that Prompt returns the selected character blender when a valid option is chosen.
        /// </summary>
        [Fact]
        public void Prompt_ReturnsSelectedCharacterBlender()
        {
            var mockConsole = new Mock<IConsole>();
            var options = new Dictionary<string, ICharacterBlender>
            {
                { "Block", new DummyCharacterBlender('█') },
                { "Space", new DummyCharacterBlender(' ') },
            };

            // Simulate user selecting option 2
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("2");
            var prompter = new CharacterBlenderPrompter(mockConsole.Object);
            var selected = prompter.Prompt(options);
            selected.Should().Be(options["Space"]);
        }

        /// <summary>
        /// Verifies that Prompt throws ArgumentException when no options are provided.
        /// </summary>
        [Fact]
        public void Prompt_ThrowsArgumentException_WhenNoOptions()
        {
            var mockConsole = new Mock<IConsole>();
            var prompter = new CharacterBlenderPrompter(mockConsole.Object);
            var options = new Dictionary<string, ICharacterBlender>();
            Assert.Throws<ArgumentException>(() => prompter.Prompt(options));
        }

        /// <summary>
        /// Verifies that Prompt shows an error and reprompts when the user enters invalid selections,
        /// and returns the correct character blender when a valid selection is finally made.
        /// </summary>
        [Fact]
        public void Prompt_InvalidSelection_ShowsErrorAndReprompts()
        {
            var mockConsole = new Mock<IConsole>();
            var options = new Dictionary<string, ICharacterBlender>
            {
                { "Block", new DummyCharacterBlender('█') },
                { "Space", new DummyCharacterBlender(' ') },
            };

            // Simulate user entering invalid option then valid option
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("0") // invalid (too low)
                .Returns("3") // invalid (too high)
                .Returns("abc") // invalid (not a number)
                .Returns("1");  // valid
            var prompter = new CharacterBlenderPrompter(mockConsole.Object);
            var selected = prompter.Prompt(options);
            selected.Should().Be(options["Block"]);

            // Optionally verify error message was shown
            mockConsole.Verify(c => c.WriteLine("Invalid selection. Please try again.", ConsoleOutputType.Error), Times.Exactly(3));
        }

        private class DummyCharacterBlender : ICharacterBlender
        {
            private readonly char character;

            public DummyCharacterBlender(char character) => this.character = character;

            public char GetCharacterForRatio(double ratio) => this.character;
        }
    }
}
