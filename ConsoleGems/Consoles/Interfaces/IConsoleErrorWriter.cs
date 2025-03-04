// <copyright file="IConsoleErrorWriter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Consoles.Interfaces
{
    /// <summary>
    /// Interface for writing error information to the console.
    /// </summary>
    public interface IConsoleErrorWriter
    {
        /// <summary>
        /// Writes an error message to the console.
        /// </summary>
        /// <param name="message">The error message to write.</param>
        void WriteError(string message);

        /// <summary>
        /// Writes information about an exception to the console.
        /// </summary>
        /// <param name="ex">
        /// The exception to write information about.
        /// </param>
        void WriteException(Exception ex);
    }
}
