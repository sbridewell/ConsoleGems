// <copyright file="TabKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// <see cref="IAutoCompleteKeyPressHandler"/> implementation which handles
    /// the tab key, and implements autocomplete functionarlity.
    /// </summary>
    public class TabKeyPressHandler : IAutoCompleteKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, IAutoCompleter autoCompleter)
        {
            if (autoCompleter.GetCurrentSuggestion() == null)
            {
                autoCompleter.SelectFirstMatchingSuggestion();
            }
            else
            {
                if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift))
                {
                    autoCompleter.SelectPreviousSuggestion();
                }
                else
                {
                    autoCompleter.SelectNextSuggestion();
                }
            }

            var currentSuggestion = autoCompleter.GetCurrentSuggestion();
            if (currentSuggestion != null)
            {
                autoCompleter.ReplaceUserInputWith(currentSuggestion);
            }
        }
    }
}
