// <copyright file="MenuWriter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Menus
{
    /// <summary>
    /// Writes a menu to the console in verbose format.
    /// </summary>
    public class MenuWriter(
        ISharedMenuItemsProvider sharedMenuItemsProvider,
        IGlobalMenuItemsProvider globalMenuItemsProvider,
        IConsole console,
        ExitCurrentMenuCommand exitCurrentMenuCommand,
        ApplicationState applicationState)
        : IMenuWriter
    {
        /// <summary>
        /// Enumeration of text justification values.
        /// </summary>
        public enum TextJustification
        {
            /// <summary>
            /// Text will not be justified and any background colour will only apply
            /// to characters which are set, i.e. the background colour will not
            /// fill the console width.
            /// </summary>
            None,

            /// <summary>
            /// Text will be left justified and any background colour will fill the
            /// console width.
            /// </summary>
            Left,

            /// <summary>
            /// Text will be centred and any background colour will fill the console
            /// width.
            /// </summary>
            Centre,

            /// <summary>
            /// Text will be right justified and any background colour will fill the
            /// console width.
            /// </summary>
            Right,
        }

        /// <inheritdoc/>
        public void WriteMenu(IMenu menu)
        {
            var sb = new StringBuilder();
            var title = Justify(menu.Title, TextJustification.Centre, console.WindowWidth);
            console.WriteLine(title, ConsoleOutputType.MenuHeader);
            var items = this.GetAllMenuItems(menu);
            var maxKeyWidth = items.Max(i => i.Key.Length);
            foreach (var menuItem in items)
            {
                var keyDisplay = Justify(menuItem.Key, TextJustification.Right, maxKeyWidth);
                var descriptionDisplay = Justify(menuItem.Description, TextJustification.Left, console.WindowWidth - maxKeyWidth - 3);
                console.WriteLine($"{keyDisplay} - {descriptionDisplay}", ConsoleOutputType.MenuBody);
            }

            console.Write(sb.ToString());
        }

        /// <inheritdoc/>
        public List<MenuItem> GetAllMenuItems(IMenu menu)
        {
            List<MenuItem> items =
            [
                .. menu.MenuItems,
                .. sharedMenuItemsProvider.MenuItems,
                .. globalMenuItemsProvider.MenuItems,
            ];

            if (applicationState.MenuDepth > 0)
            {
                items.Add(
                    new MenuItem
                    {
                        Key = "back",
                        Description = "Go back to the previous menu",
                        Command = exitCurrentMenuCommand,
                    });
            }

            return items;
        }

        /// <summary>
        /// Applies the supplied justification to the supplied text.
        /// </summary>
        /// <param name="text">The text to justify.</param>
        /// <param name="justification">The justification to apply.</param>
        /// <param name="availableWidth">The width to justify the text within.</param>
        /// <returns>
        /// The supplied text, padded with spaces to apply the supplied justification.
        /// </returns>
        protected static string Justify(string text, TextJustification justification, int availableWidth)
        {
            // TODO: #10 better formatting of menus
            ArgumentNullException.ThrowIfNull(text);
            text = text.Trim();
            return justification switch
            {
                TextJustification.None => text,
                TextJustification.Left => text.PadRight(availableWidth),
                TextJustification.Centre => text.PadLeft((availableWidth + text.Length) / 2).PadRight(availableWidth),
                TextJustification.Right => text.PadLeft(availableWidth),
                _ => throw new ArgumentOutOfRangeException(
                    nameof(justification),
                    justification,
                    $"The supplied value is not a member of the {nameof(TextJustification)} enum."),
            };
        }
    }
}
