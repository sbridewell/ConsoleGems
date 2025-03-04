// <copyright file="DeleteKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// <see cref="IAutoCompleteKeyPressHandler"/> implementation to handle
    /// the delete key.
    /// </summary>
    public class DeleteKeyPressHandler : IAutoCompleteKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, IAutoCompleter autoCompleter)
        {
            if (!autoCompleter.CursorIsAtEnd)
            {
                autoCompleter.RemoveCurrentCharacterFromUserInput();
                autoCompleter.SelectNoSuggestion();
            }
        }
    }
}
