// <copyright file="TextJustification.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Text
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
}
