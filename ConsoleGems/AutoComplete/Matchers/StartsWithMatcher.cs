// <copyright file="StartsWithMatcher.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.Matchers
{
    /// <summary>
    /// Finds the best match in the suggestions which starts with something like
    /// the user input.
    /// </summary>
    /// <remarks>
    /// If none of the suggestions start with the user input, the matcher will
    /// repeatedly remove the final character from the user input and test again
    /// for a match, until the user input is an empty string, which will result
    /// in -1 being returned.
    /// If the user input is an empty string, then 0, representing the first
    /// suggestion in the list, will be returned.
    /// </remarks>
    public class StartsWithMatcher : IAutoCompleteMatcher
    {
        /// <inheritdoc/>
        public int FindMatch(
            string userInput,
            List<string> suggestions,
            StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            if (userInput == string.Empty)
            {
                return 0;
            }

            var shortenedUserInput = userInput;
            while (shortenedUserInput.Length > 0)
            {
                var matchIndex = suggestions.FindIndex(
                    item => item.StartsWith(shortenedUserInput, comparisonType));
                if (matchIndex >= 0)
                {
                    return matchIndex;
                }

                shortenedUserInput = shortenedUserInput[..^1]; // remove last character
            }

            return -1;
        }
    }
}
