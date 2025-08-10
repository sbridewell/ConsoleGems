# Sprint 26

## User stories - sprint 26

### As a developer, I want to implement a new `ICharacterBlender` that uses a larger set of characters than `MultiCharacterBlender`, so that the quality of ASCII art can be further improved.
- Given the ICharacterBlender class allows for custom character sets,
- When I implement a new character blender that uses a larger set of characters,
- Then the new character blender will be able to select from 255 different characters, which represent a smooth gradient of ratios of foreground colour to background colour,
- Then the image will be represented in the console using the new character blender.
  - How it works:
	- The new character blender will implement the ICharacterBlender interface and use a larger set of characters than the MultiCharacterBlender.
	- The character selection logic will be updated to use the new character set.
  - Acceptance criteria:
	- The new character blender is implemented and can be selected by the user.
	- The new character blender uses 255 different characters.
	- The image is rendered correctly using the new character blender.
	- All new code and tests include XML documentation comments.
