# Sprint 20

## User stories - sprint 20

### As a developer, I want to research techniques for increasing the effective colour palette in console applications, so that ASCII art can display more nuanced colours.
- Given the limitations of the standard console colour palette,
- When I investigate combining foreground and background colours with different characters,
- Then I will understand the potential to simulate additional colours and shades.

### As a developer, I want to identify and document which characters provide different ratios of foreground to background colour, so that I can use them to blend colours effectively.
- Given a set of available console characters,
- When I analyze their visual density and coverage,
- Then I can create a mapping of characters to their foreground/background ratio.

### As a developer, I want to evaluate the technical feasibility and limitations of using foreground/background colour combinations in .NET console applications, so that I can determine what is possible across different platforms.
- Given the .NET console APIs and platform differences,
- When I test and document support for combined colour rendering,
- Then I will know what approaches are portable and reliable.

### As a developer, I want to review existing research, libraries, or open source projects that use similar techniques, so that I can learn from prior work and avoid reinventing the wheel.
- Given the popularity of ASCII art and console graphics,
- When I search for and review related resources,
- Then I can summarize best practices and potential pitfalls.

### As a developer, I want to produce a summary document outlining the findings and recommended next steps, so that future sprints can proceed with implementation based on solid research.
- Given the results of my investigation,
- When I document the findings,
- Then the team will have a clear plan for increasing the effective colour palette in the console.

## Findings from the user stories

### Characters with different ratios of foreground to background colour

| Ratio   | Character | Unicode Name        | Unicode Code Point | ASCII Code |
|---------|-----------|---------------------|-------------------|------------| 
| 0%      | (space)   | Space               | U+0020            | 32         | 
| 25%     | ░         | Light Shade         | U+2591            | —          | 
| 50%     | ▒         | Medium Shade        | U+2592            | —          | 
| 75%     | ▓         | Dark Shade          | U+2593            | —          | 
| 100%    | █         | Full Block          | U+2588            | —          |

### Prototype implementations

We introduced the `AsciiArtCell` record to represent the return value from mapping an image colour to a console pixel. 

We introduced the `BlendingCellMapper` implementation of `IAsciiArtCellMapper` to map an image colour to a console pixel by blending the foreground and background colours using the character with the specified ratio of foreground to background colour. 

We introduced the `PrototypeAsciiArtGenerator` class to generate ASCII art using the `BlendingCellMapper`. The `PrototypeAsciiArtGenerator` uses the `BlendingCellMapper` to map each pixel of the image to a console pixel, blending the foreground and background colours based on the character's ratio.

We introduced the `PrototypeCreateAsciiArtCommand` class to allow `PrototypeAsciiArtGenerator` to be used as part of the ConsoleGems menu system.

#### AsciiArtCell.cs

```csharp
    /// <summary>
    /// Represents the result of mapping the colour of a pixel in a source
    /// image to a character to be written to the console, with a combination
    /// of foreground colour, background colour and character to maximise the
    /// number of perceived colours displayed in the console from the different
    /// combinations of foreground colour, background colour and the ratio of
    /// the two colours controlled by the character.
    /// </summary>
    /// <param name="Character">
    /// The character which controls the ratio of foreground to background colour.
    /// </param>
    /// <param name="Foreground">The foreground colour.</param>
    /// <param name="Background">The background colour.</param>
    public record AsciiArtCell(char Character, ConsoleColor Foreground, ConsoleColor Background)
    {
    }
```

#### BlendingCellMapper.cs

```csharp
    /// <summary>
    /// Cell mapper that finds the best combination of foreground, background, and character to approximate the pixel colour.
    /// </summary>
    public class BlendingCellMapper : IAsciiArtCellMapper
    {
        // TODO: A way of reusing existing ICharacterBlender and IConsoleColourMapper implementation and selecting them at runtime
        private static readonly (char Character, double Ratio)[] CharRatios = new[]
        {
            (' ', 0.0),
            ('░', 0.25),
            ('▒', 0.5),
            ('▓', 0.75),
            ('█', 1.0),
        };

        private static readonly (ConsoleColor Color, (int R, int G, int B) Rgb)[] ConsoleColors =
            Enum.GetValues<ConsoleColor>()
                .Select(c => (c, GetConsoleColorRgb(c)))
                .ToArray();

        /// <inheritdoc/>
        public AsciiArtCell Map(Pixel pixel)
        {
            var target = (R: pixel.Red, G: pixel.Green, B: pixel.Blue);
            double minDist = double.MaxValue;
            AsciiArtCell best = new(' ', ConsoleColor.Black, ConsoleColor.Black);

            foreach (var (fgColor, fgRgb) in ConsoleColors)
            {
                foreach (var (bgColor, bgRgb) in ConsoleColors)
                {
                    foreach (var (ch, ratio) in CharRatios)
                    {
                        int blendedR = (int)Math.Round((ratio * fgRgb.R) + ((1 - ratio) * bgRgb.R));
                        int blendedG = (int)Math.Round((ratio * fgRgb.G) + ((1 - ratio) * bgRgb.G));
                        int blendedB = (int)Math.Round((ratio * fgRgb.B) + ((1 - ratio) * bgRgb.B));
                        double dist = Math.Pow(target.R - blendedR, 2) + Math.Pow(target.G - blendedG, 2) + Math.Pow(target.B - blendedB, 2);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            best = new AsciiArtCell(ch, fgColor, bgColor);
                        }
                    }
                }
            }

            return best;
        }

        private static (int R, int G, int B) GetConsoleColorRgb(ConsoleColor color)
        {
            // Standard Windows console palette
            return color switch
            {
                ConsoleColor.Black => (0, 0, 0),
                ConsoleColor.DarkBlue => (0, 0, 128),
                ConsoleColor.DarkGreen => (0, 128, 0),
                ConsoleColor.DarkCyan => (0, 128, 128),
                ConsoleColor.DarkRed => (128, 0, 0),
                ConsoleColor.DarkMagenta => (128, 0, 128),
                ConsoleColor.DarkYellow => (128, 128, 0),
                ConsoleColor.Gray => (192, 192, 192),
                ConsoleColor.DarkGray => (128, 128, 128),
                ConsoleColor.Blue => (0, 0, 255),
                ConsoleColor.Green => (0, 255, 0),
                ConsoleColor.Cyan => (0, 255, 255),
                ConsoleColor.Red => (255, 0, 0),
                ConsoleColor.Magenta => (255, 0, 255),
                ConsoleColor.Yellow => (255, 255, 0),
                ConsoleColor.White => (255, 255, 255),
                _ => (0, 0, 0),
            };
        }
    }
```

