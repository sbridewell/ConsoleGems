// <copyright file="IConsole.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Consoles
{
    /// <summary>
    /// Interface representing <see cref="System.Console"/>.
    /// </summary>
    public interface IConsole
    {
        #region properties

        /// <summary>
        /// Gets or sets a value indicating whether the cursor is visible.
        /// </summary>
        bool CursorVisible { get; set; }

        /// <summary>
        /// Gets or sets the column position of the cursor
        /// within the buffer area.
        /// </summary>
        int CursorLeft { get; set; }

        /// <summary>
        /// Gets or sets the row position of the cursor.
        /// </summary>
        int CursorTop { get; set; }

        /// <summary>
        /// Gets or sets the width of the console window in characters.
        /// </summary>
        int WindowWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the console window in characters.
        /// </summary>
        int WindowHeight { get; set; }

        #endregion

        #region methods

        /// <inheritdoc cref="Console.Read"/>.
        int Read();

        /// <inheritdoc cref="Console.ReadKey(bool)"/>.
        ConsoleKeyInfo ReadKey(bool intercept = false);

        /// <summary>
        /// Reads the next line of characters from the standard input stream.
        /// </summary>
        /// <returns>
        /// The next line of characters from the standard input stream, or
        /// <see cref="string.Empty"/> if no more lines are available.
        /// </returns>
        string ReadLine();

        /// <summary>
        /// Writes the supplied text to the console without a terminating line break.
        /// </summary>
        /// <param name="textToWrite">The text to write.</param>
        /// <param name="outputType">
        /// Indicates the type of output and therefore how it should be formatted.
        /// </param>
        void Write(string textToWrite, ConsoleOutputType outputType = default);

        /// <summary>
        /// Writes the supplied character to the console without a terminating line break.
        /// </summary>
        /// <param name="characterToWrite">The character to write.</param>
        /// <param name="outputType">
        /// Indicates the type of output and therefore how it should be formatted.
        /// </param>
        void Write(char characterToWrite, ConsoleOutputType outputType = default);

        /// <summary>
        /// Writes the supplied text to the console with a terminating line break.
        /// </summary>
        /// <param name="textToWrite">The text to write.</param>
        /// <param name="outputType">
        /// Indicates the type of output and therefore how it should be formatted.
        /// </param>
        void WriteLine(string textToWrite = "", ConsoleOutputType outputType = default);

        /// <summary>
        /// Writes the supplied character to the console with a terminating line break.
        /// </summary>
        /// <param name="characterToWrite">The character to write.</param>
        /// <param name="outputType">
        /// Indicates the type of output and therefore how it should be formatted.
        /// </param>
        void WriteLine(char characterToWrite, ConsoleOutputType outputType = default);

        #endregion
    }
}
