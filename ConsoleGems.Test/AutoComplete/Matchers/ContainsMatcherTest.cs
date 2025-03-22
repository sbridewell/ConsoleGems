// <copyright file="ContainsMatcherTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.AutoComplete.Matchers
{
    /// <summary>
    /// Unit tests for the <see cref="ContainsMatcher"/> class.
    /// </summary>
    public class ContainsMatcherTest : MatcherTestBase
    {
        /// <inheritdoc/>
        public override IAutoCompleteMatcher MatcherUnderTest => new ContainsMatcher();

        /// <summary>
        /// Tests that the index of the expected suggestion is returned when
        /// the user input is supplied.
        /// </summary>
        /// <param name="userInput">The string entered by the user.</param>
        /// <param name="expectedSuggestion">
        /// The suggestion which should be returned.
        /// </param>
        [Theory]
        [InlineData("a", "ant")]
        [InlineData("an", "ant")]
        [InlineData("ant", "ant")]
        [InlineData("ante", "antelope")]
        [InlineData("tel", "antelope")]
        [InlineData("ici", "anticipate")]
        public void FindMatch_ReturnsCorrectMatch(string userInput, string expectedSuggestion)
        {
            // Arrange
            var matcher = new ContainsMatcher();

            // Act
            var matchIndex = matcher.FindMatch(userInput, this.Suggestions);

            // Assert
            matchIndex.Should().Be(this.Suggestions.IndexOf(expectedSuggestion));
        }
    }
}
