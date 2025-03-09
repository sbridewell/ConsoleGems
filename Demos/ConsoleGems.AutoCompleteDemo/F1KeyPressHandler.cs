// <copyright file="F1KeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoCompleteDemo
{
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.AutoComplete.KeyPressHandlers;

    /// <summary>
    /// Handles the F1 key by moving the cursor one character to the left,
    /// but only if the shift key is held down.
    /// </summary>
    public class F1KeyPressHandler : IAutoCompleteKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, IAutoCompleter autoCompleter)
        {
            if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift))
            {
                autoCompleter.MoveCursorLeft();
            }
            else
            {
                var literalKeyHandler = new LiteralKeyPressHandler();
                literalKeyHandler.Handle(keyInfo, autoCompleter);
            }
        }
    }
}
