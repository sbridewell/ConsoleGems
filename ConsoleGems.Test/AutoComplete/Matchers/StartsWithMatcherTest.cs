// <copyright file="StartsWithMatcherTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.Matchers
{
    /// <summary>
    /// Tests for the <see cref="StartsWithMatcher"/> class.
    /// </summary>
    public class StartsWithMatcherTest
    {
        private readonly List<string> suggestions =
        [
            "ant",
            "antelope",
            "anticipate",
            "cant",
            "canterbury",
            "cantilever",
        ];

        /// <summary>
        /// Tests that the index of the expected suggestion is returned when
        /// the user input is supplied.
        /// </summary>
        /// <param name="userInput">The string entered by the user.</param>
        /// <param name="expectedSuggestion">
        /// The suggestion which should be returned.
        /// </param>
        [Theory]
        [InlineData("", "ant")]
        [InlineData("a", "ant")]
        [InlineData("an", "ant")]
        [InlineData("ant", "ant")]
        [InlineData("ante", "antelope")]
        [InlineData("anti", "anticipate")]
        [InlineData("anto", "ant")]
        public void FindMatch_ReturnsCorrectMatch(string userInput, string expectedSuggestion)
        {
            // Arrange
            var matcher = new StartsWithMatcher();

            // Act
            var matchIndex = matcher.FindMatch(userInput, this.suggestions);

            // Assert
            matchIndex.Should().Be(this.suggestions.IndexOf(expectedSuggestion));
        }

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
            var matcher = new StartsWithMatcher();

            // Act
            var matchIndex = matcher.FindMatch(userInput, this.suggestions);

            // Assert
            matchIndex.Should().Be(-1);
        }
    }
}
