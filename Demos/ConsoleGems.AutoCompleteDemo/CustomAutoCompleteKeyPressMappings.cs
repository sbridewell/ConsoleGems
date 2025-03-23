// <copyright file="CustomAutoCompleteKeyPressMappings.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.AutoCompleteDemo
{
    using Sde.ConsoleGems.AutoComplete;
    using Sde.ConsoleGems.AutoComplete.KeyPressHandlers;

    /// <summary>
    /// A completely counter-intuitive set of keypress mappings.
    /// Press shift-F1 to move the cursor left and shift-F2 to
    /// move the cursor right.
    /// Other keys such as home, end, backspace and delete are
    /// not mapped.
    /// </summary>
    public class CustomAutoCompleteKeyPressMappings : IAutoCompleteKeyPressMappings
    {
        private readonly IDictionary<ConsoleKey, IAutoCompleteKeyPressHandler> mappings
            = new Dictionary<ConsoleKey, IAutoCompleteKeyPressHandler>
        {
            // These mappings are the same as the defaults
            { ConsoleKey.LeftArrow, new ArrowKeyPressHandler() },
            { ConsoleKey.RightArrow, new ArrowKeyPressHandler() },
            { ConsoleKey.Backspace, new BackspaceKeyPressHandler() },
            { ConsoleKey.Delete, new DeleteKeyPressHandler() },
            { ConsoleKey.Home, new HomeEndKeyPressHandler() },
            { ConsoleKey.End, new HomeEndKeyPressHandler() },
            { ConsoleKey.Tab, new TabKeyPressHandler() },
            { ConsoleKey.V, new VKeyPressHandler() },

            // These mappings are additional in this class
            { ConsoleKey.F1, new F1KeyPressHandler() },
            { ConsoleKey.F2, new F2KeyPressHandler() },
        };

        /// <inheritdoc/>
        public IDictionary<ConsoleKey, IAutoCompleteKeyPressHandler> Mappings => this.mappings;

        /// <inheritdoc/>
        public IAutoCompleteKeyPressHandler DefaultHandler => new LiteralKeyPressHandler();
    }
}
