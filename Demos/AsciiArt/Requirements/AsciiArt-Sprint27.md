# Sprint 27

## User stories - sprint 27

### As a developer, I want to refactor private members accessed via Reflection in AsciiArtGenerator and related classes into dedicated helper classes, so that each class in the AsciiArt project has a one-to-one relationship with a corresponding unit test class.
- Given there are test methods in AsciiArtGeneratorTests that use Reflection to access private members of AsciiArtGenerator and related classes,
- When I refactor these private members into dedicated helper classes (e.g., PixelLoader, BufferBuilder, CursorHelper),
- Then each helper class will have a corresponding public API and a dedicated unit test class,
- Then the AsciiArtGeneratorTests will be updated to directly test these helper classes without using Reflection.
  - How it works:
    - Identify all private methods in AsciiArtGenerator and related classes that are accessed via Reflection in unit tests.
    - Move each method into a new dedicated class in the AsciiArt project, making them public or internal as appropriate.
    - Ensure each new class has a corresponding unit test class in the AsciiArt.Test project.
    - Update all affected unit tests to use the new helper classes directly, removing all Reflection usage.
    - Ensure all new code and tests include XML documentation comments.
    - **Methods to refactor:**
      1. [x] `LoadPixels(Bitmap bitmap)`
      2. [x] `WithCursorHidden(IConsole console, Action action)`
      3. [x] `RenderLines(int consoleWidth, int consoleHeight, Func<int, int, CharacterColourRun[]> bufferBuilder, IConsole console)`
      4. [x] `BuildColourMapperBuffer(int y, int width, int consoleHeight, int imgWidth, Pixel[,] pixels, IConsoleColourMapper consoleColourMapper)`
      5. [x] `BuildCellMapperBuffer(int y, int width, int consoleHeight, int imgWidth, Pixel[,] pixels, IAsciiArtCellMapper cellMapper)`
  - Acceptance criteria:
    - All private members previously accessed via Reflection are refactored into dedicated helper classes.
    - Each helper class has a corresponding unit test class.
    - All unit tests are updated to use the new helper classes directly.
    - No unit tests use Reflection to access private members.
    - All new code and tests include XML documentation comments.