#### PrototypeAsciiArtGenerator.cs

```csharp
    /// <summary>
    /// Prototype generator that uses both a colour mapper and a character blender to render ASCII art.
    /// </summary>
    public class PrototypeAsciiArtGenerator : IAsciiArtGenerator
    {
        private readonly IAsciiArtCellMapper cellMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeAsciiArtGenerator"/> class.
        /// </summary>
        /// <param name="cellMapper">The cell mapper to use.</param>
        public PrototypeAsciiArtGenerator(IAsciiArtCellMapper cellMapper)
        {
            this.cellMapper = cellMapper;
        }

        /// <inheritdoc/>
        [SupportedOSPlatform("windows")]
        public void RenderImageAsAsciiArt(string imagePath, IConsole console, IConsoleColourMapper consoleColourMapper)
        {
            // TODO: remove consoleColourMapper parameter, as it is not used in this implementation?
            ArgumentNullException.ThrowIfNull(imagePath);
            ArgumentNullException.ThrowIfNull(console);
            ArgumentNullException.ThrowIfNull(this.cellMapper);

            using var bitmap = new Bitmap(imagePath);
            int imgWidth = bitmap.Width;
            int imgHeight = bitmap.Height;

            // Get console window size
            int winWidth = console.WindowWidth;
            int winHeight = console.WindowHeight;

            // Calculate scale to fit image in window, preserving aspect ratio
            double charAspect = 2.0; // height/width
            double scaleX = (double)imgWidth / winWidth;
            double scaleY = (double)imgHeight / (winHeight * charAspect);
            double scale = Math.Max(scaleX, scaleY);
            int outWidth = Math.Min(winWidth, (int)Math.Ceiling(imgWidth / scale));
            int outHeight = Math.Min(winHeight, (int)Math.Ceiling(imgHeight / (scale * charAspect)));

            // TODO: use unsafe code for performance
            for (int y = 0; y < outHeight; y++)
            {
                int srcY = Math.Min((int)(y * scale * charAspect), imgHeight - 1);
                for (int x = 0; x < outWidth; x++)
                {
                    int srcX = Math.Min((int)(x * scale), imgWidth - 1);
                    var color = bitmap.GetPixel(srcX, srcY);
                    var pixel = new Pixel(color.R, color.G, color.B);
                    var cell = this.cellMapper.Map(pixel);
                    console.Write(cell.Character, new ConsoleColours(cell.Foreground, cell.Background));
                }

                console.WriteLine();
            }
        }
    }
```

#### PrototypeCreateAsciiArtCommand.cs

```csharp
    /// <summary>
    /// Prototype command to create ASCII art from an image file, allowing selection of both colour mapper and character blender.
    /// </summary>
    [SupportedOSPlatform("Windows")]
    public class PrototypeCreateAsciiArtCommand : ICommand
    {
        private readonly IConsole console;
        private readonly IFilePrompter prompter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeCreateAsciiArtCommand"/> class.
        /// </summary>
        /// <param name="console">The console to render output to.</param>
        /// <param name="prompter">The file prompter used to select image files.</param>
        public PrototypeCreateAsciiArtCommand(
            IConsole console,
            IFilePrompter prompter)
        {
            this.console = console;
            this.prompter = prompter;
        }

        /// <inheritdoc/>
        [SupportedOSPlatform("windows")]
        public void Execute()
        {
            var imagePath = this.prompter.Prompt(
                new DirectoryInfo(Environment.CurrentDirectory),
                "Select an image file to render as ASCII art: ",
                true);

            var cellMapper = new BlendingCellMapper();
            var asciiArtGenerator = new PrototypeAsciiArtGenerator(cellMapper);
            asciiArtGenerator.RenderImageAsAsciiArt(imagePath.FullName, this.console, null);
        }
    }
```

## Sprint retrospective - sprint 20
- What went well:
  - Successful implementation of a prototype that blends foreground and background colours using characters with different ratios of the two colours.
- What could be improved:
  - There seems to be quite a lot of duplication of code between the different implementations of IConsoleColourMapper, can we refactor some of this into helper classes?
  - The `BlendingCellMapper` class has hard coded colour and character mappings, which makes it less flexible. It would be better to allow these to be configured at runtime.
- What should be done next:
  - Ensure the new prototypes are covered by unit tests.
  - Find a way of passing different colour mapping strategies and cell mappers to the `BlendingCellMapper` at runtime so that we can test different strategies.
  - Refactor the `IConsoleColourMapper` implementations to reduce duplication.