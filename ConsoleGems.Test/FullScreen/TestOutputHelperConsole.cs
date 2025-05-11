// <copyright file="TestOutputHelperConsole.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.FullScreen
{
    using Xunit.Abstractions;

    /// <summary>
    /// <see cref="IConsole"/> implementation which writes to the test
    /// output window using Xunit's ITestOutputHelper.
    /// </summary>
    public class TestOutputHelperConsole(ITestOutputHelper output, ConsoleSize windowSize)
        : IConsole
    {
        private int cursorTop;
        private int cursorLeft;

        /// <summary>
        /// Gets the screen buffer.
        /// </summary>
        public string[] ScreenBuffer { get; } = new string[windowSize.Height];

        /// <summary>
        /// Gets or sets a value indicating whether the cursor is visible.
        /// Does nothing in this implementation.
        /// </summary>
        public bool CursorVisible { get => false; set => _ = value; }

        #region cursor position properties

        /// <summary>
        /// Gets or sets the zero-base horizontal position of the cursor.
        /// </summary>
        public int CursorLeft
        {
            get => this.cursorLeft;
            set
            {
                if (value < 0 || value >= this.WindowWidth)
                {
                    var msg = $"CursorLeft {value} is outside the bounds of the console window. " +
                        $"Must be between 0 and {this.WindowWidth - 1}.";
                    throw new ArgumentOutOfRangeException(nameof(value), msg);
                }

                this.cursorLeft = value;
            }
        }

        /// <summary>
        /// Gets or sets the zero-based vertical position of the cursor.
        /// </summary>
        public int CursorTop
        {
            get => this.cursorTop;
            set
            {
                if (value < 0 || value >= this.WindowHeight)
                {
                    var msg = $"CursorTop {value} is outside the bounds of the console window. " +
                        $"Must be between 0 and {this.WindowHeight - 1}.";
                    throw new ArgumentOutOfRangeException(nameof(value), msg);
                }

                this.cursorTop = value;
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the window width.
        /// The setter does nothing in this implementation.
        /// </summary>
        public int WindowWidth { get => windowSize.Width; set => _ = value; }

        /// <summary>
        /// Gets or sets the window height.
        /// The setter does nothing in this implementation.
        /// </summary>
        public int WindowHeight { get => windowSize.Height; set => _ = value; }

        #region Read methods - not implemented

        /// <summary>
        /// Reads the next character from the console.
        /// </summary>
        /// <returns>Not implemented.</returns>
        public int Read()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the next keypress from the console.
        /// </summary>
        /// <param name="intercept">True to intercept the keypress.</param>
        /// <returns>Not implemented.</returns>
        public ConsoleKeyInfo ReadKey(bool intercept = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the next line from the console.
        /// </summary>
        /// <returns>Not implemented.</returns>
        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Writes the supplied text to the console without a terminating line break.
        /// This is not written to the test output window until the <see cref="Flush"/> method
        /// is called.
        /// </summary>
        /// <param name="textToWrite">The text to write.</param>
        /// <param name="outputType">The parameter is not used.</param>
        public void Write(string textToWrite, ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            this.ThrowIfNotInitialised();
            for (var y = 0; y < textToWrite.Length; y++)
            {
                this.Write(textToWrite[y], outputType);
            }
        }

        /// <summary>
        /// Writes the supplied character to the screen buffer without a terminating line break.
        /// This is not written to the test output window until the <see cref="Flush"/> method
        /// is called.
        /// </summary>
        /// <param name="characterToWrite">The character to write.</param>
        /// <param name="outputType">The parameter is not used.</param>
        public void Write(char characterToWrite, ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            this.ThrowIfNotInitialised();
            var tempString = this.ScreenBuffer[this.CursorTop].Insert(this.CursorLeft, characterToWrite.ToString());
            this.ScreenBuffer[this.CursorTop] = tempString.Remove(this.WindowWidth);
            if (this.CursorLeft == this.WindowWidth - 1)
            {
                this.CursorLeft = 0;
                if (this.CursorTop == this.WindowHeight - 1)
                {
                    this.CursorTop = 0;
                }
                else
                {
                    this.CursorTop++;
                }
            }
            else
            {
                this.CursorLeft++;
            }
        }

        #region WriteLine methods - not implemented

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="textToWrite">The text to write.</param>
        /// <param name="outputType">The parameter is not used.</param>
        public void WriteLine(string textToWrite = "", ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="characterToWrite">The character to write.</param>
        /// <param name="outputType">The parameter is not used.</param>
        public void WriteLine(char characterToWrite, ConsoleOutputType outputType = ConsoleOutputType.Default)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Clears the screen buffer.
        /// This must be called before calling any of the write methods.
        /// </summary>
        public void Clear()
        {
            for (var y = 0; y < this.WindowHeight; y++)
            {
                this.ScreenBuffer[y] = new string(' ', this.WindowWidth);
            }
        }

        /// <summary>
        /// Writes the contents of the screen buffer to the test output window.
        /// </summary>
        public void Flush()
        {
            this.ThrowIfNotInitialised();
            foreach (var line in this.ScreenBuffer)
            {
                output.WriteLine(line);
            }
        }

        private void ThrowIfNotInitialised()
        {
            if (this.ScreenBuffer.Any(line => line == null))
            {
                var msg = $"The Clear method of {this.GetType().Name} must be called before calling any other methods";
                throw new InvalidOperationException(msg);
            }
        }
    }
}
