// <copyright file="ColourMapperPrompterTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Moq;
    using Sde.AsciiArt;
    using Sde.AsciiArt.ColourMappers;
    using Sde.ConsoleGems.Consoles;
    using Xunit;

    /// <summary>
    /// Tests for <see cref="ColourMapperPrompter"/>.
    /// </summary>
    public class ColourMapperPrompterTests
    {
        /// <summary>
        /// Verifies that the correct mapper is returned when a valid option is selected.
        /// </summary>
        [Fact]
        public void Prompt_ReturnsSelectedMapper()
        {
            var consoleMock = new Mock<IConsole>();
            var prompter = new ColourMapperPrompter(consoleMock.Object);
            var mapper1 = new RuleBasedRgbConsoleColourMapper();
            var mapper2 = new WeightedRgbConsoleColourMapper();
            var options = new Dictionary<string, IConsoleColourMapper>
            {
                { "RuleBasedRgbConsoleColourMapper", mapper1 },
                { "WeightedRgbConsoleColourMapper", mapper2 },
            };

            // Simulate user entering "2" to select the second option
            var inputSequence = new Queue<string>(new[] { "2" });
            consoleMock.Setup(c => c.ReadLine()).Returns(() => inputSequence.Dequeue());

            // Should return the second mapper
            var selected = prompter.Prompt(options);
            selected.Should().Be(mapper2);
        }

        /// <summary>
        /// Verifies that an exception is thrown when no options are provided.
        /// </summary>
        [Fact]
        public void Prompt_Throws_When_NoOptions()
        {
            var consoleMock = new Mock<IConsole>();
            var prompter = new ColourMapperPrompter(consoleMock.Object);
            var options = new Dictionary<string, IConsoleColourMapper>();
            Action act = () => prompter.Prompt(options);
            act.Should().Throw<ArgumentException>();
        }

        /// <summary>
        /// Verifies that Prompt handles invalid input and loops until a valid selection is made.
        /// </summary>
        [Fact]
        public void Prompt_Retries_On_Invalid_Input()
        {
            var consoleMock = new Moq.Mock<IConsole>();
            var prompter = new ColourMapperPrompter(consoleMock.Object);
            var mapper1 = new RuleBasedRgbConsoleColourMapper();
            var mapper2 = new WeightedRgbConsoleColourMapper();
            var options = new Dictionary<string, IConsoleColourMapper>
            {
                { "RuleBasedRgbConsoleColourMapper", mapper1 },
                { "WeightedRgbConsoleColourMapper", mapper2 },
            };

            // Simulate user entering invalid input, then valid input
            var inputSequence = new Queue<string>(new[] { "foo", "0", "3", "1" });
            consoleMock.Setup(c => c.ReadLine()).Returns(() => inputSequence.Dequeue());

            var selected = prompter.Prompt(options);
            selected.Should().Be(mapper1);
        }
    }
}
