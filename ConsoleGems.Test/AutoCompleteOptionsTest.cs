// <copyright file="AutoCompleteOptionsTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test
{
    /// <summary>
    /// Unit tests for the <see cref="AutoCompleteOptions"/> class.
    /// </summary>
    public class AutoCompleteOptionsTest
    {
        /// <summary>
        /// Tests that the UseKeyPressMappings method sets the KeyPressMappings property.
        /// </summary>
        [Fact]
        public void UseKeyPressMappings_SetsKeyPressMappings()
        {
            // Arrange
            var options = new AutoCompleteOptions();

            // Act
            options.UseKeyPressMappings<TestKeyPressMappings>();

            // Assert
            options.KeyPressMappings.Should().Be(typeof(TestKeyPressMappings));
        }

        /// <summary>
        /// Tests that the UseMatcher method sets the Matcher property.
        /// </summary>
        [Fact]
        public void UseMatcher_SetsMatcher()
        {
            // Arrange
            var options = new AutoCompleteOptions();

            // Act
            options.UseMatcher<TestMatcher>();

            // Assert
            options.Matcher.Should().Be(typeof(TestMatcher));
        }

        private class TestKeyPressMappings : IAutoCompleteKeyPressMappings
        {
            public IDictionary<ConsoleKey, IAutoCompleteKeyPressHandler> Mappings => throw new NotImplementedException();

            public IAutoCompleteKeyPressHandler DefaultHandler => throw new NotImplementedException();
        }

        private class TestMatcher : IAutoCompleteMatcher
        {
            public int FindMatch(string userInput, List<string> suggestions, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
            {
                throw new NotImplementedException();
            }
        }
    }
}
