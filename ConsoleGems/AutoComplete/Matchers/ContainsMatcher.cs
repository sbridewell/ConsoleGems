// <copyright file="ContainsMatcher.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.Matchers
{
    /// <summary>
    /// Finds the best match in the suggestions which contains the
    /// user input.
    /// </summary>
    /// <remarks>
    /// It would be possible to implement a more sophisticated matching
    /// algorithm which tries to find a match by removing characters from
    /// the start and end of the user input, but tha's not a priority
    /// for now.
    /// </remarks>
    public class ContainsMatcher : IAutoCompleteMatcher
    {
        /// <inheritdoc/>
        public int FindMatch(
            string userInput,
            List<string> suggestions,
            StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            return suggestions.FindIndex(
                item => item.Contains(userInput, comparisonType));
        }
    }
}
