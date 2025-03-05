// <copyright file="ServiceCollectionExtensions.cs" company="Simon Bridewell">
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
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds basic ConsoleGems functionality to the service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddConsoleGems(this IServiceCollection services)
        {
            services.AddSingleton<IConsole, Console>();
            services.AddSingleton<IConsoleErrorWriter, ConsoleErrorWriter>();
            services.AddSingleton<ApplicationState>();
            services.AddSingleton<ISharedMenuItemsProvider, EmptySharedMenuItemsProvider>();
            return services;
        }

        // TODO: consider replacing the remaining extension methods with some kind of configuration object
        // e.g. services.UseConsoleGems(options => options.UseColours().UseAutoComplete().UsePrompters().AddSharedMenuItems<TSharedMenuItemsProvider>().SetMainMenu<TMenu>())

        /// <summary>
        /// Registers the necessary dependencies for a colourful console application.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection UseColours(this IServiceCollection services)
        {
            services.AddSingleton<IConsoleColourManager, ConsoleColourManager>();
            services.AddSingleton<IConsole, ColourfulConsole>();
            return services;
        }

        /// <summary>
        /// Registers the necessary dependencies for a console application with auto-complete functionality.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection UseAutoComplete(this IServiceCollection services)
        {
            services.AddSingleton<IAutoCompleter, AutoCompleter>();
            services.AddSingleton<IAutoCompleteKeyPressMappings, AutoCompleteKeyPressDefaultMappings>();
            return services;
        }

        /// <summary>
        /// Registers the necessary dependencies for a console application with prompters.
        /// Calls the <see cref="UseAutoComplete"/> method so you don't need to.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection UsePrompters(this IServiceCollection services)
        {
            services.UseAutoComplete();
            services.AddSingleton<IPrompter<bool?>, BooleanPrompter>();
            services.AddSingleton<IFilePrompter, FilePrompter>();
            services.AddSingleton<IDirectoryPrompter, DirectoryPrompter>();
            return services;
        }

        /// <summary>
        /// Adds menu items which are shared between all menus in the current application.
        /// Calls the <see cref="AddCommands"/> method so you don't need to.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <typeparam name="TSharedMenuItemsProvider">
        /// <see cref="ISharedMenuItemsProvider"/> instance which provides the shared
        /// menu items.
        /// </typeparam>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddSharedMenuItems<TSharedMenuItemsProvider>(this IServiceCollection services)
            where TSharedMenuItemsProvider : class, ISharedMenuItemsProvider
        {
            services.AddSingleton<ISharedMenuItemsProvider, TSharedMenuItemsProvider>();
            services.AddCommands(typeof(TSharedMenuItemsProvider));
            return services;
        }

        /// <summary>
        /// Sets the main menu for the application.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <typeparam name="TMenu">The type of the main menu.</typeparam>
        /// <returns>The service collection.</returns>
        public static IServiceCollection SetMainMenu<TMenu>(this IServiceCollection services)
            where TMenu : class, IMenu
        {
            services.UseMenus();
            services.AddSingleton<TMenu>();
            services.AddMenu(typeof(TMenu));
            return services;
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
        /// <param name="services">Service collection.</param>
        /// <param name="menuType">The type of menu to add.</param>
        /// <returns>The service collection.</returns>
        private static IServiceCollection AddMenu(this IServiceCollection services, Type menuType)
        {
            services.AddSingleton(menuType);
            services.AddCommands(menuType);
            services.AddChildMenus(menuType);
            return services;
        }

        /// <summary>
        /// Adds the commands which are dependencies of the supplied type.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="dependentType">The type which depends on some commands.</param>
        /// <returns>The service collection.</returns>
        private static IServiceCollection AddCommands(this IServiceCollection services, Type dependentType)
        {
            var constructorParams = GetSoleConstructorParameters(dependentType);
            var dependencies = constructorParams.Where(p => p.ParameterType.IsAssignableTo(typeof(ICommand)));
            foreach (var dependency in dependencies)
            {
                services.AddSingleton(dependency.ParameterType);
            }

            return services;
        }

        /// <summary>
        /// Recursively adds the child menus of the supplied menu type.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="menuType">
        /// The menu type for which to add the child menus.
        /// </param>
        /// <returns>The service collection.</returns>
        private static IServiceCollection AddChildMenus(this IServiceCollection services, Type menuType)
        {
            var constructorParams = GetSoleConstructorParameters(menuType);
            var childMenus = constructorParams.Where(p => p.ParameterType.IsAssignableTo(typeof(IMenu)));
            foreach (var childMenu in childMenus)
            {
                services.AddMenu(childMenu.ParameterType);
            }

            return services;
        }

        /// <summary>
        /// Registers the necessary dependencies for a console application with menus.
        /// Calls the <see cref="UsePrompters"/> method so you don't need to.
        /// </summary>
        private static IServiceCollection UseMenus(this IServiceCollection services)
        {
            services.UsePrompters();
            services.AddSingleton<IMenuWriter, MenuWriter>();
            services.AddSingleton<IGlobalMenuItemsProvider, GlobalMenuItemsProvider>();
            services.AddCommands(typeof(GlobalMenuItemsProvider));

            // TODO: Need to keep this for now because exit current menu is not a menu item in GlobalMenuItemsProvider
            // instead it's added dynamically if the menu stack is greater than 1
            services.AddSingleton<ExitCurrentMenuCommand>();
            return services;
        }
    }
}
