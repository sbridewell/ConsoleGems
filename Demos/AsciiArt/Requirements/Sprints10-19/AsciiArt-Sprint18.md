# Sprint 18

## User stories - sprint 18

### As a user, I want to select the colour mapping strategy at runtime, so that I can choose the one that best suits my needs.
- Given the AsciiArt library supports multiple colour mapping strategies,
- When I run the AsciiArt application,
- Then the application will use Reflection to scan the AsciiArt assembly for implementations of IConsoleColourMapper,
- Then the application will store the available IConsoleColourMapper implementations in a dictionary, using the short name of the class as the key and a new instance of the implementation as the value,
- Then the application will prompt the user to select a colour mapping strategy from the available options, using a custom implementation of the IPrompter interface from the ConsoleGems class library
- Then the application will use the selected colour mapping strategy to display the image on the console.

### As a developer, I want to implement the CIEDE2000 strategy for mapping image colours to console colours, so that I can achieve the highest colour mapping accuracy.
- Given the AsciiArt library has a colour mapping strategy based on CIE94,
- When the CIEDE2000 strategy is implemented,
- Then the colour mapping will be the most accurate, especially for blues and neutrals, and the CIEDE2000 algorithm will be used to calculate the colour difference between the image pixel’s RGB value and each console colour’s RGB value.
  - How it works:
    - The most advanced CIELAB-based metric, CIEDE2000 further refines the calculation to better match human perception, especially for blues and neutrals.
  - Pros:
    - Most perceptually accurate of the CIE algorithms
    - Industry standard for colour difference
  - Cons:
    - Computationally expensive
    - More complex to implement

### As a developer, I want to implement the CEILUV strategy for mapping image colours to console colours, so that I can improve the colour mapping accuracy compared to the CIE76 strategy.
- Given the AsciiArt library has a colour mapping strategy based on CIE76,
- When the CEILUV strategy is implemented,
- Then the colour mapping will be more accurate, especially for small colour differences, and the CEILUV algorithm will be used to calculate the colour difference between the image pixel’s RGB value and each console colour’s RGB value.
  - How it works:
    - Converts RGB to CIELUV (a perceptually uniform colour space), then uses Euclidean distance to find the closest console colour.
  - Pros:
    - More perceptually accurate than RGB
    - Still relatively simple
  - Cons:
    - Requires RGB-to-LUV conversion (slower than RGB)
    - CIELUV is not perfect for all colour differences

## Sprint retrospective - sprint 18
- What went well:
  - The CIEDE2000 strategy was successfully implemented, achieving the highest colour mapping accuracy.
  - The CEILUV strategy was successfully implemented, improving the colour mapping accuracy compared to the CIE76 strategy.
  - The AsciiArt library now supports multiple colour mapping strategies, allowing users to select the one that best suits their needs at runtime.
- What could be improved:
  - Identifying compilation warnings, failing unit tests, and code formatting issues is still a manual process, requiring the lead developer to instruct the Copilot agent every time to resolve them.
- What should be done next:
  - Implement the precomputed lookup table and dithering strategies for mapping image colours to console colours, to further improve the colour mapping accuracy and performance.
