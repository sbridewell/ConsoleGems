# Sprint 22

## User stories - sprint 22

### As a user, I want to be able to select the colour mapping strategy and character blending strategy at runtime, so that I can customize the ASCII art generation process.
- Given the AsciiArt library has multiple colour mapping strategies and character blending strategies,
- When I have selected an image to be represented in the console
- Then I will be prompted to select a colour mapping strategy
- Then I will be prompted to select a character blending strategy
- Then the selected colour mapping strategy will be used to map image colours to console foreground colours
- Then the selected colour mapping strategy will be used to map image colours to console background colours
- Then the selected character blending strategy will be used to blend characters in the ASCII art representation
- Then the resulting combination of foreground colour, background colour and character will be the best representation available of the image colour

- Given the AsciiArt library has the ability to use different colour mapping strategies and character blending strategies,
- When the `AsciiArtGenerator.RenderImageAsAsciiArt` method is called
- Then it will be passed an instance of `IConsoleColourMapper` and an instance of `IAsciiArtCellMapper`
- Then it will use the supplied instances of `IConsoleColourMapper` and an instance of `IAsciiArtCellMapper` to map image colours to console colours

- Given the AsciiArt library has multiple colour mapping strategies and character blending strategies,
- When the user is prompted to select an image to represent in the console (in `CreateAsciiArtCommand`)
- Then `CreateAsciiArtCommand` will use reflection to discover the available implementations of `IConsoleColourMapper` and `ICharacterBlender`
- Then the user will also be prompted to select a colour mapping strategy (using `ColourMapperPrompter`)
- Then the user will also be prompted to select a character blending strategy (using a new `CharacterBlenderPrompter` which implements a new `ICharacterBlenderPrompter` interface derived from `IPrompter`)
- Then the selected strategies will be used for the currently selected image only, and will not affect the default strategies used for subsequent images

## Sprint retrospective - sprint 22
- What went well:
  - The ability to select colour mapping and character blending strategies at runtime was implemented, allowing for greater customization of the ASCII art generation process.
- What could be improved:
  - Better prompting to ensure that generated code meets expected standards, such as being covered by unit tests, passing compilation checks with having no warnings, and all public classes and members having XML documentation comments.
- What should be done next:
  - Add an implementation of `ICharacterBlender` which can select from more than 5 different characters.

