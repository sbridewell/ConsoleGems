﻿// <copyright file="Console.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Consoles.Concrete
{
    /// <summary>
    /// Implementation of <see cref="IConsole"/> which uses
    /// <see cref="System.Console"/>.
    /// </summary>
    public class Console : IConsole
    {
        #region properties

        /// <inheritdoc/>
        [SupportedOSPlatform("Windows")]
        [ExcludeFromCodeCoverage]
        public bool CursorVisible { get => System.Console.CursorVisible; set => System.Console.CursorVisible = value; }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public int CursorLeft { get => System.Console.CursorLeft; set => System.Console.CursorLeft = value; }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public int CursorTop { get => System.Console.CursorTop; set => System.Console.CursorTop = value; }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public int WindowWidth { get => System.Console.WindowWidth; set => System.Console.WindowWidth = value; }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public int WindowHeight { get => System.Console.WindowHeight; set => System.Console.WindowHeight = value; }

        #endregion

        #region methods

        /// <inheritdoc/>
        public int Read()
        {
            return System.Console.Read();
        }

        /// <inheritdoc/>.
        [ExcludeFromCodeCoverage(Justification = "It doesn't seem possible to unit test Console.ReadKey")]
        public ConsoleKeyInfo ReadKey(bool intercept = false)
        {
            return System.Console.ReadKey(intercept);
        }

        /// <inheritdoc/>.
        public string ReadLine()
        {
            var text = System.Console.ReadLine();
            text ??= string.Empty;
            return text;
        }

        /// <inheritdoc/>
        public virtual void Write(string textToWrite, ConsoleOutputType outputType = default)
        {
            System.Console.Write(textToWrite);
        }

        /// <inheritdoc/>
        public virtual void Write(char characterToWrite, ConsoleOutputType outputType = default)
        {
            System.Console.Write(characterToWrite);
        }

        /// <inheritdoc/>
        public virtual void WriteLine(string textToWrite = "", ConsoleOutputType outputType = default)
        {
            System.Console.WriteLine(textToWrite);
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"CursorLeft: {this.CursorLeft}, CursorTop: {this.CursorTop}";
        }

        #endregion
    }
}
