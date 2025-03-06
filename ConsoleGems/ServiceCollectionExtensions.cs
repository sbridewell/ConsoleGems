// <copyright file="ServiceCollectionExtensions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems
{
    using System.Reflection;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            var defaultOptions = new ConsoleGemsOptions { UseColours = true };
            return AddConsoleGems(services, defaultOptions);
        }

        /// <summary>
        /// Adds ConsoleGems functionality to the service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="options">
        /// Options controlling which ConsoleGems functionality to add.
        /// </param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddConsoleGems(this IServiceCollection services, ConsoleGemsOptions options)
        {
            if (options.UseColours)
            {
                services.AddSingleton<IConsole, ColourfulConsole>();
                services.AddSingleton<IConsoleColourManager, ConsoleColourManager>();
            }
            else
            {
                services.AddSingleton<IConsole, Console>();
            }

            services.AddSingleton<IConsoleErrorWriter, ConsoleErrorWriter>();
            services.AddSingleton<ApplicationState>();

            if (options.SharedMenuItemsProvider == null)
            {
                services.AddSingleton<ISharedMenuItemsProvider, EmptySharedMenuItemsProvider>();
            }
            else
            {
                services.AddSingleton(typeof(ISharedMenuItemsProvider), options.SharedMenuItemsProvider);
                services.AddCommands(options.SharedMenuItemsProvider);
            }

            if (options.AutoCompleteKeyPressMappings != null)
            {
                services.AddSingleton<IAutoCompleter, AutoCompleter>();
                services.AddSingleton(typeof(IAutoCompleteKeyPressMappings), options.AutoCompleteKeyPressMappings);

                // TODO: a way in the options to specify a different IAutoCompleter implementation
            }

            if (options.MainMenu != null)
            {
                services.UseMenus().AddMenu(options.MainMenu);
            }

            if (options.Prompters.Any())
            {
                foreach (var prompter in options.Prompters)
                {
                    services.AddSingleton(prompter.Key, prompter.Value);
                }
            }

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
        /// </summary>
        private static IServiceCollection UseMenus(this IServiceCollection services)
        {
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
