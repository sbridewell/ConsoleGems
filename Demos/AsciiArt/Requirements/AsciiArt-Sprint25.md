# Sprint 25

## User stories - sprint 25

### As a user, I want to be prompted to choose between different character blenders when I select the "best quality" quality mode, so that I can perform like-for-like comparisons of different character blenders
- Given the AsciiArt library implements different character blenders
- When I select the "best quality" colour mapping strategy
- Then the application will use Reflection to discover the available `ICharacterBlender` implementations
- Then I will be prompted to choose between the available character blenders
- Then the image will be represented in the console using the character blender that I select.
  - How it works:
	- The application will use Reflection to discover all classes that implement the `ICharacterBlender` interface.
	- The user will be presented with a menu to select one of the available character blenders.
	- The selected character blender will be used to render the ASCII art image.
  - Acceptance criteria:
	- The application correctly discovers and lists all available `ICharacterBlender` implementations.
	- The user can select a character blender from the list.
	- The selected character blender is used to render the ASCII art image.
	- All new code and tests include XML documentation comments.

### As a developer, I want to remove all usage of tuples, so that the code is more readable and maintainable
- Given the current implementation of the AsciiArt library uses tuples in various places
- When the code is refactored
- Then each tuple will be replaced by a record or model class which more clearly indicates the meaning of the values
- Then all replacement code will be fully documented with XML comments.
  - How it works:
	- The code will be refactored to replace tuples with named record types or model classes.
	- Each tuple will be replaced with a class that has properties with meaningful names.
	- The new classes will be documented with XML comments to explain their purpose and usage.
  - Acceptance criteria:
	- All tuples in the codebase are replaced with named record types or model classes.
	- The new classes are well-documented with XML comments.
	- The code remains functional and passes all existing tests.
	- No new functionality is introduced, only refactoring for clarity and maintainability.

## Sprint retrospecitve - sprint 25
- What went well:
  - The implementation of the character blender selection feature allows users to choose between different blending strategies, enhancing the flexibility and quality of ASCII art rendering.
  - The removal of tuples improved code readability and maintainability, making it easier for developers to understand and work with the codebase.
- What could be improved:
  - Code updates required significant intervention from the lead developer in order to get them to build and meet all quality requirements
- What should be done next:
  - Introduce a new `ICharacterBlender` implementation that uses a neural network to blend characters, so that the quality of ASCII art can be further improved.
  - Introduce a new `ICharacterBlender` implementation that uses a larger number of characters than `MultiCharacterBlender`, so that the quality of ASCII art can be further improved.