// <copyright file="ConsoleGemsAsciiArtRendererTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace AsciiArt.Test
{
    using Xunit;
    using Moq;
    using Sde.ConsoleGems.Consoles;
    using AsciiArt;

    public class ConsoleGemsAsciiArtRendererTests
    {
        [Fact]
        public void Render_WritesCellsToConsole()
        {
            var console = new Mock<IConsole>();
            var cells = new ConsoleCell[2, 1];
            cells[0, 0] = new ConsoleCell(System.ConsoleColor.Red, System.ConsoleColor.Black, '@');
            cells[1, 0] = new ConsoleCell(System.ConsoleColor.Green, System.ConsoleColor.Black, '.');
            var renderer = new ConsoleGemsAsciiArtRenderer();
            renderer.Render(cells, console.Object);
            console.Verify(x => x.Write(It.IsAny<string>(), ConsoleOutputType.Default), Times.Exactly(2));
            console.Verify(x => x.WriteLine(It.IsAny<string>(), ConsoleOutputType.Default), Times.AtLeastOnce);
        }
    }
}
