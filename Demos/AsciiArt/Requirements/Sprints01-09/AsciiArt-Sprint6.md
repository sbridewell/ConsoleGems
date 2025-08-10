# Sprint 6

## User stories - sprint 6

### As a user, I want the program to take into account the fact that a character on the console is taller than it is wide, so that it can better preserve the aspect ratio of the original image
- Given I have an image whose width in pixels is the same as its height in pixels
- When the image is represented on the console
- Then the width of the console representation of the image in pixels (rather than characters) will be the same as the height of the console representation of the image in pixels (rather than characters)

- Given I have an image whose width in pixels is twice its height in pixels
- When the image is represented on the console
- Then the width of the console representation of the image in pixels (rather than characters) will be double the height of the console representation of the image in pixels (rather than characters)

This user story supersedes previous user stories which relate to preserving the aspect ratio of the original image, which did not convey my intent clearly enough.

### As a developer, I want all remaining code analysis warnings to be fixed, so that I am working with a clean codebase, and any newly introduced warnings are clearly visible
- Given a line of code references an instance member of a class
- When the line of code is analysed by the code analysis tools
- Then the instance member must be prefixed with `this.` to indicate that it is an instance member, and not a static member
- Then the code analysis tools will not report any SA1101 warnings

- Given a class has a constructor
- When the class is analysed by the code analysis tools
- Then the constructor must be placed after all properties in the class
- Then the code analysis tools will not report any SA1201 warnings

- Given a line of code contains an arithmetic expression
- When the line of code is analysed by the code analysis tools
- Then the arithmetic expression must declare precedence using parentheses, so that the order of operations is clear (BODMAS - Brackets, Orders, Division and Multiplication, Addition and Subtraction)
- Then the code analysis tools will not report any SA1407 warnings

- Given a line of code contains a multi-line initializer
- When the line of code is analysed by the code analysis tools
- Then the multi-line initializer must use a trailing comma after the last item in the initializer
- Then the code analysis tools will not report any SA1413 warnings

- Given a line of code contains a closing brace (`}`)
- When the line of code is analysed by the code analysis tools
- Then the closing brace must be followed by a blank line
- Then the code analysis tools will not report any SA1513 warnings

- Given a line of code contains a single-line comment
- When the line of code is analysed by the code analysis tools
- Then the single-line comment must be preceded by a blank line
- Then the code analysis tools will not report any SA1515 warnings

## Sprint retrospective - sprint 6

- What went well:
  - The aspect ratio of the image in the console is now much closer to the aspect ratio of the original image
  - The code analysis tools no longer report any SA1101 or SA1413 warnings in the AsciiArt project.
- What could be improved:
  - There are still 90 code analysis warnings have been introduced, which is not acceptable according to the non-functional requirements, and many of them are new in this sprint.
  - We discovered during this sprint that Copilot is unable to see compilation warnings, only errors. This means that it is also not able to see code analysis warnings, and so the quality of the code does not meet our expected standards. It also seems that there is no way to configure Copilot to see code analysis warnings without a human running a command which captures them in a file which Copilot can then analyse.
- What should be done next:
  - Explore ways of automating the process of running code analysis tools and capturing the warnings in a file which Copilot can then analyse.