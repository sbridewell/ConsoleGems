// <copyright file="ConsoleTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Consoles
{
    using System.IO;
    using FluentAssertions;
    using Sde.ConsoleGems.Consoles;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="Sde.ConsoleGems.Consoles.Console"/> class, including all Write overloads.
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

        /// <summary>
        /// Tests that Write(string, ConsoleOutputType) writes the correct string.
        /// </summary>
        [Fact]
        public void Write_String_OutputType_WritesText()
        {
            lock (LockObjects.ConsoleLock)
            {
                var console = new Sde.ConsoleGems.Consoles.Console();
                using var sw = new StringWriter();
                var originalOut = System.Console.Out;
                System.Console.SetOut(sw);
                try
                {
                    console.Write("Hello", ConsoleOutputType.Default);
                    sw.ToString().Should().Be("Hello");
                }
                finally
                {
                    System.Console.SetOut(originalOut);
                }
            }
        }

        /// <summary>
        /// Tests that Write(string, ConsoleColours) writes the correct string.
        /// </summary>
        [Fact]
        public void Write_String_ConsoleColours_WritesText()
        {
            lock (LockObjects.ConsoleLock)
            {
                var console = new Sde.ConsoleGems.Consoles.Console();
                using var sw = new StringWriter();
                var originalOut = System.Console.Out;
                System.Console.SetOut(sw);
                try
                {
                    var colours = new ConsoleColours(ConsoleColor.White, ConsoleColor.Black);
                    console.Write("World", colours);
                    sw.ToString().Should().Be("World");
                }
                finally
                {
                    System.Console.SetOut(originalOut);
                }
            }
        }

        /// <summary>
        /// Tests that Write(char, ConsoleOutputType) writes the correct character.
        /// </summary>
        [Fact]
        public void Write_Char_OutputType_WritesChar()
        {
            lock (LockObjects.ConsoleLock)
            {
                var console = new Sde.ConsoleGems.Consoles.Console();
                using var sw = new StringWriter();
                var originalOut = System.Console.Out;
                System.Console.SetOut(sw);
                try
                {
                    console.Write('X', ConsoleOutputType.Default);
                    sw.ToString().Should().Be("X");
                }
                finally
                {
                    System.Console.SetOut(originalOut);
                }
            }
        }

        /// <summary>
        /// Tests that Write(char, ConsoleColours) writes the correct character.
        /// </summary>
        [Fact]
        public void Write_Char_ConsoleColours_WritesChar()
        {
            lock (LockObjects.ConsoleLock)
            {
                var console = new Sde.ConsoleGems.Consoles.Console();
                using var sw = new StringWriter();
                var originalOut = System.Console.Out;
                System.Console.SetOut(sw);
                try
                {
                    var colours = new ConsoleColours(ConsoleColor.Yellow, ConsoleColor.Blue);
                    console.Write('Y', colours);
                    sw.ToString().Should().Be("Y");
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