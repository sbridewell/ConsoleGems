# Sprint 1

## User stories - sprint 1

### As a developer, I want to be able to read an image file and get the pixel colours in memory so that I can manipulate them.
- Given an image file in a format supported by the .NET `Image` class
- When the image is read into memory
- Then the pixel colours should be available as red, green, blue values from 0 to 255
- Then the red, green and blue values for a single pixel should be stored in a `Pixel` class with properties for each colour component

### As a developer, I want to map RGB values to console colours, so that I can display the image in the console.
- Given a pixel with red, green and blue values from 0 to 255
- When the pixel is mapped to a console colour
- Then the colour of the pixel should be mapped to the closest console colour out of the Black, Blue, Green, Cyan, Red, Magenta, Yellow and White members of the `ColsoleColor` enum and must be displayed in the console as a space character with the mapped console colour as the background colour.

### As a user, I want to be able to run the code delivered in sprint 1, so that I can provide fast feedback to the developers.
- Given the developers have already implemented a console application which includes a way of passing a file path to the `IAsciiArtRenderer.Render` method
- When I use the console application to pass an image file path to the `Render` method
- Then the `Render` method should read the image file, map the pixel colours to console colours, and display the image in the console as a series of space characters with the mapped console colours as the background colour.

## Sprint retrospective - sprint 1
- What went well:
  - The program was able to read image files and extract pixel colours successfully.
  - The console application was set up to allow for easy testing of the functionality.
- What could be improved:
  - Although the representation of the image in the console is recognisable, it does not preserve the aspect ratio of the original image.
  - The program is mapping some colours to `ConsoleColor.Gray`, which is not one of the colours which is in scope for this sprint.
  - The program does not appear to be mapping any colours to the Cyan or Magenta console colours.
  - There are 160 compilation warnings. The non-functional requirements state that all compilation warnings must be fixed, so this is a significant issue.
  - The unit tests do not cover 100% of the Sde.AsciiArt.AsciiArtGenerator class, in particular the exception paths are not covered.
  - I'm not sure that I'm articulating the requirements clearly enough, as the Copilot agent mode is generating code which does not meet my functional or quality expectations.
- What should be done next:
  - Refine the PRD to explicitly state that all compilation and analyzer warnings must be resolved before a sprint is considered complete.
  - Add a checklist or acceptance criteria section to each sprint/user story, including non-functional requirements (e.g., “No warnings”, “100% test coverage”, “XML documentation complete”).
  - Clarify the scope of supported ConsoleColor values for each sprint, and ensure mapping logic and tests only use those values.
  - Require unit tests to cover all code paths, including exception handling and platform-specific branches.
  - Specify that test coverage reports should be generated and reviewed for each sprint.
  - Emphasize the need for platform checks and PlatformNotSupportedException handling in the PRD and user stories.
  - Mandate alphabetical ordering and placement of using directives within the namespace in code review criteria.
  - Require that all arithmetic expressions in code use explicit parentheses for precedence.
  - Add examples of compliant and non-compliant code for naming conventions and documentation in the PRD.
  - Encourage feedback loops: after each sprint, update the PRD with lessons learned and clarifications to avoid repeated misunderstandings.
