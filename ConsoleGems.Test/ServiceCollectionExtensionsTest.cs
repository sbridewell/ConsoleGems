// <copyright file="ServiceCollectionExtensionsTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Sde.ConsoleGems;
    using Sde.ConsoleGems.Menus;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTest
    {
        /// <summary>
        /// Tests that the constructor registers the correct dependencies.
        /// </summary>
        [Fact]
        public void Constructor_RegistersCorrectDependencies()
        {
            // Arrange
            Func<IServiceCollection, IServiceCollection> pipeline = (sc) => sc.AddConsoleGems();

            // Act
            var serviceProvider = BuildServiceProvider(pipeline);

            // Assert
            var console = serviceProvider.GetRequiredService<IConsole>();
            var consoleErrorWriter = serviceProvider.GetRequiredService<IConsoleErrorWriter>();
            var applicationState = serviceProvider.GetRequiredService<ApplicationState>();
            var sharedMenuItemsProvider = serviceProvider.GetRequiredService<ISharedMenuItemsProvider>();
            console.Should().BeOfType<Console>();
            consoleErrorWriter.Should().BeOfType<ConsoleErrorWriter>();
            applicationState.Should().BeOfType<ApplicationState>();
            sharedMenuItemsProvider.Should().BeOfType<EmptySharedMenuItemsProvider>();
            sharedMenuItemsProvider.MenuItems.Should().BeEmpty();
        }

        /// <summary>
        /// Tests that the UseColours method registers the correct
        /// console dependency.
        /// </summary>
        [Fact]
        public void UseColours_RegistersCorrectDependencies()
        {
            // Arrange
            Func<IServiceCollection, IServiceCollection> pipeline = (sc) => sc.UseColours();

            // Act
            var serviceProvider = BuildServiceProvider(pipeline);

            // Assert
            var console = serviceProvider.GetRequiredService<IConsole>();
            console.Should().BeOfType<ColourfulConsole>();
        }

        /// <summary>
        /// Tests that the UseAutoComplete method registers the correct
        /// auto completer dependency.
        /// </summary>
        [Fact]
        public void UseAutoComplete_RegistersCorrectIAutoCompleter()
        {
            // Arrange
            Func<IServiceCollection, IServiceCollection> pipeline = (sc) => sc.AddConsoleGems().UseAutoComplete();

            // Act
            var serviceProvider = BuildServiceProvider(pipeline);

            // Assert
            var autoCompleter = serviceProvider.GetRequiredService<IAutoCompleter>();
            var autoCompleterKeyPressMappings = serviceProvider.GetRequiredService<IAutoCompleteKeyPressMappings>();
            autoCompleter.Should().BeOfType<AutoCompleter>();
            autoCompleterKeyPressMappings.Should().BeOfType<AutoCompleteKeyPressDefaultMappings>();
        }

        /// <summary>
        /// Tests that the UsePrompters method registers the
        /// correct prompter dependencies.
        /// </summary>
        [Fact]
        public void UsePrompters_RegistersCorrectPrompters()
        {
            // Arrange
            Func<IServiceCollection, IServiceCollection> pipeline = (sc) => sc.AddConsoleGems().UsePrompters();

            // Act
            var serviceProvider = BuildServiceProvider(pipeline);

            // Assert
            var filePrompter = serviceProvider.GetRequiredService<IFilePrompter>();
            var directoryPrompter = serviceProvider.GetRequiredService<IDirectoryPrompter>();
            var booleanPrompter = serviceProvider.GetRequiredService<IPrompter<bool?>>();
            var autoCompleter = serviceProvider.GetRequiredService<IAutoCompleter>();
            filePrompter.Should().BeOfType<FilePrompter>();
            directoryPrompter.Should().BeOfType<DirectoryPrompter>();
            booleanPrompter.Should().BeOfType<BooleanPrompter>();
            autoCompleter.Should().BeOfType<AutoCompleter>();
        }

        /// <summary>
        /// Tests that the AddSharedMenuItems method registers the
        /// menu items from the supplied
        /// <see cref="ISharedMenuItemsProvider"/> implementation.
        /// </summary>
        [Fact]
        public void AddSharedMenuItems_RegistersCorrectDependencies()
        {
            // Arrange
            Func<IServiceCollection, IServiceCollection> pipeline
                = (sc) => sc.AddSharedMenuItems<SharedMenuItemsProvider>();

            // Act
            var serviceProvider = BuildServiceProvider(pipeline);

            // Assert
            var sharedMenuItemsProvider = serviceProvider.GetRequiredService<ISharedMenuItemsProvider>();
            sharedMenuItemsProvider.Should().BeOfType<SharedMenuItemsProvider>();
            sharedMenuItemsProvider.MenuItems.Should().ContainSingle();
            sharedMenuItemsProvider.MenuItems[0].Key.Should().Be("a key");
        }

        /// <summary>
        /// Tests that the SetMainMenu method registers the correct dependencies.
        /// </summary>
        [Fact]
        public void SetMainMenu_RegistersCorrectDependencies()
        {
            // Arrange
            Func<IServiceCollection, IServiceCollection> pipeline
                = (sc) => sc.AddConsoleGems().SetMainMenu<DemoMenu>();

            // Act
            var serviceProvider = BuildServiceProvider(pipeline);

            // Assert
            var menuWriter = serviceProvider.GetRequiredService<IMenuWriter>();
            var globalMenuItemsProvider = serviceProvider.GetRequiredService<IGlobalMenuItemsProvider>();
            var demoMenu = serviceProvider.GetRequiredService<DemoMenu>();
            var getADrinkCommand = serviceProvider.GetRequiredService<GetADrinkCommand>();
            var selectAFileCommand = serviceProvider.GetRequiredService<SelectAFileCommand>();
            var throwExceptionCommand = serviceProvider.GetRequiredService<ThrowExceptionCommand>();
            var exitProgramCommand = serviceProvider.GetRequiredService<ExitProgramCommand>();
            menuWriter.Should().BeOfType<MenuWriter>();
            globalMenuItemsProvider.Should().BeOfType<GlobalMenuItemsProvider>();
            demoMenu.Should().BeOfType<DemoMenu>();
            getADrinkCommand.Should().BeOfType<GetADrinkCommand>();
            selectAFileCommand.Should().BeOfType<SelectAFileCommand>();
            throwExceptionCommand.Should().BeOfType<ThrowExceptionCommand>();
            exitProgramCommand.Should().BeOfType<ExitProgramCommand>();
        }

        /// <summary>
        /// Tests that the SetMainMenu
        /// method registers the correct dependencies for a menu with child menus.
        /// </summary>
        [Fact]
        public void SetMainMenu_MenuHasSubMenus_RegistersCorrectDependencies()
        {
            // Arrange
            Func<IServiceCollection, IServiceCollection> pipeline
                = (sc) => sc.AddConsoleGems().SetMainMenu<MenuWithChildMenus>();

            // Act
            var provider = BuildServiceProvider(pipeline);

            // Assert
            var mainMenu = provider.GetRequiredService<MenuWithChildMenus>();
            var subMenu = provider.GetRequiredService<DemoMenu>();
            mainMenu.Should().BeOfType<MenuWithChildMenus>();
            subMenu.Should().BeOfType<DemoMenu>();
        }

        private static ServiceProvider BuildServiceProvider(Func<IServiceCollection, IServiceCollection> arrangeAndAct)
        {
            var serviceCollection = new ServiceCollection();
            arrangeAndAct(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

        private class SharedMenuItemsProvider : ISharedMenuItemsProvider
        {
            public List<MenuItem> MenuItems =>
            [
                new MenuItem { Key = "a key", Description = "a description", Command = new Mock<ICommand>().Object },
                    ];
        }

        private class MenuWithChildMenus(
            DemoMenu demoMenu,
            IAutoCompleter autoCompleter,
            IMenuWriter consoleMenuWriter,
            IConsoleErrorWriter consoleErrorWriter,
            ApplicationState applicationState)
            : AbstractMenu(autoCompleter, consoleMenuWriter, consoleErrorWriter, applicationState)
        {
            public override string Title => "Menu with child menus";

            public override string Description => "A menu with child menus";

            public override List<MenuItem> MenuItems =>
            [
                new () { Key = "a key", Description = "a description", Command = new Mock<ICommand>().Object, },
                        new () { Key = "demo", Description = "Demo menu", Command = demoMenu.ShowCommand, },
                    ];
        }
    }
}
