// <copyright file="VKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// <see cref="IAutoCompleteKeyPressHandler"/> implementation for handling
    /// the V key.
    /// </summary>
    /// <remarks>
    /// This is implemented so that ctrl + V can be used to paste text into the console.
    /// </remarks>
    public class VKeyPressHandler : IAutoCompleteKeyPressHandler
    {
        /// <inheritdoc/>
        public void Handle(ConsoleKeyInfo keyInfo, IAutoCompleter autoCompleter)
        {
            if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control))
            {
                // ClipboardService comes from the TextCopy NuGet package
                var clipboardText = TextCopy.ClipboardService.GetText();
                if (!string.IsNullOrEmpty(clipboardText))
                {
                    autoCompleter.InsertUserInput(clipboardText);
                    autoCompleter.SelectNoSuggestion();
                }
            }
            else
            {
                var literalKeyHandler = new LiteralKeyPressHandler();
                literalKeyHandler.Handle(keyInfo, autoCompleter);
            }
        }
    }
}
