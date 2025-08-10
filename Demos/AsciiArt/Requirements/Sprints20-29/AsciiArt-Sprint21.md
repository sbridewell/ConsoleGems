# ASCII art sprint 21

## User stories - sprint 21

### As a developer, I want to identify and reduce duplication in the various colour mapping strategies, so that I can improve the maintainability of the code.
- Given the AsciiArt library has multiple colour mapping strategies,
- When multiple IConsoleColourMapper implementations contain the same or very similar code snippets
- Then the duplicated code will be refactored into a shared utility class or method, reducing code duplication and improving maintainability.

## Sprint retrospective - sprint 21
- What went well:
  - The code duplication in the colour mapping strategies was identified and reduced. This improved the clarity and maintainability of the code.
- What could be improved:
  - The prototype code introduced in sprint 20 still needs to be unit tested and integrated into the main codebase.
  - We need a way of selecting the IConsoleColourMapper and ICharacterBlender implementations to pass to the IAsciiArtCellMapper at runtime.
    - New code must be covered by unit tests.
What should be done next:
  - We need to implement a way of selecting the IConsoleColourMapper and ICharacterBlender implementations to pass to the IAsciiArtCellMapper at runtime.
  - The prototype code should be unit tested and integrated into the main codebase.
