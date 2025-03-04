// <copyright file="ConsoleErrorWriter.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Consoles.Concrete
{
    /// <summary>
    /// Basic <see cref="IConsoleErrorWriter"/> implementation, which you
    /// can call directly, or use as a base class for your own implementations.
    /// </summary>
    /// <param name="console">Interface for console interaction.</param>
    public class ConsoleErrorWriter(IConsole console)
        : IConsoleErrorWriter
    {
        /// <summary>
        /// Writes an error message to the console.
        /// </summary>
        /// <param name="message">The error message to write.</param>
        public void WriteError(string message)
        {
            this.BeforeWriteError();
            console.WriteLine(message, ConsoleOutputType.Error);
            this.AfterWriteError();
        }

        /// <summary>
        /// Writes details of an exception to the console.
        /// </summary>
        /// <param name="ex">The exception to write.</param>
        public void WriteException(Exception ex)
        {
            this.WriteError("Unhandled exception:");
            this.BeforeWriteExceptionDetails();
            console.WriteLine(ex.ToString());
            this.AfterWriteExceptionDetails();
        }

        /// <summary>
        /// Called before writing an error message.
        /// </summary>
        protected virtual void BeforeWriteError()
        {
        }

        /// <summary>
        /// Called after writing an error message.
        /// </summary>
        protected virtual void AfterWriteError()
        {
        }

        /// <summary>
        /// Called before writing exception details.
        /// </summary>
        protected virtual void BeforeWriteExceptionDetails()
        {
        }

        /// <summary>
        /// Called after writing exception details.
        /// </summary>
        protected virtual void AfterWriteExceptionDetails()
        {
        }
    }
}
