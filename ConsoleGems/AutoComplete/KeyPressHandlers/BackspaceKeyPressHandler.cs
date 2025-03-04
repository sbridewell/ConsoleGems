// <copyright file="BackspaceKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// <see cref="IAutoCompleteKeyPressHandler"/> implementation for handling
    /// the backspace key.
    /// </summary>
    public class BackspaceKeyPressHandler : IAutoCompleteKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, IAutoCompleter autoCompleter)
        {
            if (!autoCompleter.CursorIsAtHome)
            {
                autoCompleter.RemovePreviousCharacterFromUserInput();
                autoCompleter.SelectNoSuggestion();
            }
        }
    }
}
