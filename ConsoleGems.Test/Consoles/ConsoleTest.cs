// <copyright file="ConsoleTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Consoles
{
    /// <summary>
    /// Unit tests for the <see cref="Console"/> class.
    /// </summary>
    public class ConsoleTest
    {
        #region Read tests

        /// <summary>
        /// Tests that the Read method returns the next character
        /// which was input to the console, and that another call
        /// to the read method reads the character after that.
        /// </summary>
        [Fact]
        public void Read_ReadsCorrectCharacters()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new Console();
                using var sr = new StringReader("Hello, world!");
                System.Console.SetIn(sr);

                // Act
                var char1 = console.Read();
                var char2 = console.Read();

                // Assert
                char1.Should().Be('H');
                char2.Should().Be('e');
            }
        }

        #endregion

        #region ReadLine tests

        /// <summary>
        /// Tests that the ReadLine method returns the string which
        /// was input to the console.
        /// </summary>
        [Fact]
        public void ReadLine_ReadsCorrectText()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new Console();
                using var sr = new StringReader("Hello, world!");
                System.Console.SetIn(sr);

                // Act
                var stringRead = console.ReadLine();

                // Assert
                stringRead.Should().Be("Hello, world!");
            }
        }

        /// <summary>
        /// Tests that the ReadLine method returns an empty string when the
        /// input is an empty string.
        /// </summary>
        /// <remarks>
        /// Note that Console.ReadLine returns null when no lines of input
        /// are available.
        /// This test verifies that the IConsoleReader implementation
        /// handles this case correctly.
        /// </remarks>
        [Fact]
        public void ReadLine_InputIsNull_ReturnsEmptyString()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new Console();
                using var sw = new StringReader(string.Empty);
                System.Console.SetIn(sw);

                // Act
                var stringRead = console.ReadLine();

                // Assert
                stringRead.Should().Be(string.Empty);
            }
        }

        #endregion

        #region Write tests

        /// <summary>
        /// Tests that the Write method writes the correct string.
        /// </summary>
        [Fact]
        public void Write_WritesCorrectString()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new Console();
                using var sw = new StringWriter();
                var originalOut = System.Console.Out;
                System.Console.SetOut(sw);

                try
                {
                    // Act
                    console.Write("Hello, world!");

                    // Assert
                    sw.ToString().Should().Be("Hello, world!");
                }
                finally
                {
                    System.Console.SetOut(originalOut);
                }
            }
        }

        /// <summary>
        /// Tests that the Write method writes the correct character.
        /// </summary>
        [Fact]
        public void Write_WritesCorrectCharacter()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new Console();
                using var sw = new StringWriter();
                var originalOut = System.Console.Out;
                System.Console.SetOut(sw);

                try
                {
                    // Act
                    console.Write('H');

                    // Assert
                    sw.ToString().Should().Be("H");
                }
                finally
                {
                    System.Console.SetOut(originalOut);
                }
            }
        }

        #endregion

        #region WriteLine tests

        /// <summary>
        /// Tests that the WriteLine method writes the correct string.
        /// </summary>
        [Fact]
        public void WriteLine_WritesCorrectString()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new Console();
                using var sw = new StringWriter();
                var originalOut = System.Console.Out;
                System.Console.SetOut(sw);

                try
                {
                    // Act
                    console.WriteLine("Hello, world!");

                    // Assert
                    sw.ToString().Should().Be("Hello, world!" + Environment.NewLine);
                }
                finally
                {
                    System.Console.SetOut(originalOut);
                }
            }
        }

        /// <summary>
        /// Tests that the WriteLine method writes the correct character
        /// followed by a line break.
        /// </summary>
        [Fact]
        public void WriteLine_WritesCorrectCharacter()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new Console();
                using var sw = new StringWriter();
                var originalOut = System.Console.Out;
                System.Console.SetOut(sw);

                try
                {
                    // Act
                    console.WriteLine('H');

                    // Assert
                    sw.ToString().Should().Be("H" + Environment.NewLine);
                }
                finally
                {
                    System.Console.SetOut(originalOut);
                }
            }
        }

        #endregion
    }
}