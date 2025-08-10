### As a developer, I want classes to use primary constructor syntax where possible, so that the code is more concise and easier to read.
- Given a class has properties that can be initialized via a constructor,
- When the class is refactored,
- Then the class will use primary constructor syntax to initialize its properties.
  - How it works:
	- The class will be refactored to use primary constructor syntax, which allows properties to be initialized directly in the constructor signature.
	- This will reduce boilerplate code and improve readability.
  - List of classes to be refactored in this way:
    - [x] Pixel
    - [x] PixelMatrix
    - ~~[ ] LabColour~~
    - ~~[ ] LuvColour~~
    - ~~[ ] NormalisedRgb~~
    - ~~[ ] XyzColour~~
    - [x] CellCharacterRun
    - [x] CharacterRun
    - ~~[ ] AsciiArtCell~~

