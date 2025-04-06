// <copyright file="ServiceCollectionExtensionsTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test
{
    using System.Text.Json;
    using Microsoft.Extensions.DependencyInjection;
    using Sde.ConsoleGems;
    using Sde.ConsoleGems.Consoles;
    using Sde.ConsoleGems.Menus;
    using Sde.ConsoleGems.Text;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class,
    /// focussed mainly on asserting that the correct dependencies have
    /// been injected. Also tests the <see cref="ConsoleGemsOptions"/>
    /// class.
    /// </summary>
    public class ServiceCollectionExtensionsTest
    {
        /// <summary>
        /// Tests that when no <see cref="ConsoleGemsOptions"/>
        /// are passed to the AddConsoleGems method, the correct
        /// dependencies are registered.
        /// </summary>
        [Fact]
        public void NoOptions_RegistersCorrectDependencies()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddConsoleGems();

            // Act
            var provider = serviceCollection.BuildServiceProvider();

            // Assert
            AssertColourfulConsole(provider);
            AssertNoAutoComplete(provider);
            AssertNoBuiltInPrompters(provider);
            AssertService(provider, typeof(ApplicationState), typeof(ApplicationState));
        }

        /// <summary>
        /// Tests that when a <see cref="ConsoleGemsOptions"/> instance
        /// with the <see cref="ConsoleGemsOptions.UseColours"/> property
        /// set to true is passed to the AddConsoleGems method, the correct
        /// dependencies are registered.
        /// </summary>
        [Fact]
        public void UseColoursTrue_RegistersCorrectDependencies()
        {
            // Arrange
            var options = new ConsoleGemsOptions { UseColours = true };

            // Act
            var provider = BuildServiceProvider(options);

            // Assert
            AssertColourfulConsole(provider);
            AssertNoAutoComplete(provider);
            AssertNoBuiltInPrompters(provider);
            AssertService(provider, typeof(ApplicationState), typeof(ApplicationState));
        }

        /// <summary>
        /// Tests that when a <see cref="ConsoleGemsOptions"/> instance
        /// with the <see cref="ConsoleGemsOptions.UseColours"/> property
        /// set to false is passed to the AddConsoleGems method, the correct
        /// dependencies are registered.
        /// </summary>
        [Fact]
        public void UseColoursFalse_RegistersCorrectDependencies()
        {
            // Arrange
            var options = new ConsoleGemsOptions { UseColours = false };

            // Act
            var provider = BuildServiceProvider(options);

            // Assert
            AssertMonochromeConsole(provider);
            AssertNoAutoComplete(provider);
            AssertNoBuiltInPrompters(provider);
            AssertService(provider, typeof(ApplicationState), typeof(ApplicationState));
        }

        /// <summary>
        /// Tests that when the UseBuiltInPrompters method of a
        /// <see cref="ConsoleGemsOptions"/> instance is called, the
        /// correct dependencies are registered.
        /// </summary>
        [Fact]
        public void UseBuiltInPrompters_RegistersCorrectPrompters()
        {
            // Arrange
            var options = new ConsoleGemsOptions()
                .UseBuiltInPrompters();

            // Act
            var provider = BuildServiceProvider(options);

            // Assert
            AssertMonochromeConsole(provider);
            AssertBuiltInPrompters(provider);

            var autoCompleter = provider.GetRequiredService<IAutoCompleter>();
            autoCompleter.Should().BeOfType<AutoCompleter>();
            AssertService(provider, typeof(ApplicationState), typeof(ApplicationState));
        }

        /// <summary>
        /// Tests that when auto-complete is used, the correct
        /// dependencies are registered.
        /// </summary>
        [Fact]
        public void UseAutoComplete_NoMappingsSupplied_RegistersCorrectDependencies()
        {
            // Arrange
            var options = new ConsoleGemsOptions()
                .UseAutoComplete();

            // Act
            var provider = BuildServiceProvider(options);

            // Assert
            AssertAutoCompleter(provider);
            AssertNoBuiltInPrompters(provider);
            AssertService(provider, typeof(IAutoCompleteKeyPressMappings), typeof(AutoCompleteKeyPressDefaultMappings));
            AssertService(provider, typeof(ApplicationState), typeof(ApplicationState));
        }

        /// <summary>
        /// Tests that when auto-complete is used with a
        /// custom <see cref="IAutoCompleteKeyPressMappings"/>
        /// implementation, the correct dependencies are registered.
        /// </summary>
        [Fact]
        public void UseAutoComplete_MapppingsSupplied_RegistersCorrectDependencies()
        {
            // Arrange
            var options = new ConsoleGemsOptions()
                .UseAutoComplete(options => options
                    .UseKeyPressMappings<TestKeyPressMappings>());

            // Act
            var provider = BuildServiceProvider(options);

            // Assert
            AssertService(provider, typeof(IAutoCompleter), typeof(AutoCompleter));
            AssertNoBuiltInPrompters(provider);
            AssertService(provider, typeof(ApplicationState), typeof(ApplicationState));
            AssertService(provider, typeof(IAutoCompleteKeyPressMappings), typeof(TestKeyPressMappings));
        }

        /// <summary>
        /// Tests that the AddSharedMenuItems method registers the
        /// menu items from the supplied
        /// <see cref="ISharedMenuItemsProvider"/> implementation.
        /// </summary>
        [Fact]
        public void AddSharedMenuItemsProvider_RegistersCorrectDependencies()
        {
            // Arrange
            var options = new ConsoleGemsOptions()
                .UseSharedMenuItemsProvider<SharedMenuItemsProvider>();

            // Act
            var provider = BuildServiceProvider(options);

            // Assert
            var sharedMenuItemsProvider = provider.GetRequiredService<ISharedMenuItemsProvider>();
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
            var options = new ConsoleGemsOptions()
                .UseMainMenu<ChildMenu>();

            // Act
            var provider = BuildServiceProvider(options);

            // Assert
            AssertMonochromeConsole(provider);
            AssertAutoCompleter(provider);
            AssertService(provider, typeof(ChildMenu), typeof(ChildMenu));
            AssertService(provider, typeof(IMenuWriter), typeof(MenuWriter));
            AssertService(provider, typeof(IGlobalMenuItemsProvider), typeof(GlobalMenuItemsProvider));
        }

        /// <summary>
        /// Tests that the SetMainMenu
        /// method registers the correct dependencies for a menu with child menus.
        /// </summary>
        [Fact]
        public void SetMainMenu_MenuHasSubMenus_RegistersCorrectDependencies()
        {
            // Arrange
            var options = new ConsoleGemsOptions()
                .UseMainMenu<MenuWithChildMenus>();

            // Act
            var provider = BuildServiceProvider(options);

            // Assert
            AssertMonochromeConsole(provider);
            AssertAutoCompleter(provider);
            AssertService(provider, typeof(MenuWithChildMenus), typeof(MenuWithChildMenus));
            AssertService(provider, typeof(ChildMenu), typeof(ChildMenu));
            AssertService(provider, typeof(IMenuWriter), typeof(MenuWriter));
            AssertService(provider, typeof(IGlobalMenuItemsProvider), typeof(GlobalMenuItemsProvider));
            AssertService(provider, typeof(AsciiArtSettings), typeof(AsciiArtSettings));
        }

        /// <summary>
        /// Tests that the provider registers the correct
        /// <see cref="AsciiArtSettings"/> instance when the UseAsciiArtSettings
        /// method of ConsoleGemsOptions is passed a filename.
        /// </summary>
        [Fact]
        public void SetMainMenu_UseAsciiArtSettings_UsesSuppliedAsciiArtSettings()
        {
            // Arrange
            var asciiArtSettings = new AsciiArtSettings
            {
                InnerBorderHorizontal = '-',
                InnerBorderJoin = '+',
                InnerBorderJoinBottom = '+',
                InnerBorderJoinTop = '+',
                InnerBorderVertical = '|',
                OuterBorderBottomLeft = '\\',
                OuterBorderBottomRight = '/',
                OuterBorderHorizontal = '-',
                OuterBorderTopLeft = '/',
                OuterBorderTopRight = '\\',
                OuterInnerJoinLeft = '+',
                OuterInnerJoinRight = '+',
                OuterInnerJoinBottom = '+',
                OuterInnerJoinTop = '+',
                OuterBorderVertical = '|',
            };
            var tempFile = Path.GetTempFileName();
            var json = JsonSerializer.Serialize(asciiArtSettings);
            File.WriteAllText(tempFile, json);
            var options = new ConsoleGemsOptions()
                .UseMainMenu<MenuWithChildMenus>()
                .UseAsciiArtSettings(tempFile);

            // Act
            var provider = BuildServiceProvider(options);

            // Assert
            var actualSettings = provider.GetRequiredService<AsciiArtSettings>();
            actualSettings.Should().BeEquivalentTo(asciiArtSettings);
        }

        /// <summary>
        /// Tests that when a custom <see cref="IMenuWriter"/> is selected,
        /// the correct dependencies are registered.
        /// </summary>
        [Fact]
        public void UseMenuWriter_RegistersCorrectDependencies()
        {
            // Arrange
            var options = new ConsoleGemsOptions()
                .UseMainMenu<MenuWithChildMenus>()
                .UseMenuWriter<MenuWriterForTesting>();

            // Act
            var provider = BuildServiceProvider(options);

            // Assert
            AssertMonochromeConsole(provider);
            AssertAutoCompleter(provider);
            AssertService(provider, typeof(MenuWithChildMenus), typeof(MenuWithChildMenus));
            AssertService(provider, typeof(ChildMenu), typeof(ChildMenu));
            AssertService(provider, typeof(IMenuWriter), typeof(MenuWriterForTesting));
            AssertService(provider, typeof(IGlobalMenuItemsProvider), typeof(GlobalMenuItemsProvider));
        }

        /// <summary>
        /// Tests that the correct exception is thrown when attempting to self-
        /// register an interface without an implementation.
        /// </summary>
        [Fact]
        public void AddSingletonInternal_SelfRegisterInterface_Throws()
        {
            // Arrange
            var options = new ConsoleGemsOptions().UseMainMenu<IMenu>();

            // Act
            var action = () => BuildServiceProvider(options);

            // Assert
            var ex = action.Should().ThrowExactly<ArgumentException>().Which;
            ex.Message.Should().Contain("Cannot register an interface without an implementation type");
            ex.ParamName.Should().Be("serviceType");
        }

        private static void AssertAutoCompleter(IServiceProvider serviceProvider)
        {
            AssertService(serviceProvider, typeof(IAutoCompleter), typeof(AutoCompleter));
            AssertService(serviceProvider, typeof(IAutoCompleteKeyPressMappings), typeof(AutoCompleteKeyPressDefaultMappings));
        }

        private static void AssertMonochromeConsole(IServiceProvider serviceProvider)
        {
            AssertService(serviceProvider, typeof(IConsole), typeof(Console));
            AssertNoService(serviceProvider, typeof(IConsoleColourManager));
        }

        private static void AssertColourfulConsole(IServiceProvider serviceProvider)
        {
            AssertService(serviceProvider, typeof(IConsole), typeof(ColourfulConsole));
            AssertService(serviceProvider, typeof(IConsoleColourManager), typeof(ConsoleColourManager));
        }

        private static void AssertNoAutoComplete(IServiceProvider serviceProvider)
        {
            AssertNoService(serviceProvider, typeof(IAutoCompleter));
            AssertNoService(serviceProvider, typeof(IAutoCompleteKeyPressMappings));
        }

        private static void AssertBuiltInPrompters(IServiceProvider serviceProvider)
        {
            AssertService(serviceProvider, typeof(IBooleanPrompter), typeof(BooleanPrompter));
            AssertService(serviceProvider, typeof(IFilePrompter), typeof(FilePrompter));
            AssertService(serviceProvider, typeof(IDirectoryPrompter), typeof(DirectoryPrompter));
        }

        private static void AssertNoBuiltInPrompters(IServiceProvider serviceProvider)
        {
            AssertNoService(serviceProvider, typeof(IBooleanPrompter));
            AssertNoService(serviceProvider, typeof(IFilePrompter));
            AssertNoService(serviceProvider, typeof(IDirectoryPrompter));
        }

        /// <summary>
        /// Asserts that the supplied <see cref="ServiceProvider"/>
        /// resolves the supplied service type to an instance of the
        /// supplied implementation type.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serviceType">The type of service.</param>
        /// <param name="implementationType">
        /// The concrete implementation type of the service.
        /// </param>
        private static void AssertService(
            IServiceProvider serviceProvider,
            Type serviceType,
            Type implementationType)
        {
            var service = serviceProvider.GetRequiredService(serviceType);
            service.Should().BeOfType(implementationType);
        }

        /// <summary>
        /// Asserts that the supplied <see cref="ServiceProvider"/>
        /// cannot resolve the supplied service type to a concrete
        /// implementation.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serviceType">The type of service.</param>
        private static void AssertNoService(
            IServiceProvider serviceProvider,
            Type serviceType)
        {
            var action = () => serviceProvider.GetRequiredService(serviceType);
            var ex = action.Should().ThrowExactly<InvalidOperationException>().Which;
            ex.Message.Should().Contain($"No service for type '{serviceType}' has been registered.");
        }

        private static ServiceProvider BuildServiceProvider(ConsoleGemsOptions options)
        {
            var serviceCollection = new ServiceCollection()
                .AddConsoleGems(options);
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
            ChildMenu childMenu,
            IAutoCompleter autoCompleter,
            IMenuWriter menuWriter,
            IConsole console,
            ApplicationState applicationState)
            : AbstractMenu(
                autoCompleter,
                menuWriter,
                console,
                applicationState)
        {
            public override string Title => "Menu with child menus";

            public override string Description => "A menu with child menus";

            public override List<MenuItem> MenuItems =>
            [
                new () { Key = "a key", Description = "a description", Command = new Mock<ICommand>().Object, },
                new () { Key = "child", Description = "Child menu", Command = childMenu.ShowCommand, },
            ];
        }

        private class ChildMenu(
            IAutoCompleter autoCompleter,
            IMenuWriter menuWriter,
            IConsole console,
            ApplicationState applicationState)
            : AbstractMenu(
                autoCompleter,
                menuWriter,
                console,
                applicationState)
        {
            public override string Title => "Child menu";

            public override string Description => "A child menu";

            public override List<MenuItem> MenuItems =>
            [
                new () { Key = "a key", Description = "a description", Command = new Mock<ICommand>().Object, },
            ];
        }

        private class MenuWriterForTesting(
            ISharedMenuItemsProvider sharedMenuItemsProvider,
            IGlobalMenuItemsProvider globalMenuItemsProvider,
            ITextJustifier textJustifier,
            IConsole console,
            AsciiArtSettings asciiArtSettings,
            ExitCurrentMenuCommand exitCurrentMenuCommand,
            ApplicationState applicationState)
            : MenuWriter(
                sharedMenuItemsProvider,
                globalMenuItemsProvider,
                textJustifier,
                console,
                asciiArtSettings,
                exitCurrentMenuCommand,
                applicationState)
        {
        }

        private class TestKeyPressMappings : IAutoCompleteKeyPressMappings
        {
            public IDictionary<ConsoleKey, IAutoCompleteKeyPressHandler> Mappings => throw new NotImplementedException();

            public IAutoCompleteKeyPressHandler DefaultHandler => throw new NotImplementedException();
        }
    }
}
