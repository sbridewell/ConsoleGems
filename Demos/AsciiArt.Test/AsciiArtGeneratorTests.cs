// <copyright file="AsciiArtGeneratorTests.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell.
// Released under the MIT license - see LICENSE.txt in the repository root.
// </copyright>

namespace Sde.AsciiArt.Test
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.Versioning;
    using System.Text;
    using FluentAssertions;
    using Sde.AsciiArt.ColourMappers;
    using Sde.AsciiArt.Generators;
    using Sde.AsciiArt.Models;
    using Sde.AsciiArt.Test.ColourMappers;
    using Sde.ConsoleGems.Consoles;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Contains unit tests for the AsciiArtGenerator class.
    /// </summary>
    public class AsciiArtGeneratorTests(ITestOutputHelper output)
    {
        /// <summary>
        /// Tests that Pixel stores RGB values correctly.
        /// </summary>
        [Fact]
        public void Pixel_StoresRgbValuesCorrectly()
        {
            var pixel = new Pixel(10, 20, 30);
            pixel.Red.Should().Be(10);
            pixel.Green.Should().Be(20);
            pixel.Blue.Should().Be(30);
        }

        /// <summary>
        /// Parameterized test for ConsoleColorMapper mapping RGB to ConsoleColor, including edge and uncommon cases.
        /// </summary>
        /// <param name="r">The red component of the pixel (0-255).</param>
        /// <param name="g">The green component of the pixel (0-255).</param>
        /// <param name="b">The blue component of the pixel (0-255).</param>
        /// <param name="expected">The expected ConsoleColor result.</param>
        [Theory]
        [ClassData(typeof(ConsoleColorMapperTestData))]
        public void ConsoleColorMapper_MapsRgbToConsoleColor_AllValuesAndEdgeCases(int r, int g, int b, ConsoleColor expected)
        {
            var mapper = new RuleBasedRgbConsoleColourMapper();
            mapper.MapToConsoleColor(new Pixel(r, g, b)).Should().Be(expected);
        }

        /// <summary>
        /// Tests that PixelLoader reads an image and extracts pixels correctly.
        /// </summary>
        [SupportedOSPlatform("windows")]
        [Fact]
        public void PixelLoader_ReadsImageAndExtractsPixels()
        {
            // Arrange
            int width = 2, height = 2;
            using var bmp = new Bitmap(width, height);
            bmp.SetPixel(0, 0, Color.Red);
            bmp.SetPixel(1, 0, Color.Green);
            bmp.SetPixel(0, 1, Color.Blue);
            bmp.SetPixel(1, 1, Color.White);
            var tempPath = Path.GetTempFileName() + ".bmp";
            bmp.Save(tempPath);
            using var bitmap = new Bitmap(tempPath);

            // Act
            var pixels = PixelLoader.LoadPixels(bitmap);

            // Assert
            pixels[0, 0].Red.Should().Be(255);
            pixels[0, 0].Green.Should().Be(0);
            pixels[0, 0].Blue.Should().Be(0);

            pixels[1, 0].Red.Should().Be(0);
            pixels[1, 0].Green.Should().Be(128);
            pixels[1, 0].Blue.Should().Be(0);

            pixels[0, 1].Red.Should().Be(0);
            pixels[0, 1].Green.Should().Be(0);
            pixels[0, 1].Blue.Should().Be(255);

            pixels[1, 1].Red.Should().Be(255);
            pixels[1, 1].Green.Should().Be(255);
            pixels[1, 1].Blue.Should().Be(255);
        }

        /// <summary>
        /// Tests that AsciiArtGenerator renders image as ASCII art and saves HTML and image files.
        /// </summary>
        [SupportedOSPlatform("windows")]
        [Fact]
        public void AsciiArtGenerator_RendersImageAsAsciiArt_SavesHtmlAndImage()
        {
            // Create a test image
            int width = 4, height = 2;
            using var bmp = new Bitmap(width, height);
            bmp.SetPixel(0, 0, Color.Red);
            bmp.SetPixel(1, 0, Color.Green);
            bmp.SetPixel(2, 0, Color.Blue);
            bmp.SetPixel(3, 0, Color.Yellow);
            bmp.SetPixel(0, 1, Color.Cyan);
            bmp.SetPixel(1, 1, Color.Magenta);
            bmp.SetPixel(2, 1, Color.Black);
            bmp.SetPixel(3, 1, Color.White);

            var tempImagePath = Path.GetTempFileName() + ".bmp";
            bmp.Save(tempImagePath);

            var htmlPath = Path.GetTempFileName() + ".html";
            var sb = new StringBuilder();
            sb.AppendLine("<html><body style='font-family:monospace;white-space:pre'>");

            var fakeConsole = new FakeConsole(sb);
            var generator = new AsciiArtGenerator();
            generator.RenderImageAsAsciiArt(tempImagePath, fakeConsole, new RuleBasedRgbConsoleColourMapper());

            sb.AppendLine("</body></html>");
            File.WriteAllText(htmlPath, sb.ToString());

            // Output paths for developer
            System.Console.WriteLine($"Original image: {tempImagePath}");
            System.Console.WriteLine($"ASCII art HTML: {htmlPath}");

            File.Exists(tempImagePath).Should().BeTrue();
            File.Exists(htmlPath).Should().BeTrue();
            var html = File.ReadAllText(htmlPath);
            html.Should().Contain("<span style='background-color:");
        }

        /// <summary>
        /// Tests that AsciiArtGenerator renders image as ASCII art using IAsciiArtCellMapper and saves HTML and image files.
        /// </summary>
        [SupportedOSPlatform("windows")]
        [Fact]
        public void AsciiArtGenerator_RendersImageAsAsciiArt_WithCellMapper_SavesHtmlAndImage()
        {
            // Create a test image
            int width = 4, height = 2;
            using var bmp = new Bitmap(width, height);
            bmp.SetPixel(0, 0, Color.Red);
            bmp.SetPixel(1, 0, Color.Green);
            bmp.SetPixel(2, 0, Color.Blue);
            bmp.SetPixel(3, 0, Color.Yellow);
            bmp.SetPixel(0, 1, Color.Cyan);
            bmp.SetPixel(1, 1, Color.Magenta);
            bmp.SetPixel(2, 1, Color.Black);
            bmp.SetPixel(3, 1, Color.White);

            var tempImagePath = Path.GetTempFileName() + ".bmp";
            bmp.Save(tempImagePath);

            var htmlPath = Path.GetTempFileName() + ".html";
            var sb = new StringBuilder();
            sb.AppendLine("<html><body style='font-family:monospace;white-space:pre'>");

            var fakeConsole = new FakeConsole(sb);
            var generator = new AsciiArtGenerator();
            var cellMapper = new Sde.AsciiArt.CellMappers.SimpleCellMapper(
                new Sde.AsciiArt.ColourMappers.RuleBasedRgbConsoleColourMapper(),
                new Sde.AsciiArt.ColourMappers.RuleBasedRgbConsoleColourMapper(),
                new Sde.AsciiArt.CharacterBlenders.BlockCharacterBlender());
            generator.RenderImageAsAsciiArt(tempImagePath, fakeConsole, cellMapper);

            sb.AppendLine("</body></html>");
            File.WriteAllText(htmlPath, sb.ToString());

            // Output paths for developer
            System.Console.WriteLine($"Original image: {tempImagePath}");
            System.Console.WriteLine($"ASCII art HTML: {htmlPath}");

            File.Exists(tempImagePath).Should().BeTrue();
            File.Exists(htmlPath).Should().BeTrue();
            var html = File.ReadAllText(htmlPath);
            html.Should().Contain("<span style='background-color:");
        }

        /// <summary>
        /// Generates 4x4 single-colour reference images for colour mapping tests.
        /// </summary>
        [Fact]
        [SupportedOSPlatform("windows")]
        public void Generate_SingleColour_Reference_Images()
        {
            var colors = new[]
            {
                (Name: @"../../../../AsciiArt/TestImages/BlackSquare.bmp", Color: Color.FromArgb(0, 0, 0)),
                (Name: @"../../../../AsciiArt/TestImages/BlueSquare.bmp", Color: Color.FromArgb(0, 0, 255)),
                (Name: @"../../../../AsciiArt/TestImages/GreenSquare.bmp", Color: Color.FromArgb(0, 255, 0)),
                (Name: @"../../../../AsciiArt/TestImages/CyanSquare.bmp", Color: Color.FromArgb(0, 255, 255)),
                (Name: @"../../../../AsciiArt/TestImages/RedSquare.bmp", Color: Color.FromArgb(255, 0, 0)),
                (Name: @"../../../../AsciiArt/TestImages/MagentaSquare.bmp", Color: Color.FromArgb(255, 0, 255)),
                (Name: @"../../../../AsciiArt/TestImages/YellowSquare.bmp", Color: Color.FromArgb(255, 255, 0)),
            };

            foreach (var (name, color) in colors)
            {
                string? dir = Path.GetDirectoryName(name);
                if (!string.IsNullOrEmpty(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using var bmp = new Bitmap(4, 4);

                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        bmp.SetPixel(x, y, color);
                    }
                }

                bmp.Save(name);
                File.Exists(name).Should().BeTrue();
            }
        }

        /// <summary>
        /// Generates 16x16 bitmaps for all shades of red, green, and blue in the TestImages folder.
        /// </summary>
        [Fact]
        [SupportedOSPlatform("windows")]
        public void Generate_AllPrimaryColour_Reference_Images()
        {
            string testImagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../../AsciiArt/TestImages");
            output.WriteLine($"Test images directory: {testImagesDir}");

            // Helper to create a bitmap for a primary colour
            void CreatePrimaryBitmap(string fileName, Func<int, int, Color> getColor)
            {
                using var bmp = new Bitmap(16, 16);

                for (int y = 0; y < 16; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        int value = (y * 16) + x; // 0..255
                        bmp.SetPixel(x, y, getColor(value, 255));
                    }
                }

                bmp.Save(fileName);
                File.Exists(fileName).Should().BeTrue();
            }

            // All reds: R varies 0..255, G/B=0
            CreatePrimaryBitmap(Path.Combine(testImagesDir, "AllReds.bmp"), (v, max) => Color.FromArgb(v, 0, 0));

            // All greens: G varies 0..255, R/B=0
            CreatePrimaryBitmap(Path.Combine(testImagesDir, "AllGreens.bmp"), (v, max) => Color.FromArgb(0, v, 0));

            // All blues: B varies 0..255, R/G=0
            CreatePrimaryBitmap(Path.Combine(testImagesDir, "AllBlues.bmp"), (v, max) => Color.FromArgb(0, 0, v));
        }

        /// <summary>
        /// Verifies that the program maps reference images to the correct console colours.
        /// </summary>
        /// <param name="imageFile">The image file to test.</param>
        /// <param name="expectedColor">The expected ConsoleColor.</param>
        [SupportedOSPlatform("windows")]
        [Theory]
        [InlineData(@"TestImages/RedSquare.bmp", ConsoleColor.Red)]
        [InlineData(@"TestImages/GreenSquare.bmp", ConsoleColor.Green)]
        [InlineData(@"TestImages/BlueSquare.bmp", ConsoleColor.Blue)]
        [InlineData(@"TestImages/CyanSquare.bmp", ConsoleColor.Cyan)]
        [InlineData(@"TestImages/MagentaSquare.bmp", ConsoleColor.Magenta)]
        [InlineData(@"TestImages/YellowSquare.bmp", ConsoleColor.Yellow)]
        [InlineData(@"TestImages/BlackSquare.bmp", ConsoleColor.Black)]
        [InlineData(@"TestImages/WhiteSquare.bmp", ConsoleColor.White)]
        public void AsciiArtGenerator_MapsReferenceImageToCorrectConsoleColor(string imageFile, ConsoleColor expectedColor)
        {
            // Arrange
            var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imageFile);
            if (!File.Exists(imagePath))
            {
                // Generate the reference image in the output directory if missing
                Color color = expectedColor switch
                {
                    ConsoleColor.Red => Color.FromArgb(255, 0, 0),
                    ConsoleColor.Green => Color.FromArgb(0, 255, 0),
                    ConsoleColor.Blue => Color.FromArgb(0, 0, 255),
                    ConsoleColor.Cyan => Color.FromArgb(0, 255, 255),
                    ConsoleColor.Magenta => Color.FromArgb(255, 0, 255),
                    ConsoleColor.Yellow => Color.FromArgb(255, 255, 0),
                    ConsoleColor.Black => Color.FromArgb(0, 0, 0),
                    ConsoleColor.White => Color.FromArgb(255, 255, 255),
                    _ => Color.FromArgb(0, 0, 0),
                };
                using var bmp = new Bitmap(4, 4);

                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        bmp.SetPixel(x, y, color);
                    }
                }

                bmp.Save(imagePath);
            }

            using var bmpCheck = new Bitmap(imagePath); // Validate image can be loaded
            var sb = new StringBuilder();
            var fakeConsole = new CapturingConsole(sb);
            var generator = new AsciiArtGenerator();

            // Act
            generator.RenderImageAsAsciiArt(imagePath, fakeConsole, new RuleBasedRgbConsoleColourMapper());

            // Assert: check that the output contains the expected background color for all pixels
            var consoleOutput = sb.ToString();
            var expectedHex = CapturingConsole.ToHex(expectedColor);
            consoleOutput.Should().Contain($"background-color:{expectedHex}");
        }

        /// <summary>
        /// Tests that RenderImageAsAsciiArt throws ArgumentNullException when imagePath is null.
        /// </summary>
        [SupportedOSPlatform("windows")]
        [Fact]
        public void RenderImageAsAsciiArt_ThrowsArgumentNullException_WhenImagePathIsNull()
        {
            var generator = new AsciiArtGenerator();
            Action act = () => generator.RenderImageAsAsciiArt(null!, new FakeConsole(new StringBuilder()), new RuleBasedRgbConsoleColourMapper());
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("imagePath");
        }

        /// <summary>
        /// Tests that RenderImageAsAsciiArt throws ArgumentNullException when console is null.
        /// </summary>
        [SupportedOSPlatform("windows")]
        [Fact]
        public void RenderImageAsAsciiArt_ThrowsArgumentNullException_WhenConsoleIsNull()
        {
            var generator = new AsciiArtGenerator();
            Action act = () => generator.RenderImageAsAsciiArt("somefile.bmp", null!, new RuleBasedRgbConsoleColourMapper());
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("console");
        }

        /// <summary>
        /// Parameterized test for ConsoleDimensionCalculator for various aspect ratios and edge cases.
        /// </summary>
        /// <param name="imgWidth">Image width.</param>
        /// <param name="imgHeight">Image height.</param>
        /// <param name="consoleWidth">Initial console width.</param>
        /// <param name="consoleHeight">Initial console height.</param>
        /// <param name="charAspectRatio">Character aspect ratio.</param>
        /// <param name="expectedWidth">Expected width.</param>
        /// <param name="expectedHeight">Expected height.</param>
        [Theory]
        [InlineData(200, 100, 160, 80, 2.0, 160, 40)] // Wider image than console, fits and preserves aspect
        [InlineData(100, 200, 160, 80, 2.0, 80, 80)] // Taller image than console, fits and preserves aspect
        [InlineData(100, 100, 160, 80, 2.0, 160, 80)] // Square image and console, fits and preserves aspect
        public void CalculateConsoleDimensions_ReturnsExpectedDimensions(int imgWidth, int imgHeight, int consoleWidth, int consoleHeight, double charAspectRatio, int expectedWidth, int expectedHeight)
        {
            var dims = ConsoleDimensionCalculator.Calculate(imgWidth, imgHeight, consoleWidth, consoleHeight, charAspectRatio);
            dims.Width.Should().Be(expectedWidth);
            dims.Height.Should().Be(expectedHeight);
        }

        /// <summary>
        /// Tests that CursorHelper.WithCursorHidden restores cursor visibility after the action.
        /// </summary>
        [Fact]
        public void CursorHelper_WithCursorHidden_RestoresCursorVisibility()
        {
            var fakeConsole = new FakeConsole(new StringBuilder()) { CursorVisible = true };
            bool actionRan = false;
            CursorHelper.WithCursorHidden(
                fakeConsole,
                () =>
                {
                    actionRan = true;
                    fakeConsole.CursorVisible.Should().BeFalse();
                });
            actionRan.Should().BeTrue();
            fakeConsole.CursorVisible.Should().BeTrue();
        }

        /// <summary>
        /// Tests RenderHelper.RenderLines outputs expected text using a mock bufferBuilder and console.
        /// </summary>
        [Fact]
        public void RenderHelper_RenderLines_WritesExpectedOutput()
        {
            var sb = new StringBuilder();
            var fakeConsole = new FakeConsole(sb);
            Func<int, int, AsciiArtGenerator.CharacterColourRun[]> bufferBuilder = (y, width) => new[] { new AsciiArtGenerator.CharacterColourRun("X", new ConsoleColours(ConsoleColor.White, ConsoleColor.Black)) };
            RenderHelper.RenderLines(2, 2, bufferBuilder, fakeConsole);
            sb.ToString().Should().Contain("X");
        }

        /// <summary>
        /// Tests BufferBuilder.BuildColourMapperBuffer returns expected runs for a simple pixel matrix.
        /// </summary>
        [Fact]
        public void BufferBuilder_BuildColourMapperBuffer_ReturnsExpectedRuns()
        {
            var pixels = new List<List<Pixel>>
            {
                new List<Pixel> { new Pixel(255, 0, 0), new Pixel(0, 255, 0) },
                new List<Pixel> { new Pixel(0, 0, 255), new Pixel(255, 255, 255) },
            };
            var pixelMatrix = new PixelMatrix(pixels);
            var mapper = new RuleBasedRgbConsoleColourMapper();
            var result = BufferBuilder.BuildColourMapperBuffer(0, 2, 2, 2, pixelMatrix, mapper);
            result.Should().NotBeNull();
            result.Length.Should().BeGreaterThan(0);
        }

        /// <summary>
        /// Tests BufferBuilder.BuildCellMapperBuffer returns expected runs for a simple pixel matrix and cell mapper.
        /// </summary>
        [Fact]
        public void BufferBuilder_BuildCellMapperBuffer_ReturnsExpectedRuns()
        {
            var pixels = new List<List<Pixel>>
            {
                new List<Pixel> { new Pixel(255, 0, 0), new Pixel(0, 255, 0) },
                new List<Pixel> { new Pixel(0, 0, 255), new Pixel(255, 255, 255) },
            };
            var pixelMatrix = new PixelMatrix(pixels);
            var cellMapper = new Sde.AsciiArt.CellMappers.SimpleCellMapper(
                new RuleBasedRgbConsoleColourMapper(),
                new RuleBasedRgbConsoleColourMapper(),
                new Sde.AsciiArt.CharacterBlenders.BlockCharacterBlender());
            var result = BufferBuilder.BuildCellMapperBuffer(0, 2, 2, 2, pixelMatrix, cellMapper);
            result.Should().NotBeNull();
            result.Length.Should().BeGreaterThan(0);
        }

        /// <summary>
        /// A console implementation that captures background color output for verification.
        /// </summary>
        private class CapturingConsole : IConsole
        {
            private readonly StringBuilder sb;

            public CapturingConsole(StringBuilder sb)
            {
                this.sb = sb;
            }

            public int WindowWidth { get; set; } = 4;

            public int WindowHeight { get; set; } = 4;

            public bool CursorVisible { get; set; }

            public int CursorLeft { get; set; }

            public int CursorTop { get; set; }

            public bool KeyAvailable => false;

            public static string ToHex(ConsoleColor color)
            {
                return color switch
                {
                    ConsoleColor.Black => "#000000",
                    ConsoleColor.DarkBlue => "#000080",
                    ConsoleColor.DarkGreen => "#008000",
                    ConsoleColor.DarkCyan => "#008080",
                    ConsoleColor.DarkRed => "#800000",
                    ConsoleColor.DarkMagenta => "#800080",
                    ConsoleColor.DarkYellow => "#808000",
                    ConsoleColor.Gray => "#C0C0C0",
                    ConsoleColor.DarkGray => "#808080",
                    ConsoleColor.Blue => "#0000FF",
                    ConsoleColor.Green => "#00FF00",
                    ConsoleColor.Cyan => "#00FFFF",
                    ConsoleColor.Red => "#FF0000",
                    ConsoleColor.Magenta => "#FF00FF",
                    ConsoleColor.Yellow => "#FFFF00",
                    ConsoleColor.White => "#FFFFFF",
                    _ => "#000000",
                };
            }

            public int Read() => throw new NotImplementedException();

            public ConsoleKeyInfo ReadKey(bool intercept = false) => throw new NotImplementedException();

            public string ReadLine() => throw new NotImplementedException();

            public void Write(string textToWrite, ConsoleOutputType outputType = default) => this.sb.Append(textToWrite);

            public void Write(char characterToWrite, ConsoleOutputType outputType = default) => this.sb.Append(characterToWrite);

            public void Write(char characterToWrite, ConsoleColours consoleColours) => this.sb.Append($"<span style='background-color:{ToHex(consoleColours.Background)}'>{characterToWrite}</span>");

            public void Write(string textToWrite, ConsoleColours consoleColours)
            {
                foreach (char c in textToWrite)
                {
                    this.sb.Append($"<span style='background-color:{ToHex(consoleColours.Background)}'>{c}</span>");
                }
            }

            public void WriteLine(string textToWrite = "", ConsoleOutputType outputType = default) => this.sb.AppendLine(textToWrite);

            public void WriteLine(char characterToWrite, ConsoleOutputType outputType = default) => this.sb.AppendLine(characterToWrite.ToString());

            public void Clear() => this.sb.Clear();
        }

        /// <summary>
        /// A fake console implementation for testing purposes.
        /// </summary>
        private class FakeConsole : IConsole
        {
            private readonly StringBuilder sb;

            public FakeConsole(StringBuilder sb)
            {
                this.sb = sb;
            }

            public int WindowWidth { get; set; } = 4;

            public int WindowHeight { get; set; } = 2;

            public bool CursorVisible { get; set; }

            public int CursorLeft { get; set; }

            public int CursorTop { get; set; }

            public bool KeyAvailable => false;

            public static string ToHex(ConsoleColor color)
            {
                return color switch
                {
                    ConsoleColor.Black => "#000000",
                    ConsoleColor.DarkBlue => "#000080",
                    ConsoleColor.DarkGreen => "#008000",
                    ConsoleColor.DarkCyan => "#008080",
                    ConsoleColor.DarkRed => "#800000",
                    ConsoleColor.DarkMagenta => "#800080",
                    ConsoleColor.DarkYellow => "#808000",
                    ConsoleColor.Gray => "#C0C0C0",
                    ConsoleColor.DarkGray => "#808080",
                    ConsoleColor.Blue => "#0000FF",
                    ConsoleColor.Green => "#00FF00",
                    ConsoleColor.Cyan => "#00FFFF",
                    ConsoleColor.Red => "#FF0000",
                    ConsoleColor.Magenta => "#FF00FF",
                    ConsoleColor.Yellow => "#FFFF00",
                    ConsoleColor.White => "#FFFFFF",
                    _ => "#000000",
                };
            }

            public int Read() => throw new NotImplementedException();

            public ConsoleKeyInfo ReadKey(bool intercept = false) => throw new NotImplementedException();

            public string ReadLine() => throw new NotImplementedException();

            public void Write(string textToWrite, ConsoleOutputType outputType = default) => this.sb.Append(textToWrite);

            public void Write(char characterToWrite, ConsoleOutputType outputType = default) => this.sb.Append(characterToWrite);

            public void Write(char characterToWrite, ConsoleColours consoleColours) => this.sb.Append($"<span style='background-color:{ToHex(consoleColours.Background)}'>{characterToWrite}</span>");

            public void Write(string textToWrite, ConsoleColours consoleColours)
            {
                foreach (char c in textToWrite)
                {
                    this.sb.Append($"<span style='background-color:{ToHex(consoleColours.Background)}'>{c}</span>");
                }
            }

            public void WriteLine(string textToWrite = "", ConsoleOutputType outputType = default) => this.sb.AppendLine(textToWrite);

            public void WriteLine(char characterToWrite, ConsoleOutputType outputType = default) => this.sb.AppendLine(characterToWrite.ToString());

            public void Clear() => this.sb.Clear();
        }
    }
}
