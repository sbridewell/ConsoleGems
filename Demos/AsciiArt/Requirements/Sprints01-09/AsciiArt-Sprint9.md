# Sprint 9

## User stories - sprint 9

### As a developer, I want the colour mapping logic to be refactored into a dedicated, well-documented class or service, so that it is easier to maintain, extend, and understand how RGB values are mapped to ConsoleColor values.

- Given the current colour mapping logic is implemented as a private static method in AsciiArtGenerator,
- When the logic is refactored into a new class (e.g., ConsoleColorMapper) with XML documentation and clear method signatures,
- Then the mapping logic is reusable, testable, and its behavior is clearly described in the code documentation.

- Given the new colour mapping class/service,
- When a developer reads the code or generated documentation,
- Then they can easily understand how RGB values are mapped to ConsoleColor values, including the algorithm and rationale.

- Given the refactored code,
- When unit tests are run,
- Then all existing and new tests for colour mapping pass, ensuring no regression in functionality.

- Given the new class/service,
- When a new ConsoleColor or mapping rule needs to be added or changed,
- Then the change can be made in a single, well-documented location without modifying unrelated code.

### As a developer, I want to expand the test coverage of the colour mapping logic to include edge cases and less common ConsoleColor values, so that I can ensure the mapping is robust and correctly handles all possible scenarios.

- Given the current unit tests only cover common ConsoleColor values (e.g., Red, Green, Blue, etc.),
- When new tests are added for less common values (e.g., DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, DarkGray, Gray),
- Then the tests verify that the mapping logic correctly maps appropriate RGB values to these ConsoleColor values.

- Given the colour mapping logic,
- When tests are added for edge cases (e.g., RGB values that are equidistant between two ConsoleColor values, or values outside the 0-255 range),
- Then the tests confirm the logic handles these cases as expected and does not throw unexpected exceptions.

- Given the expanded test suite,
- When all tests are run,
- Then all tests pass, demonstrating that the colour mapping logic is correct for both common and uncommon ConsoleColor values and edge cases.

- Given the new tests and their documentation,
- When a developer reviews the test code,
- Then it is clear which RGB values are expected to map to which ConsoleColor values, and why.

## Sprint retrospective - sprint 9

- What went well:
  - The colour mapping logic was successfully refactored into a dedicated class, improving maintainability and readability.
  - The new class is well-documented, making it easier for developers to understand the mapping logic.
  - Expanded test coverage included edge cases and less common ConsoleColor values, ensuring robustness of the mapping logic.
  - We learned that the lead developer can make build warnings / errors available to the Copilot agent by clicking the "+" button above the prompt window and selecting "Output (Build)".
- What could be improved:
  - The colour mapping is not mapping colours to console colours in a way which looks accurate to a human viewer, in particular some shades of primary colours are being mapped to Gray or DarkGray.
  - The MapToConsoleColor method of ConsoleColorMapper is very long and could be refactored to improve readability.
- What should be done next:
  - Refactor the MapToConsoleColor method to improve readability, possibly by breaking it down into smaller methods or using a more structured approach to mapping.
  - Improve the colour mapping so that shades of primary colors are mapped to a range of ConsoleColor values from black to the brightest shade of the primary colour, with no grey or white at any point.
  - When prompting the Copilot agent to perform any task which involves updating or running code or unit tests, the lead developer should ensure that "Output (Build)" is selected in the prompt window to make build warnings / errors and unit test results available to the Copilot agent. Alternatively, this instruction can be included in the prompt.
  -	Consider adding a perceptual color distance metric (e.g., CIEDE2000 or CIELAB) for more human-accurate color mapping.
  - Add more documentation and code comments explaining the rationale behind mapping rules and thresholds.
  - Provide a configuration or extension point for custom color mapping strategies, making it easier to experiment or adapt for different environments.
  - Add visual regression tests (e.g., generate and compare ASCII art output for reference images) to catch subtle mapping issues.
  - Solicit feedback from users/developers on the visual quality of the mapping and iterate based on real-world usage.
