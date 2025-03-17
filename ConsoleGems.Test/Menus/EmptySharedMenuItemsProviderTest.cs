// <copyright file="EmptySharedMenuItemsProviderTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Menus
{
    /// <summary>
    /// Unit tests for the <see cref="EmptySharedMenuItemsProvider"/> class.
    /// </summary>
    public class EmptySharedMenuItemsProviderTest
    {
        /// <summary>
        /// Tests that the <see cref="EmptySharedMenuItemsProvider.MenuItems"/>
        /// property returns an empty list.
        /// </summary>
        [Fact]
        public void MenuItems_ShouldBeEmpty()
        {
            // Arrange
            var provider = new EmptySharedMenuItemsProvider();

            // Act
            var items = provider.MenuItems;

            // Assert
            items.Should().BeEmpty();
        }
    }
}
