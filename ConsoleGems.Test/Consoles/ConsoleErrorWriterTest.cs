// <copyright file="ConsoleErrorWriterTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.ConsoleGems.Test.Consoles
{
    /// <summary>
    /// Unit tests for the <see cref="ConsoleErrorWriter"/> class.
    /// </summary>
    public class ConsoleErrorWriterTest
    {
        /// <summary>
        /// Tests that the WriteError method writes the correct output and calls the correct
        /// Before... and After... methods.
        /// </summary>
        [Fact]
        public void WriteError_WritesCorrectOutputAndCallsCorrectBeforeAndAfterMethods()
        {
            // Arrange
            var console = new Mock<IConsole>();
            var consoleErrorWriter = new ConsoleErrorWriterForTests(console.Object);

            // Act
            consoleErrorWriter.WriteError("An error occurred");

            // Assert
            console.Verify(cw => cw.WriteLine("An error occurred", ConsoleOutputType.Error), Times.Once);
            consoleErrorWriter.BeforeWriteErrorCallCount.Should().Be(1);
            consoleErrorWriter.AfterWriteErrorCallCount.Should().Be(1);
            consoleErrorWriter.BeforeWriteExceptionDetailsCallCount.Should().Be(0);
            consoleErrorWriter.AfterWriteExceptionDetailsCallCount.Should().Be(0);
        }

        /// <summary>
        /// Tests that the WriteException method writes the correct output and calls the correct
        /// Before... and After... methods.
        /// </summary>
        [Fact]
        public void WriteException_WritesCorrectOutputAndCallsCorrectBeforeAndAfterMethods()
        {
            // Arrange
            var console = new Mock<IConsole>();
            var consoleErrorWriter = new ConsoleErrorWriterForTests(console.Object);

            // Act
            try
            {
                throw new Exception("An exception occurred");
            }
            catch (Exception ex)
            {
                consoleErrorWriter.WriteException(ex);
            }

            // Assert
            console.Verify(cw => cw.WriteLine("Unhandled exception:", ConsoleOutputType.Error), Times.Once);
            console.Verify(cw => cw.WriteLine(It.Is<string>(s => s.Contains("System.Exception: An exception occurred")), ConsoleOutputType.Default), Times.Once);
            consoleErrorWriter.BeforeWriteErrorCallCount.Should().Be(1);
            consoleErrorWriter.AfterWriteErrorCallCount.Should().Be(1);
            consoleErrorWriter.BeforeWriteExceptionDetailsCallCount.Should().Be(1);
            consoleErrorWriter.AfterWriteExceptionDetailsCallCount.Should().Be(1);
        }

        /// <summary>
        /// Subclass of <see cref="ConsoleErrorWriter"/> which is implemented to spy on
        /// calls made to the protected Before... and After... methods.
        /// </summary>
        private class ConsoleErrorWriterForTests(IConsole console)
            : ConsoleErrorWriter(console)
        {
            /// <summary>
            /// Gets the number of times that the BeforeWriteError method has been called.
            /// </summary>
            public int BeforeWriteErrorCallCount { get; private set; }

            /// <summary>
            /// Gets the number of times that the AfterWriteError method has been called.
            /// </summary>
            public int AfterWriteErrorCallCount { get; private set; }

            /// <summary>
            /// Gets the number of times that the BeforeWriteExceptionDetails method
            /// has been called.
            /// </summary>
            public int BeforeWriteExceptionDetailsCallCount { get; private set; }

            /// <summary>
            /// Gets the number of times that the AfterWriteExceptionDetails method
            /// has been called.
            /// </summary>
            public int AfterWriteExceptionDetailsCallCount { get; private set; }

            /// <inheritdoc/>
            protected override void BeforeWriteError()
            {
                this.BeforeWriteErrorCallCount++;
                base.BeforeWriteError();
            }

            /// <inheritdoc/>
            protected override void AfterWriteError()
            {
                this.AfterWriteErrorCallCount++;
                base.AfterWriteError();
            }

            /// <inheritdoc/>
            protected override void BeforeWriteExceptionDetails()
            {
                this.BeforeWriteExceptionDetailsCallCount++;
                base.BeforeWriteExceptionDetails();
            }

            /// <inheritdoc/>
            protected override void AfterWriteExceptionDetails()
            {
                this.AfterWriteExceptionDetailsCallCount++;
                base.AfterWriteExceptionDetails();
            }
        }
    }
}
