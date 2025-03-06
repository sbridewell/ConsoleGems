// <copyright file="ConsoleGemsOptions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems
{
    using Sde.ConsoleGems.AutoComplete;

    /// <summary>
    /// Options for configuring the behaviour of the ConsoleGems library.
    /// </summary>
    public class ConsoleGemsOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether to use colours
        /// in the console window.
        /// If true then operations which use <see cref="IConsole"/> to
        /// write to the console window can use members of the
        /// <see cref="ConsoleOutputType"/> enumeration to specify the
        /// type of output, and therefore its foreground and background
        /// colours.
        /// Mappings of <see cref="ConsoleOutputType"/> values to colours
        /// can be overridden using the properties of <see cref="ConsoleColours"/>.
        /// </summary>
        public bool UseColours { get; set; }

        /// <summary>
        /// Gets the key press mappings to be used during auto-complete
        /// operations.
        /// Returns null if auto-complete is not enabled.
        /// </summary>
        public IAutoCompleteKeyPressMappings? AutoCompleteKeyPressMappings { get; private set; }

        /// <summary>
        /// Gets the main menu for the application.
        /// Returns null if the application does not use menus.
        /// </summary>
        public Type? MainMenu { get; private set; }

        /// <summary>
        /// Gets the provider of menu items shared across all menus
        /// in the application.
        /// </summary>
        public Type? SharedMenuItemsProvider { get; private set; }

        /// <summary>
        /// Gets the prompters that the application can use.
        /// </summary>
        public Dictionary<Type, Type> Prompters { get; } = new ();

        /// <summary>
        /// Adds auto-complete support to the application using the
        /// default key press mappings.
        /// This will register an <see cref="IAutoCompleter"/> with dependency
        /// injection.
        /// </summary>
        /// <returns>The updated options.</returns>
        public ConsoleGemsOptions UseAutoComplete()
        {
            this.UseAutoComplete(new AutoCompleteKeyPressDefaultMappings());
            return this;
        }

        /// <summary>
        /// Adds auto-complete support to the application using the
        /// supplied key press mappings.
        /// </summary>
        /// <param name="autoCompleteKeyPressMappings">
        /// The key press mappings to use.
        /// </param>
        /// <returns>The update options.</returns>
        public ConsoleGemsOptions UseAutoComplete(IAutoCompleteKeyPressMappings autoCompleteKeyPressMappings)
        {
            this.AutoCompleteKeyPressMappings = autoCompleteKeyPressMappings;
            return this;
        }

        /// <summary>
        /// Sets the main menu for the application.
        /// </summary>
        /// <typeparam name="TMenu">The type of the menu.</typeparam>
        /// <returns>The updated options.</returns>
        public ConsoleGemsOptions SetMainMenu<TMenu>()
            where TMenu : IMenu
        {
            this.MainMenu = typeof(TMenu);
            return this;
        }

        /// <summary>
        /// Adds the built-in <see cref="IPrompter"/> implementations
        /// to the application. These are:
        /// <list type="bullet">
        /// <item><see cref="IBooleanPrompter"/></item>
        /// <item><see cref="IFilePrompter"/></item>
        /// <item><see cref="IDirectoryPrompter"/></item>
        /// </list>
        /// </summary>
        /// <returns>The updated options.</returns>
        public ConsoleGemsOptions UseBuiltInPrompters()
        {
            this.AddPrompter<IBooleanPrompter, BooleanPrompter>();
            this.AddPrompter<IFilePrompter, FilePrompter>();
            this.AddPrompter<IDirectoryPrompter, DirectoryPrompter>();
            return this;
        }

        /// <summary>
        /// Adds the supplied <see cref="IPrompter"/> implementation
        /// to the application.
        /// </summary>
        /// <typeparam name="TService">Interface for the prompter.</typeparam>
        /// <typeparam name="TImplementation">Implementation of the prompter.</typeparam>
        /// <returns>The updated options.</returns>
        public ConsoleGemsOptions AddPrompter<TService, TImplementation>()
            where TService : IPrompter
            where TImplementation : class, TService
        {
            if (this.AutoCompleteKeyPressMappings == null)
            {
                this.UseAutoComplete(new AutoCompleteKeyPressDefaultMappings());
            }

            this.Prompters.Add(typeof(TService), typeof(TImplementation));
            return this;
        }

        /// <summary>
        /// Adds the supplied <see cref="ISharedMenuItemsProvider"/> implementation
        /// to the application.
        /// The menu items returned by the provider will be shared across all menus
        /// in the application.
        /// </summary>
        /// <typeparam name="TSharedMenuItemsProvider">
        /// The type of <see cref="ISharedMenuItemsProvider"/> to add.
        /// </typeparam>
        /// <returns>The updated options.</returns>
        public ConsoleGemsOptions AddSharedMenuItemsProvider<TSharedMenuItemsProvider>()
            where TSharedMenuItemsProvider : class, ISharedMenuItemsProvider
        {
            this.SharedMenuItemsProvider = typeof(TSharedMenuItemsProvider);
            return this;
        }
    }
}
