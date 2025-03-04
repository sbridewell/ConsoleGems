// <copyright file="ConsoleApplicationBuilder.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Sde.ConsoleGems.Menus;

    /// <summary>
    /// Class to simplify registering the necessary dependencies.
    /// </summary>
    public class ConsoleApplicationBuilder
    {
        private readonly IServiceCollection serviceCollection;
        private ServiceProvider? serviceProvider;
        private Type? mainMenuType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleApplicationBuilder"/> class.
        /// </summary>
        public ConsoleApplicationBuilder()
        {
            this.serviceCollection = new ServiceCollection();
            this.serviceCollection.AddSingleton<IConsole, Console>();
            this.serviceCollection.AddSingleton<IConsoleErrorWriter, ConsoleErrorWriter>();
            this.serviceCollection.AddSingleton<ApplicationState>();
            this.serviceCollection.AddSingleton<ISharedMenuItemsProvider, EmptySharedMenuItemsProvider>();
        }

        /// <summary>
        /// Gets the service collection.
        /// </summary>
        /// <remarks>
        /// Use this property's AddSingleton etc methods to register additional dependencies
        /// which are not supported by <see cref="ConsoleApplicationBuilder"/>.
        /// </remarks>
        [ExcludeFromCodeCoverage]
        public IServiceCollection ServiceCollection => this.serviceCollection;

        /// <summary>
        /// Registers the necessary dependencies for a colourful console application.
        /// </summary>
        /// <returns>The current instance.</returns>
        public ConsoleApplicationBuilder UseColours()
        {
            this.serviceCollection.AddSingleton<IConsoleColourManager, ConsoleColourManager>();
            this.serviceCollection.AddSingleton<IConsole, ColourfulConsole>();
            return this;
        }

        /// <summary>
        /// Registers the necessary dependencies for a console application with auto-complete functionality.
        /// </summary>
        /// <returns>The current instance.</returns>
        public ConsoleApplicationBuilder UseAutoComplete()
        {
            this.serviceCollection.AddSingleton<IAutoCompleter, AutoCompleter>();
            this.serviceCollection.AddSingleton<IAutoCompleteKeyPressMappings, AutoCompleteKeyPressDefaultMappings>();
            return this;
        }

        /// <summary>
        /// Registers the necessary dependencies for a console application with prompters.
        /// Calls the <see cref="UseAutoComplete"/> method so you don't need to.
        /// </summary>
        /// <returns>The current instance.</returns>
        public ConsoleApplicationBuilder UsePrompters()
        {
            this.UseAutoComplete();
            this.serviceCollection.AddSingleton<IPrompter<bool?>, BooleanPrompter>();
            this.serviceCollection.AddSingleton<IFilePrompter, FilePrompter>();
            this.serviceCollection.AddSingleton<IDirectoryPrompter, DirectoryPrompter>();
            return this;
        }

        /// <summary>
        /// Adds menu items which are shared between all menus in the current application.
        /// </summary>
        /// <typeparam name="TSharedMenuItemsProvider">
        /// <see cref="ISharedMenuItemsProvider"/> instance which provides the shared
        /// menu items.
        /// </typeparam>
        /// <returns>The current instance.</returns>
        public ConsoleApplicationBuilder AddSharedMenuItems<TSharedMenuItemsProvider>()
            where TSharedMenuItemsProvider : class, ISharedMenuItemsProvider
        {
            this.serviceCollection.AddSingleton<ISharedMenuItemsProvider, TSharedMenuItemsProvider>();
            this.AddCommands(typeof(TSharedMenuItemsProvider));
            return this;
        }

        /// <summary>
        /// Adds a menu to the application.
        /// Calls the <see cref="UsePrompters"/> method so you don't need to.
        /// </summary>
        /// <typeparam name="TMenu">The type of menu to add.</typeparam>
        /// <returns>The current instance.</returns>
        public ConsoleApplicationBuilder SetMainMenu<TMenu>()
            where TMenu : class, IMenu
        {
            this.UseMenus();
            this.mainMenuType = typeof(TMenu);
            this.AddMenu(typeof(TMenu));
            return this;
        }

        /// <summary>
        /// Builds the service provider, which can be used to explicitly obtain
        /// specific dependencies.
        /// </summary>
        /// <returns>The service provider.</returns>
        public ServiceProvider BuildServiceProvider()
        {
            this.serviceProvider = this.serviceCollection.BuildServiceProvider();
            return this.serviceProvider;
        }

        /// <summary>
        /// Shows the main menu.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="SetMainMenu"/> method has not been called.
        /// -- or --
        /// The <see cref="BuildServiceProvider"/> method has not been called.
        /// </exception>
        public void ShowMainMenu()
        {
            if (this.mainMenuType == null)
            {
                throw new InvalidOperationException($"Main menu has not been set. Call the {nameof(this.SetMainMenu)} method before calling the {nameof(this.ShowMainMenu)} method.");
            }

            if (this.serviceProvider == null)
            {
                throw new InvalidOperationException($"Service provider has not been built. Call the {nameof(this.BuildServiceProvider)} method before calling the {nameof(this.ShowMainMenu)} method.");
            }

            var mainMenu = (IMenu)this.serviceProvider.GetRequiredService(this.mainMenuType);
            mainMenu.ShowCommand.Execute();
        }

        /// <summary>
        /// Gets the parameters of the sole constructor of the supplied type.
        /// </summary>
        /// <param name="t">The type to get the constructor parameters of.</param>
        /// <returns>The parameters of the constructor.</returns>
        /// <remarks>
        /// This only works with types which have exactly one public instance constructor.
        /// This should be fine because <see cref="IMenu"/> and <see cref="ICommand"/>
        /// implementations should only need one constructor, the parameters of which are
        /// supplied by dependency injection.
        /// </remarks>
        private static ParameterInfo[] GetSoleConstructorParameters(Type t)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var constructor = t.GetConstructors(flags)[0];
            var constructorParams = constructor.GetParameters();
            return constructorParams;
        }

        /// <summary>
        /// Adds the supplied menu and child menus and their commands.
        /// </summary>
        /// <param name="menuType">The type of menu to add.</param>
        private void AddMenu(Type menuType)
        {
            this.serviceCollection.AddSingleton(menuType);
            this.AddCommands(menuType);
            this.AddChildMenus(menuType);
        }

        /// <summary>
        /// Adds the commands which are dependencies of the supplied type.
        /// </summary>
        /// <param name="dependentType">The type which depends on some commands.</param>
        private void AddCommands(Type dependentType)
        {
            var constructorParams = GetSoleConstructorParameters(dependentType);
            var dependencies = constructorParams.Where(p => p.ParameterType.IsAssignableTo(typeof(ICommand)));
            foreach (var dependency in dependencies)
            {
                this.serviceCollection.AddSingleton(dependency.ParameterType);
            }
        }

        /// <summary>
        /// Recursively adds the child menus of the supplied menu type.
        /// </summary>
        /// <param name="menuType">
        /// The menu type for which to add the child menus.
        /// </param>
        private void AddChildMenus(Type menuType)
        {
            var constructorParams = GetSoleConstructorParameters(menuType);
            var childMenus = constructorParams.Where(p => p.ParameterType.IsAssignableTo(typeof(IMenu)));
            foreach (var childMenu in childMenus)
            {
                this.AddMenu(childMenu.ParameterType);
            }
        }

        /// <summary>
        /// Registers the necessary dependencies for a console application with menus.
        /// Calls the <see cref="UsePrompters"/> method so you don't need to.
        /// </summary>
        private void UseMenus()
        {
            this.UsePrompters();
            this.serviceCollection.AddSingleton<IMenuWriter, MenuWriter>();
            this.serviceCollection.AddSingleton<IGlobalMenuItemsProvider, GlobalMenuItemsProvider>();
            this.AddCommands(typeof(GlobalMenuItemsProvider));

            // TODO: Need to keep this for now because exit current menu is not a menu item in GlobalMenuItemsProvider
            // instead it's added dynamically if the menu stack is greater than 1
            this.serviceCollection.AddSingleton<ExitCurrentMenuCommand>();
        }
    }
}
