// <copyright file="TextWriterConsole.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.FullScreen
{
    using System.Text;

    /// <summary>
    /// An <see cref="IConsole"/> implementation with a ToString method which returns
    /// what would have been written to the console.
    /// </summary>
    public class TextWriterConsole
        : IConsole
    {
        private char[,] output = new char[0, 0];
        private int windowWidth;
        private int windowHeight;

        /// <inheritdoc/>
        public bool CursorVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <inheritdoc/>
        public int CursorLeft { get; set; }

        /// <inheritdoc/>
        public int CursorTop { get; set; }

        /// <inheritdoc/>
        public int WindowWidth
        {
            get => this.windowWidth;
            set
            {
                this.windowWidth = value;
                this.InitialiseOutput();
            }
        }

        /// <inheritdoc/>
        public int WindowHeight
        {
            get => this.windowHeight;
            set
            {
                this.windowHeight = value;
                this.InitialiseOutput();
            }
        }

        /// <summary>
        /// Gets a value indicating whether a key press is available in the input stream.
        /// </summary>
        public bool KeyAvailable { get => throw new NotImplementedException(); }

        /// <inheritdoc/>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int Read()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ConsoleKeyInfo ReadKey(bool intercept = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Write(string textToWrite, ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            for (var i = 0; i < textToWrite.Length; i++)
            {
                if (this.CursorLeft < this.WindowWidth && this.CursorTop < this.WindowHeight)
                {
                    this.output[this.CursorLeft, this.CursorTop] = textToWrite[i];
                    this.CursorLeft++;
                }
            }
        }

        /// <inheritdoc/>
        public void Write(char characterToWrite, ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            this.output[this.CursorLeft, this.CursorTop] = characterToWrite;
            this.CursorLeft++;
        }

        /// <inheritdoc/>
        public void WriteLine(string textToWrite = "", ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            this.Write(textToWrite + Environment.NewLine, outputType);
        }

        /// <inheritdoc/>
        public void WriteLine(char characterToWrite, ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var y = 0; y < this.windowHeight; y++)
            {
                for (var x = 0; x < this.windowWidth; x++)
                {
                    sb.Append(this.output[x, y]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private void InitialiseOutput()
        {
            this.output = new char[this.windowWidth, this.WindowHeight];
            for (var y = 0; y < this.WindowHeight; y++)
            {
                for (var x = 0; x < this.windowWidth; x++)
                {
                    this.output[x, y] = ' ';
                }
            }
        }
    }
}
