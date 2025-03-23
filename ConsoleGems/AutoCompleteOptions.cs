// <copyright file="AutoCompleteOptions.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems
{
    /// <summary>
    /// Options object which provides a fluent API to configure the behaviour
    /// of as <see cref="IAutoCompleter"/> instance.
    /// </summary>
    public class AutoCompleteOptions
    {
        /// <summary>
        /// Gets the type of <see cref="IAutoCompleteKeyPressMappings"/>
        /// which defines how different key presses are handled.
        /// </summary>
        public Type? KeyPressMappings { get; private set; }

        /// <summary>
        /// Gets the type of <see cref="IAutoCompleteMatcher"/> which
        /// defines how to select the suggestion which best matches
        /// the text entered by the user so far.
        /// </summary>
        public Type? Matcher { get; private set; }

        /// <summary>
        /// Sets the type of <see cref="IAutoCompleteKeyPressMappings"/>
        /// which defines how different key presses are handled.
        /// </summary>
        /// <typeparam name="TMappings">
        /// The type of key press mappings to use.
        /// </typeparam>
        /// <returns>The updated options.</returns>
        public AutoCompleteOptions UseKeyPressMappings<TMappings>()
            where TMappings : IAutoCompleteKeyPressMappings
        {
            this.KeyPressMappings = typeof(TMappings);
            return this;
        }

        /// <summary>
        /// Sets the type of <see cref="IAutoCompleteMatcher"/> which
        /// defines how to select the suggestion which best matches
        /// the text entered by the user so far.
        /// </summary>
        /// <typeparam name="TMatcher">The type of matcher to use.</typeparam>
        /// <returns>The updated options.</returns>
        public AutoCompleteOptions UseMatcher<TMatcher>()
            where TMatcher : IAutoCompleteMatcher
        {
            this.Matcher = typeof(TMatcher);
            return this;
        }
    }
}
