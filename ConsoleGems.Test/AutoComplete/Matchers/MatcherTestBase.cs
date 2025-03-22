// <copyright file="MatcherTestBase.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.Matchers
{
    /// <summary>
    /// Abstract base class for unit tests for
    /// <see cref="IAutoCompleteMatcher"/> implementations.
    /// </summary>
    public abstract class MatcherTestBase
    {
        /// <summary>
        /// Gets the <see cref="IAutoCompleteMatcher"/> implementation
        /// under test.
        /// </summary>
        public abstract IAutoCompleteMatcher MatcherUnderTest { get; }

        /// <summary>
        /// Gets a list of suggestions for the matcher under test.
        /// </summary>
        public List<string> Suggestions { get; } =
        [
            "ant",
            "antelope",
            "anticipate",
            "cant",
            "canterbury",
            "cantilever",
        ];

        /// <summary>
        /// Tests that -1 is returned when there is no match in the suggestions
        /// for the supplied user input.
        /// </summary>
        /// <param name="userInput">The string entered by the user.</param>
        [Theory]
        [InlineData("foo")]
        public void FindMatch_NoMatch_ReturnsMinus1(string userInput)
        {
            // Arrange
            var matcher = this.MatcherUnderTest;

            // Act
            var matchIndex = matcher.FindMatch(userInput, this.Suggestions);

            // Assert
            matchIndex.Should().Be(-1);
        }
    }
}
