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
        /// Gets the <see cref="IAutoCompleteKeyPressMappings"/> instance
        /// which defines how different key presses are handled.
        /// </summary>
        public IAutoCompleteKeyPressMappings? KeyPressMappings { get; private set; }

        /// <summary>
        /// Gets the type of <see cref="IAutoCompleteMatcher"/> which
        /// defines how to select the suggestion which best matches
        /// the text entered by the user so far.
        /// </summary>
        public Type? Matcher { get; private set; }

        // TODO: #23 make the key press mappings a type parameter

        /// <summary>
        /// Sets the <see cref="IAutoCompleteKeyPressMappings"/> instance
        /// which defines how different key presses are handled.
        /// </summary>
        /// <param name="keyPressMappings">
        /// The <see cref="IAutoCompleteKeyPressMappings"/> instance to use.
        /// </param>
        /// <returns>The updated options.</returns>
        public AutoCompleteOptions UseKeyPressMappings(IAutoCompleteKeyPressMappings keyPressMappings)
        {
            this.KeyPressMappings = keyPressMappings;
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
