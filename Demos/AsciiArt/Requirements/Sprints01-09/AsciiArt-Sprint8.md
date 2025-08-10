# Sprint 8

## User stories - sprint 8

### As a developer, I want to capture compilation warnings and errors in a text file, and I want to capture unit test results in a .trx file, so that I can make it easier for the Copilot agent to fix any issues in the code
- Given I have a PowerShell script which captures compilation warnings and errors in a text file and which captures unit test results in a .trx file
- When I run the PowerShell script
- Then the compilation warnings and errors will be captured in the file /bin/build_warnings.txt
- Then the unit test results will be captured in the file TestResults/AsciiArt.Tests.trx

### As a user, I want some simple single-colour reference images, so that I can use them to test that the program is mapping colours correctly
- Given I have colour mapping logic which maps RGB values to console colours
- When I run the unit tests
- Then a bitmap of 4 pixels by 4 pixels, all coloured black (#000) will be created with the file name BlackSquare.bmp
- Then a bitmap of 4 pixels by 4 pixels, all coloured blue (#00F) will be created with the file name BlueSquare.bmp
- Then a bitmap of 4 pixels by 4 pixels, all coloured green (#0F0) will be created with the file name GreenSquare.bmp
- Then a bitmap of 4 pixels by 4 pixels, all coloured cyan (#0FF) will be created with the file name CyanSquare.bmp
- Then a bitmap of 4 pixels by 4 pixels, all coloured red (#F00) will be created with the file name RedSquare.bmp
- Then a bitmap of 4 pixels by 4 pixels, all coloured magenta (#F0F) will be created with the file name MagentaSquare.bmp
- Then a bitmap of 4 pixels by 4 pixels, all coloured yellow (#FF0) will be created with the file name YellowSquare.bmp
- Then all the above images will be added to the AsciiArt project will have their "copy to output folder" property set to "copy always"

Clarification: The images should be created by a unit test, and should be added to the project's root folder. The images will be included in source control, however should still be recreated each time the unit test runs.

### As a user, I want the program to map RGB colours to the correct console colours, so that the image is displayed correctly in the console
- Given I have an image with red (#f00) pixels
- When the image is displayed in the console
- Then the pixels will be displayed using ConsoleColor.Red as the background colour

- Given I have an image with green (#0f0) pixels
- When the image is displayed in the console
- Then the pixels will be displayed using ConsoleColor.Green as the background colour

- Given I have an image with blue (#00f) pixels
- When the image is displayed in the console
- Then the pixels will be displayed using ConsoleColor.Blue as the background colour

- Given I have an image with cyan (#0ff) pixels
- When the image is displayed in the console
- Then the pixels will be displayed using ConsoleColor.Cyan as the background colour

- Given I have an image with magenta (#f0f) pixels
- When the image is displayed in the console
- Then the pixels will be displayed using ConsoleColor.Magenta as the background colour

- Given I have an image with yellow (#ff0) pixels
- When the image is displayed in the console
- Then the pixels will be displayed using ConsoleColor.Yellow as the background colour

- Given I have an image with black (#000) pixels
- When the image is displayed in the console
- Then the pixels will be displayed using ConsoleColor.Black as the background colour

- Given I have an image with white (#fff) pixels
- When the image is displayed in the console
- Then the pixels will be displayed using ConsoleColor.White as the background colour

- Given the above acceptance criteria are met
- When I run the unit tests
- Then the above acceptance criteria will be verified by the unit tests

## Sprint retrospective - sprint 8

- What went well:
  - Image colours are being mapped correctly to console colours, for the colours which are in scope for this sprint, and the unit tests cover this
  -	Automated tests and StyleCop integration helped maintain code quality and catch issues early.
  - The use of unit tests to generate and verify reference images ensured reliable color mapping.
- What could be improved:
  - Code generation is a slow process which requires intervention from the lead developer to capture compilation warnings and unit test results which the Copilot agent is unable to access
  -	Automating the process of capturing and reporting build/test results could further streamline the workflow.
  - Consider expanding test coverage to edge cases and less common ConsoleColor values.
- What needs to be done next:
  - Expand the mapping of image colours to console colours, so that it includes all remaining members of the ConsoleColor enum.
  -	Refactor and document the color mapping logic for maintainability.
  - Explore cross-platform compatibility for image and console color handling.