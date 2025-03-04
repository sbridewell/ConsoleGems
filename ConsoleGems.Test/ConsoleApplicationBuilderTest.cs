// <copyright file="ConsoleApplicationBuilderTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test
{
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Sde.ConsoleGems.Commands.Demo;
    using Sde.ConsoleGems.Menus;

    /// <summary>
    /// Unit tests for the <see cref="ConsoleApplicationBuilder"/> class.
    /// </summary>
    public class ConsoleApplicationBuilderTest
    {
        /// <summary>
        /// Tests that the constructor registers the correct console reader and writer dependencies.
        /// </summary>
        [Fact]
        public void Constructor_RegistersCorrectDependencies()
        {
            // Arrange
            var serviceProvider = new ConsoleApplicationBuilder().BuildServiceProvider();

            // Act
            var console = serviceProvider.GetRequiredService<IConsole>();
            var consoleErrorWriter = serviceProvider.GetRequiredService<IConsoleErrorWriter>();
            var applicationState = serviceProvider.GetRequiredService<ApplicationState>();
            var sharedMenuItemsProvider = serviceProvider.GetRequiredService<ISharedMenuItemsProvider>();

            // Assert
            console.Should().BeOfType<Console>();
            consoleErrorWriter.Should().BeOfType<ConsoleErrorWriter>();
            applicationState.Should().BeOfType<ApplicationState>();
            sharedMenuItemsProvider.Should().BeOfType<EmptySharedMenuItemsProvider>();
            sharedMenuItemsProvider.MenuItems.Should().BeEmpty();
        }

        /// <summary>
        /// Tests that the <see cref="ConsoleApplicationBuilder.UseColours"/>
        /// method registers the correct console writer dependency.
        /// </summary>
        [Fact]
        public void UseColours_RegistersCorrectDependencies()
        {
            // Arrange
            var serviceProvider = new ConsoleApplicationBuilder()
                .UseColours()
                .BuildServiceProvider();

            // Act
            var console = serviceProvider.GetRequiredService<IConsole>();

            // Assert
            console.Should().BeOfType<ColourfulConsole>();
        }

        /// <summary>
        /// Tests that the <see cref="ConsoleApplicationBuilder.UseAutoComplete"/>
        /// method registers the correct auto completer dependency.
        /// </summary>
        [Fact]
        public void UseAutoComplete_RegistersCorrectIAutoCompleter()
        {
            // Arrange
            var serviceProvider = new ConsoleApplicationBuilder()
                .UseAutoComplete()
                .BuildServiceProvider();

            // Act
            var autoCompleter = serviceProvider.GetRequiredService<IAutoCompleter>();
            var autoCompleterKeyPressMappings = serviceProvider.GetRequiredService<IAutoCompleteKeyPressMappings>();

            // Assert
            autoCompleter.Should().BeOfType<AutoCompleter>();
            autoCompleterKeyPressMappings.Should().BeOfType<AutoCompleteKeyPressDefaultMappings>();
        }

        /// <summary>
        /// Tests that the <see cref="ConsoleApplicationBuilder.UsePrompters"/>
        /// method registers the correct prompter dependencies.
        /// </summary>
        [Fact]
        public void UsePrompters_RegistersCorrectPrompters()
        {
            // Arrange
            var serviceProvider = new ConsoleApplicationBuilder()
                .UsePrompters()
                .BuildServiceProvider();

            // Act
            var filePrompter = serviceProvider.GetRequiredService<IFilePrompter>();
            var directoryPrompter = serviceProvider.GetRequiredService<IDirectoryPrompter>();
            var booleanPrompter = serviceProvider.GetRequiredService<IPrompter<bool?>>();
            var autoCompleter = serviceProvider.GetRequiredService<IAutoCompleter>();

            // Assert
            filePrompter.Should().BeOfType<FilePrompter>();
            directoryPrompter.Should().BeOfType<DirectoryPrompter>();
            booleanPrompter.Should().BeOfType<BooleanPrompter>();
            autoCompleter.Should().BeOfType<AutoCompleter>();
        }

        /// <summary>
        /// Tests that the <see cref="ConsoleApplicationBuilder.AddSharedMenuItems{TSharedMenuItemsProvider}"/>
        /// method registers the menu items from the supplied
        /// <see cref="ISharedMenuItemsProvider"/> implementation.
        /// </summary>
        [Fact]
        public void AddSharedMenuItems_RegistersCorrectDependencies()
        {
            // Arrange
            var serviceProvider = new ConsoleApplicationBuilder()
                .AddSharedMenuItems<SharedMenuItemsProvider>()
                .BuildServiceProvider();

            // Act
            var sharedMenuItemsProvider = serviceProvider.GetRequiredService<ISharedMenuItemsProvider>();

            // Assert
            sharedMenuItemsProvider.Should().BeOfType<SharedMenuItemsProvider>();
            sharedMenuItemsProvider.MenuItems.Should().ContainSingle();
            sharedMenuItemsProvider.MenuItems[0].Key.Should().Be("a key");
        }

        /// <summary>
        /// Tests that the <see cref="ConsoleApplicationBuilder.SetMainMenu{TMenu}"/>
        /// method registers the correct dependencies.
        /// </summary>
        [Fact]
        public void SetMainMenu_RegistersCorrectDependencies()
        {
            // Arrange
            var builder = new ConsoleApplicationBuilder();
            var serviceProvider = builder
                .SetMainMenu<DemoMenu>()
                .BuildServiceProvider();

            // Act
            var menuWriter = serviceProvider.GetRequiredService<IMenuWriter>();
            var globalMenuItemsProvider = serviceProvider.GetRequiredService<IGlobalMenuItemsProvider>();

            // Act - demo menu and its items
            var demoMenu = serviceProvider.GetRequiredService<DemoMenu>();
            var getADrinkCommand = serviceProvider.GetRequiredService<GetADrinkCommand>();
            var selectAFileCommand = serviceProvider.GetRequiredService<SelectAFileCommand>();
            var throwExceptionCommand = serviceProvider.GetRequiredService<ThrowExceptionCommand>();

            // Act = global menu items
            var exitProgramCommand = serviceProvider.GetRequiredService<ExitProgramCommand>();

            // Assert
            menuWriter.Should().BeOfType<MenuWriter>();
            globalMenuItemsProvider.Should().BeOfType<GlobalMenuItemsProvider>();
            demoMenu.Should().BeOfType<DemoMenu>();
            getADrinkCommand.Should().BeOfType<GetADrinkCommand>();
            selectAFileCommand.Should().BeOfType<SelectAFileCommand>();
            throwExceptionCommand.Should().BeOfType<ThrowExceptionCommand>();
            exitProgramCommand.Should().BeOfType<ExitProgramCommand>();
        }

        /// <summary>
        /// Tests that the <see cref="ConsoleApplicationBuilder.SetMainMenu{TMenu}"/>
        /// method registers the correct dependencies for a menu with child menus.
        /// </summary>
        [Fact]
        public void SetMainMenu_MenuHasSubMenus_RegistersCorrectDependencies()
        {
            // Arrange
            var builder = new ConsoleApplicationBuilder();
            var provider = builder
                .SetMainMenu<MenuWithChildMenus>()
                .BuildServiceProvider();

            // Act
            var mainMenu = provider.GetRequiredService<MenuWithChildMenus>();
            var subMenu = provider.GetRequiredService<DemoMenu>();

            // Assert
            mainMenu.Should().BeOfType<MenuWithChildMenus>();
            subMenu.Should().BeOfType<DemoMenu>();
        }

        /// <summary>
        /// Tests that the <see cref="ConsoleApplicationBuilder.ShowMainMenu"/>
        /// method calls the <see cref="IMenu.ShowCommand"/> method of the main menu.
        /// </summary>
        [Fact]
        public void ShowMainMenu_ShowsMainMenu()
        {
            // Arrange
            var builder = new ConsoleApplicationBuilder();
            builder.SetMainMenu<FakeMenu>();
            var serviceProvider = builder.BuildServiceProvider();

            // Act
            builder.ShowMainMenu();

            // Assert
            serviceProvider.GetRequiredService<FakeMenu>().ShowCommandWasCalled.Should().BeTrue();
        }

        /// <summary>
        /// Tests that the correct exception is thrown by the <see cref="ConsoleApplicationBuilder.ShowMainMenu"/>
        /// method if the <see cref="ConsoleApplicationBuilder.SetMainMenu{TMenu}"/>
        /// method has not been called.
        /// </summary>
        [Fact]
        public void ShowMainMenu_NoMainMenuSet_Throws()
        {
            // Arrange
            var builder = new ConsoleApplicationBuilder();
            builder.BuildServiceProvider();
            var expectedMsg = "Main menu has not been set. Call the SetMainMenu method before calling the ShowMainMenu method.";

            // Act
            var action = () => builder.ShowMainMenu();

            // Assert
            var ex = action.Should().ThrowExactly<InvalidOperationException>().Which;
            ex.Message.Should().Be(expectedMsg);
        }

        /// <summary>
        /// Tests that the correct exception is thrown by the <see cref="ConsoleApplicationBuilder.ShowMainMenu"/>
        /// method if the <see cref="ConsoleApplicationBuilder.BuildServiceProvider"/>
        /// method has not been called.
        /// </summary>
        [Fact]
        public void ShowMainMenu_ServiceProviderNotBuilt_Throws()
        {
            // Arrange
            var builder = new ConsoleApplicationBuilder();
            builder.SetMainMenu<FakeMenu>();
            var expectedMsg = "Service provider has not been built. Call the BuildServiceProvider method before calling the ShowMainMenu method.";

            // Act
            var action = () => builder.ShowMainMenu();

            // Assert
            var ex = action.Should().ThrowExactly<InvalidOperationException>().Which;
            ex.Message.Should().Be(expectedMsg);
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

        private class FakeMenu(
            IAutoCompleter autoCompleter,
            IMenuWriter menuWriter,
            IConsoleErrorWriter consoleErrorWriter,
            ApplicationState applicationState)
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
            : AbstractMenu(autoCompleter, menuWriter, consoleErrorWriter, applicationState)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
        {
            public override string Title => "Fake menu";

            public override string Description => "Fake menu";

            public bool ShowCommandWasCalled { get; private set; }

            public override List<MenuItem> MenuItems =>
            [
                new () { Key = "a key", Description = "a description", Command = new Mock<ICommand>().Object, },
            ];

            public override ShowMenuCommand ShowCommand
            {
                get
                {
                    this.ShowCommandWasCalled = true;
                    var mockMenuWriter = new Mock<IMenuWriter>();
                    mockMenuWriter.Setup(m => m.GetAllMenuItems(It.IsAny<IMenu>())).Returns(new List<MenuItem>());
                    return new ShowMenuCommand(
                        this,
                        autoCompleter,
                        mockMenuWriter.Object,
                        consoleErrorWriter,
                        new ApplicationState { ExitCurrentMenu = true });
                }
            }
        }
    }
}
