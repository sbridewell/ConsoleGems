// <copyright file="LiteralKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// <see cref="IAutoCompleteKeyPressHandler"/> implementation, which
    /// treats all keypresses as a literal character to be inserted into
    /// the user input and displayed on the console.
    /// </summary>
    public class LiteralKeyPressHandler : IAutoCompleteKeyPressHandler
    {
        /// <inheritdoc />
        public virtual void Handle(ConsoleKeyInfo keyInfo, IAutoCompleter autoCompleter)
        {
            var key = keyInfo.KeyChar;
            if (key == 0)
            {
                // It's a key which doesn't represent a printable character
                return;
            }

            autoCompleter.InsertUserInput(key);
            autoCompleter.SelectNoSuggestion();
        }
    }
}
