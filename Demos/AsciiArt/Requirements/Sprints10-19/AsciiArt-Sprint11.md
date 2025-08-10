# Sprint 11

## User stories - sprint 11

### As a developer, I want each class in the AsciiArt library to have a corresponding unit test class in the AsciiArt.Test project, so that the tests are organised and easy to find, and debugging will be easier.
- Given the AsciiArt library contains multiple classes (e.g., AsciiArtGenerator, ConsoleColorMapper),
- When the unit tests are organised,
- Then each class in the AsciiArt library will have a corresponding unit test class in the AsciiArt.Test project, named similarly to the class it tests (e.g., AsciiArtGeneratorTests, ConsoleColorMapperTests).

### As a developer, I want to debug the ConsoleColorMapper class, so that I can identify why the colour mapping does not work correctly when the program is run as a console application.
- Given the ConsoleColorMapper class is responsible for mapping RGB values to ConsoleColor values,
- When I run the console application in debug mode,
- Then I can set breakpoints in the ConsoleColorMapper class to inspect the RGB values being processed and the resulting ConsoleColor values.

# Sprint retrospective - sprint 11
- What went well:
  - The unit tests for the AsciiArt library are now organised, with each class having a corresponding unit test class in the AsciiArt.Test project, making it easier to find and maintain tests.
  - We identified that the apparent problem with mapping image colours to console colours was actually due to the reference images used for testing being GIF files rather than bitmap files. After creating suitable bitmap files, the mappings look much better.
- What could be improved:
  - Is it correct to map e.g. #F00 to Red? Would DarkRed be more appropriate?
  - The ConsoleColorMapper class has Cyclomatic complexity and NPath complexity of 102, which is quite high. This could make the code harder to maintain and understand.
- What should be done next:
  - Refactor the ConsoleColorMapper class to reduce its complexity, possibly by breaking it down into smaller methods or classes.
  - Ensure that the refactored code is well-documented and that the unit tests cover all the edge cases for colour mapping.
  - Consider using color science libraries or perceptual metrics for more accurate color-to-console mapping.
    - Color Science Libraries: These are libraries (such as Colourful, ColorMine, or System.Drawing.ColorConverter) that provide advanced color models and conversions, including CIELAB, CIELUV, and others. These models are designed to be more perceptually uniform than RGB.
    - Perceptual Metrics: Metrics like CIE76, CIE94, CIEDE2000, and Delta E are mathematical formulas that quantify the perceived difference between two colors as seen by the human eye. Using these, you can map an arbitrary RGB color to the closest ConsoleColor in a way that better matches what a person would expect.
    - Why it matters: RGB distance (e.g., Euclidean distance in RGB space) can be misleading because the human eye is more sensitive to some colors than others. Perceptual metrics account for this, so mapping is visually more accurate.
    - How to use:
      1. Convert the input RGB color and each ConsoleColor’s RGB value to a perceptual color space (e.g., LAB).
      2. Use a Delta E formula to compute the perceptual distance between the input color and each ConsoleColor.
      3. Choose the ConsoleColor with the smallest perceptual distance.
  - Document the rationale for mapping rules and thresholds in code and tests.