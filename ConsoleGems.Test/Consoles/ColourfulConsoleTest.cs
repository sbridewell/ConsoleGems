// <copyright file="ColourfulConsoleTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Consoles
{
    /// <summary>
    /// Unit tests for the <see cref="ColourfulConsole"/> class.
    /// </summary>
    public class ColourfulConsoleTest : ConsoleTest
    {
        private readonly Mock<IConsoleColourManager> mockConsoleColourManager = new ();

        /// <summary>
        /// Gets the expected relationships between
        /// <see cref="ConsoleOutputType"/> members and the
        /// corresponding <see cref="ConsoleColours"/>.
        /// </summary>
        public static TheoryData<ConsoleOutputType, ConsoleColours> OutputTypesAndColours => new ()
        {
            { ConsoleOutputType.Default, ConsoleColours.Default },
            { ConsoleOutputType.Prompt, ConsoleColours.Prompt },
            { ConsoleOutputType.UserInput, ConsoleColours.UserInput },
            { ConsoleOutputType.Error, ConsoleColours.Error },
            { ConsoleOutputType.MenuHeader, ConsoleColours.MenuHeader },
            { ConsoleOutputType.MenuBody, ConsoleColours.MenuBody },
        };

        /// <summary>
        /// Tests that the console colours are set to those for user input
        /// and then back to the default when the Read method is called.
        /// </summary>
        [Fact]
        public void Read_SetsCorrectConsoleColours()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new ColourfulConsole(this.mockConsoleColourManager.Object);
                var sr = new StringReader("Hello, world!");
                System.Console.SetIn(sr);

                // Act
                console.Read();

                // Assert
                this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.UserInput), Times.Once);
                this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.Default), Times.Once);
            }
        }

        /// <summary>
        /// Tests that the console colours are set to those for user input
        /// and then back to the default when the ReadLine method is called.
        /// </summary>
        [Fact]
        public void ReadLine_SetsCorrectConsoleColours()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new ColourfulConsole(this.mockConsoleColourManager.Object);
                var sr = new StringReader("Hello, world!");
                System.Console.SetIn(sr);

                // Act
                console.ReadLine();

                // Assert
                this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.UserInput), Times.Once);
                this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.Default), Times.Once);
            }
        }

        /// <summary>
        /// Tests that the correct <see cref="ConsoleOutputType"/> is passed to the
        /// <see cref="ConsoleColourManager"/> when none is supplied to the Write
        /// method.
        /// </summary>
        [Fact]
        public void Write_NoConsoleOutputTypeSupplied_UsesDefault()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new ColourfulConsole(this.mockConsoleColourManager.Object);

                // Act
                console.Write("Hello, world!");

                // Assert
                this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.Default), Times.Exactly(2));
            }
        }

        /// <summary>
        /// Tests that the correct <see cref="ConsoleOutputType"/> is passed to the
        /// <see cref="ConsoleColourManager"/> when none is supplied to the WriteLine
        /// method.
        /// </summary>
        [Fact]
        public void WriteLine_NoConsoleOutputTypeSupplied_UsesDefault()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new ColourfulConsole(this.mockConsoleColourManager.Object);

                // Act
                console.WriteLine("Hello, world!");

                // Assert
                this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.Default), Times.Exactly(2));
            }
        }

        /// <summary>
        /// Tests that the Write method uses the correct colours for the
        /// supplied <see cref="ConsoleOutputType"/>.
        /// </summary>
        /// <param name="outputType">The output type.</param>
        /// <param name="colours">The expected colours.</param>
        [Theory]
        [MemberData(nameof(OutputTypesAndColours))]
        public void Write_UsesColoursWithSameNameAsConsoleOutputTypeAndResetsColours(
            ConsoleOutputType outputType,
            ConsoleColours colours)
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new ColourfulConsole(this.mockConsoleColourManager.Object);

                // Act
                console.Write("Hello, world!", outputType);

                // Assert
                if (outputType == ConsoleOutputType.Default)
                {
                    this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.Default), Times.Exactly(2));
                }
                else
                {
                    this.mockConsoleColourManager.Verify(m => m.SetColours(colours), Times.Once);
                    this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.Default), Times.Once);
                }
            }
        }

        /// <summary>
        /// Tests that the Write(char) method uses the correct colours for the
        /// supplied <see cref="ConsoleOutputType"/>.
        /// </summary>
        /// <param name="outputType">The output type.</param>
        /// <param name="colours">The expected colours.</param>
        [Theory]
        [MemberData(nameof(OutputTypesAndColours))]
        public void Write_Char_UsesColoursWithSameNameAsConsoleOutputTypeAndResetsColours(
            ConsoleOutputType outputType,
            ConsoleColours colours)
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new ColourfulConsole(this.mockConsoleColourManager.Object);

                // Act
                console.Write('H', outputType);

                // Assert
                if (outputType == ConsoleOutputType.Default)
                {
                    this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.Default), Times.Exactly(2));
                }
                else
                {
                    this.mockConsoleColourManager.Verify(m => m.SetColours(colours), Times.Once);
                    this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.Default), Times.Once);
                }
            }
        }

        /// <summary>
        /// Tests that the WriteLine(char) method uses the correct colours for the
        /// supplied <see cref="ConsoleOutputType"/>.
        /// </summary>
        /// <param name="outputType">The output type.</param>
        /// <param name="colours">The expected colours.</param>
        [Theory]
        [MemberData(nameof(OutputTypesAndColours))]
        public void WriteLine_Char_UsesColoursWithSameNameAsConsoleOutputTypeAndResetsColours(
            ConsoleOutputType outputType,
            ConsoleColours colours)
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new ColourfulConsole(this.mockConsoleColourManager.Object);

                // Act
                console.WriteLine('H', outputType);

                // Assert
                if (outputType == ConsoleOutputType.Default)
                {
                    this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.Default), Times.Exactly(2));
                }
                else
                {
                    this.mockConsoleColourManager.Verify(m => m.SetColours(colours), Times.Once);
                    this.mockConsoleColourManager.Verify(m => m.SetColours(ConsoleColours.Default), Times.Once);
                }
            }
        }

        /// <summary>
        /// Tests that the correct exception is thrown if the
        /// <see cref="ConsoleColours"/> class does not have a public
        /// static property with the same name as the supplied
        /// <see cref="ConsoleOutputType"/> member.
        /// </summary>
        [Fact]
        public void Write_ConsoleOutputTypeHasNoMatchingConsoleColoursProperty_Throws()
        {
            lock (LockObjects.ConsoleLock)
            {
                // Arrange
                var console = new ColourfulConsole(this.mockConsoleColourManager.Object);
                var invalidOutputType = (ConsoleOutputType)int.MaxValue;

                // Act
                var action = () => console.Write("Hello, world!", invalidOutputType);

                // Assert
                var ex = action.Should().Throw<InvalidOperationException>().Which;
                ex.Message.Should().Contain($"The '{nameof(ConsoleColours)}' class does not have a public static property called '2147483647'.");
            }
        }
    }
}
