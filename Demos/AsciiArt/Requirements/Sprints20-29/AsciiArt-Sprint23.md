# Sprint 23

## User stories - sprint 23

### As a developer, I want to implement an `ICharacterBlender` that can select from more than 5 different characters, so that I can improve the visual quality of the ASCII art.
- Given the AsciiArt library has an `ICharacterBlender` implementation that selects from a limited set of characters,
- When the new `MultiCharacterBlender` is implemented,
- Then it will be able to select from a larger set of characters, allowing for more detailed and visually appealing ASCII art representations.
  - How it works:
    - The `MultiCharacterBlender` will use a larger set of characters to blend characters based on the brightness or colour of the image pixel. The recommended character set, ordered from least to most filled, is:
      - `' '` (space)
      - `'.'`
      - `','`
      - `':'`
      - `'-'`
      - `'~'`
      - `'+'`
      - `'*'`
      - `'='`
      - `'%'`
      - `'@'`
      - `'#'`
      - `'░'` (U+2591 Light Shade)
      - `'▒'` (U+2592 Medium Shade)
      - `'▓'` (U+2593 Dark Shade)
      - `'▁'` (U+2581 Lower One Eighth Block)
      - `'▂'` (U+2582 Lower One Quarter Block)
      - `'▃'` (U+2583 Lower Three Eighths Block)
      - `'▄'` (U+2584 Lower Half Block)
      - `'▅'` (U+2585 Lower Five Eighths Block)
      - `'▆'` (U+2586 Lower Three Quarters Block)
      - `'▇'` (U+2587 Lower Seven Eighths Block)
      - `'█'` (U+2588 Full Block)
    - This set provides a smooth gradient for blending and can be customized as needed.
  - Pros:
	- Allows for more detailed and nuanced ASCII art
	- Can create smoother gradients and transitions
  - Cons:
	- May require more complex logic to determine the best character for each pixel

### As a developer, I want to ensure that the `MultiCharacterBlender` is well-tested and integrated into the main codebase, so that it can be used in the ASCII art generation process.
- Given the `MultiCharacterBlender` is implemented,
- When the unit tests are written,
- Then the tests will cover various scenarios, including:
  - Different image sizes and resolutions
  - Different character sets and blending strategies
  - Edge cases such as images with very low or very high contrast

### As a developer, I want all new code and unit tests to have XML documentation comments, so that the code is well-documented and easy to understand.
- Given the `MultiCharacterBlender` and its unit tests are implemented,
- When the code is reviewed,
- Then all public classes, methods, properties, and parameters will have XML documentation comments that explain their purpose and usage.

## Sprint retrospective - sprint 23
