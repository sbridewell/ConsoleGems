// <copyright file="IAutoCompleteMatcher.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.Matchers
{
    /// <summary>
    /// Interface for selecting the best match from a list of strings
    /// for the text the user has entered so far.
    /// </summary>
    public interface IAutoCompleteMatcher
    {
        /// <summary>
        /// Gets the best match from the supplied list of <paramref name="suggestions"/>
        /// parameter for the text supplied in the <paramref name="userInput"/> parameter.
        /// </summary>
        /// <param name="userInput">The text the user has entered so far.</param>
        /// <param name="suggestions">The suggestions from which to select.</param>
        /// <param name="comparisonType">
        /// Specifies the culture, sort rules and case sensitivity of the match.
        /// </param>
        /// <returns>
        /// The zero-based intex into the suggestions of the best match.
        /// </returns>
        public int FindMatch(
            string userInput,
            List<string> suggestions,
            StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase);
    }
}
