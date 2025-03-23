// <copyright file="ConsoleGemsOptions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems
{
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.AutoComplete.Matchers;
    using Sde.ConsoleGems.Consoles;

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
        [ExcludeFromCodeCoverage(Justification = "Simple get/set property")]
        public bool UseColours { get; set; }

        /// <summary>
        /// Gets the type of the main menu for the application.
        /// Returns null if the application does not use menus.
        /// </summary>
        public Type? MainMenu { get; private set; }

        /// <summary>
        /// Gets the type of <see cref="IMenuWriter"/> to use for
        /// writing menus to the console.
        /// </summary>
        public Type? MenuWriter { get; private set; }

        /// <summary>
        /// Gets the provider of menu items shared across all menus
        /// in the application.
        /// </summary>
        public Type? SharedMenuItemsProvider { get; private set; }

        /// <summary>
        /// Gets the <see cref="AutoCompleteOptions"/> instance which
        /// controls the behaviour of the auto-complete feature.
        /// </summary>
        public AutoCompleteOptions? AutoCompleteOptions { get; private set; }

        /// <summary>
        /// Gets the prompters that the application can use.
        /// </summary>
        public Dictionary<Type, Type> Prompters { get; } = new ();

        /// <summary>
        /// Configures ConsoleGems to use auto-complete.
        /// </summary>
        /// <param name="configure">
        /// An action which returns an <see cref="AutoCompleteOptions"/> instance
        /// defining the auto-complete configuration.
        /// If none is supplied, ConsoleGems will use a default configuration.
        /// </param>
        /// <returns>
        /// The updated options.
        /// </returns>
        public ConsoleGemsOptions UseAutoComplete(Action<AutoCompleteOptions>? configure = default)
        {
            this.AutoCompleteOptions = new AutoCompleteOptions();
            if (configure == null)
            {
                this.AutoCompleteOptions
                    .UseKeyPressMappings<AutoCompleteKeyPressDefaultMappings>()
                    .UseMatcher<StartsWithMatcher>();
            }
            else
            {
                configure(this.AutoCompleteOptions);
            }

            return this;
        }

        /// <summary>
        /// Sets the main menu for the application.
        /// </summary>
        /// <typeparam name="TMenu">The type of the menu.</typeparam>
        /// <returns>The updated options.</returns>
        public ConsoleGemsOptions UseMainMenu<TMenu>()
            where TMenu : IMenu
        {
            this.MainMenu = typeof(TMenu);
            if (this.AutoCompleteOptions == null)
            {
                this.UseAutoComplete();
            }

            return this;
        }

        /// <summary>
        /// Sets the type of <see cref="IMenuWriter"/> to use for writing
        /// menus to the console.
        /// If this method is not called, the built-in <see cref="MenuWriter"/>
        /// will be used.
        /// </summary>
        /// <typeparam name="TMenuWriter">
        /// The type to use for writing menus to the console.
        /// </typeparam>
        /// <returns>The updated options.</returns>
        public ConsoleGemsOptions UseMenuWriter<TMenuWriter>()
            where TMenuWriter : class, IMenuWriter
        {
            this.MenuWriter = typeof(TMenuWriter);
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
            if (this.AutoCompleteOptions == null)
            {
                this.UseAutoComplete();
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
        public ConsoleGemsOptions UseSharedMenuItemsProvider<TSharedMenuItemsProvider>()
            where TSharedMenuItemsProvider : class, ISharedMenuItemsProvider
        {
            this.SharedMenuItemsProvider = typeof(TSharedMenuItemsProvider);
            return this;
        }
    }
}
