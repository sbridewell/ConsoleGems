// <copyright file="ConsoleGemsOptionsTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test
{
    /// <summary>
    /// Unit tests for the <see cref="ConsoleGemsOptions"/> class.
    /// </summary>
    public class ConsoleGemsOptionsTest
    {
        /// <summary>
        /// Tests that when options are passed to the UseAutoComplete method, the
        /// AutoCompleteOptions property is configured correctly.
        /// </summary>
        [Fact]
        public void UseAutoComplete_ConfigureSupplied_ConfiguresAutoCompleteOptions()
        {
            // Arrange
            var options = new ConsoleGemsOptions();

            // Act
            options.UseAutoComplete(options =>
            {
                options.UseKeyPressMappings<TestKeyPressMappings>();
                options.UseMatcher<TestMatcher>();
            });

            // Assert
            options.AutoCompleteOptions.Should().NotBeNull();
            options.AutoCompleteOptions.KeyPressMappings.Should().Be(typeof(TestKeyPressMappings));
            options.AutoCompleteOptions.Matcher.Should().Be(typeof(TestMatcher));
        }

        /// <summary>
        /// Tests that when no options are passed to the UseAutoComplete method,
        /// the correct default AutoCompleteOptions are configured.
        /// </summary>
        [Fact]
        public void UseAutoComplete_NoConfigureSupplied_ConfiguresDefaultAutoCompleteOptions()
        {
            // Arrange
            var options = new ConsoleGemsOptions();

            // Act
            options.UseAutoComplete();

            // Assert
            options.AutoCompleteOptions.Should().NotBeNull();
            options.AutoCompleteOptions.KeyPressMappings.Should().Be(typeof(AutoCompleteKeyPressDefaultMappings));
            options.AutoCompleteOptions.Matcher.Should().Be(typeof(StartsWithMatcher));
        }

        /// <summary>
        /// Tests that if auto-complete has not already been configured,
        /// the UseMainMenu method sets the correct main menu and default
        /// AutoCompleteOptions.
        /// </summary>
        [Fact]
        public void UseMainMenu_AutoCompleteNotAlreadyConfigured_SetsMainMenuAndDefaultAutoCompleteOptions()
        {
            // Arrange
            var options = new ConsoleGemsOptions();

            // Act
            options.UseMainMenu<TestMenu>();

            // Assert
            options.MainMenu.Should().Be(typeof(TestMenu));
            options.AutoCompleteOptions.Should().NotBeNull();
            options.AutoCompleteOptions.KeyPressMappings.Should().Be(typeof(AutoCompleteKeyPressDefaultMappings));
            options.AutoCompleteOptions.Matcher.Should().Be(typeof(StartsWithMatcher));
        }

        /// <summary>
        /// Tests that if auto-complete has already been configured, the
        /// UseMainMenu method sets the correct main menu and does not
        /// change the AutoCompleteOptions.
        /// </summary>
        [Fact]
        public void UseMainMenu_AutoCompleteAlreadyConfigured_SetsMainMenuAndDoesNotChangeAutoCompleteOptions()
        {
            // Arrange
            var options = new ConsoleGemsOptions();
            options.UseAutoComplete(options => options
                .UseKeyPressMappings<TestKeyPressMappings>()
                .UseMatcher<TestMatcher>());

            // Act
            options.UseMainMenu<TestMenu>();

            // Assert
            options.MainMenu.Should().Be(typeof(TestMenu));
            options.AutoCompleteOptions.Should().NotBeNull();
            options.AutoCompleteOptions.KeyPressMappings.Should().Be(typeof(TestKeyPressMappings));
            options.AutoCompleteOptions.Matcher.Should().Be(typeof(TestMatcher));
        }

        /// <summary>
        /// Tests that the UseMenuWriter method sets the correct menu writer.
        /// </summary>
        [Fact]
        public void UseMenuWriter_SetsMenuWriter()
        {
            // Arrange
            var options = new ConsoleGemsOptions();

            // Act
            options.UseMenuWriter<TestMenuWriter>();

            // Assert
            options.MenuWriter.Should().Be(typeof(TestMenuWriter));
        }

        /// <summary>
        /// Tests that the UseBuiltinPrompters method adds the built-in prompters.
        /// </summary>
        [Fact]
        public void UseBuiltInPrompters_AddsPrompters()
        {
            // Arrange
            var options = new ConsoleGemsOptions();

            // Act
            options.UseBuiltInPrompters();

            // Assert
            options.Prompters.Should().HaveCount(3);
            options.Prompters[typeof(IBooleanPrompter)].Should().Be(typeof(BooleanPrompter));
            options.Prompters[typeof(IFilePrompter)].Should().Be(typeof(FilePrompter));
            options.Prompters[typeof(IDirectoryPrompter)].Should().Be(typeof(DirectoryPrompter));
        }

        /// <summary>
        /// Tests that the AddPrompter method adds the supplied prompter.
        /// </summary>
        [Fact]
        public void AddPrompter_AddsPrompter()
        {
            // Arrange
            var options = new ConsoleGemsOptions();

            // Act
            options.AddPrompter<IBooleanPrompter, TestPrompter>();

            // Assert
            options.Prompters.Should().HaveCount(1);
            options.Prompters[typeof(IBooleanPrompter)].Should().Be(typeof(TestPrompter));
        }

        /// <summary>
        /// Tests that the UseSharedMenuItemsProvider method sets the shared
        /// menu items provider to the supplied type.
        /// </summary>
        [Fact]
        public void UseSharedMenuItemsProvider_SetsSharedMenuItemsProvider()
        {
            // Arrange
            var options = new ConsoleGemsOptions();

            // Act
            options.UseSharedMenuItemsProvider<TestSharedMenuItemsProvider>();

            // Assert
            options.SharedMenuItemsProvider.Should().Be(typeof(TestSharedMenuItemsProvider));
        }

        #region private classes

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

        private class TestMenu : IMenu
        {
            public string Title => throw new NotImplementedException();

            public string Description => throw new NotImplementedException();

            public List<MenuItem> MenuItems => throw new NotImplementedException();

            public ShowMenuCommand ShowCommand => throw new NotImplementedException();
        }

        private class TestMenuWriter : IMenuWriter
        {
            public List<MenuItem> GetAllMenuItems(IMenu menu)
            {
                throw new NotImplementedException();
            }

            public void WriteMenu(IMenu menu)
            {
                throw new NotImplementedException();
            }
        }

        private class TestPrompter : IBooleanPrompter
        {
            public bool? Prompt(string prompt, bool? defaultValue = null)
            {
                throw new NotImplementedException();
            }
        }

        private class TestSharedMenuItemsProvider : ISharedMenuItemsProvider
        {
            public List<MenuItem> MenuItems => throw new NotImplementedException();
        }

        #endregion
    }
}
