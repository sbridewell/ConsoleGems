// <copyright file="IAutoCompleteKeyPressHandler.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoComplete.KeyPressHandlers
{
    /// <summary>
    /// Interface for handling a keypress for an auto-complete console.
    /// </summary>
    public interface IAutoCompleteKeyPressHandler
    {
        /// <summary>
        /// Handles the supplied keypress before it reaches the console.
        /// </summary>
        /// <param name="keyInfo">Represents the keypress.</param>
        /// <param name="autoCompleter">
        /// AutoCompleter which orchestrates the handling of keypresses
        /// and builds the text entered or autocompleted by the user.
        /// </param>
        void Handle(ConsoleKeyInfo keyInfo, IAutoCompleter autoCompleter);
    }
}
