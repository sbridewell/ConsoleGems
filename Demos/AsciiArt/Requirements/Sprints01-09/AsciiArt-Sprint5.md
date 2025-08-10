# Sprint 5

## User stories - sprint 5

### As a product owner, I want the program to preserve the aspect ratio of the original image when displaying it in the console, so that the image is displayed correctly.
- Given an image file in a format supported by the .NET `Image` class
- When the image is represented in the console
- Then the aspect ratio of the original image must be preserved, so that the image is displayed correctly in the console

### As a developer, I want to ensure that the code meets the quality standards for usage of whitespace, so that the code is more readable.
- Given a line of code contains a comma
- When the line of code is analysed by the code analysis tools
- Then the comma must be followed by either a single space character or a line break
- Then the code analysis tools will not report any SA1001 warnings

- Given a line of code contains an opening brace (`{`)
- When the line of code is analysed by the code analysis tools
- Then the opening brace must be followed by a single space character or a line break
- Then the code analysis tools will not report any SA1012 warnings

- Given a line of code contains a closing brace (`}`)
- When the line of code is analysed by the code analysis tools
- Then the closing brace must be preceded by a single space character or a line break
- Then the code analysis tools will not report any SA1013 warnings

- Given a line of code contains an arethmetic expression
- When the line of code is analysed by the code analysis tools
- Then the arithmetic expression must declare precedence using parentheses, so that the order of operations is clear (BODMAS - Brackets, Orders, Division and Multiplication, Addition and Subtraction)
- Then the code analysis tools will not report any SA1407 warnings

## Sprint retrospective - sprint 5
- What went well:
  - The code analysis tools no longer report any SA1001, SA1012, SA1013, or SA1407 warnings in the AsciiArt project.
- What could be improved:
  - There are still 81 warnings returned by the code analysis tools, which is not acceptable according to the non-functional requirements.
  - The program does not preserve the aspect ratio of the original image when displaying it in the console. This is probably because the height and width of a pixel are the same, however the height of a character in the console is greater than the width of a character, so the aspect ratio is not preserved, and the console representation of the image is taller with respect to its width than the original image.
  - Copilot cannot analyse code coverage with the toolset available in Visual Studio Community Edition.
- What should be done next:
  - Update the Definition of Done with instructions for running the Invoke-UnitTestsWithCodeCoverage PowerShell module to analyse code coverage.
  - Write a user story to address the issue of preserving the aspect ratio of the original image when displaying it in the console, taking into account the fact that characters in the console are taller than they are wide.
  - Write user stories to address the following code analysis warnings:
    - SA1101: Prefix local calls with this
    - SA1201: A constructor should not follow a property
    - SA1407: Arithmetic expressions should declare precedence
    - SA1413: Use trailing comma in multi-line initializers
    - SA1513: Closing brace should be followed by blank line
    - SA1515: Single-line comment should be preceded by blank line
