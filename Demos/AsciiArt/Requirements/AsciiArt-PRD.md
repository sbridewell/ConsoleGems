# Product Requirements Document (PRD)

## Project Overview
- **Product Name:** ASCII Art
- **Author:** Simon Bridewell
- **Date:** Jul 2025
- **Version:** 0.1

## Objective
Create a C# library which can examine the pixels in an image file and generate a full colour ASCII art representation of the image.

## Background
There is an existing project written mostly in C which can generate ASCII art, but it has not been updated in 4 years, and I don't know enough C to work out how to build it. The project can be found at https://github.com/cacalabs/libcaca.

The development team consists of only two members. One is a human who is acting as product owner, designer, lead developer and quality assurance engineer. The other is a GitHub Copilot agent which is acting as a developer, and is capable of writing code in C#.

The solution is developed using Visual Studio 2022 Community Edition, running on a Windows 11 physical laptop.

## Requirements

The solution will be delivered in an agile manner, in the form of multiple sprints, or iterations, each of which implements a small increment on the previous iteration.

Note: All requirements in this section apply to every sprint and every user story, unless explicitly overridden in a later section. Sprints and user stories must be implemented and reviewed in accordance with these requirements. The sprint is not considered complete until all of these requirements are met.

### Functional Requirements
- The program must be able to read an image file of any format supported by the .net `Image` class, and represent the colours of its pixels in memory as red, green, blue values from 0 to 255.
- The program must determine the width and height of the console / terminal, and map each available character in the console / terminal to a range of pixels in the original image, such that the proportion of the console / terminal occupied by that character is the same as the proportion of the original image occupied by the corresponding range of pixels.
- The program must make use of the colours available in the `System.Console` class, and map the RGB colours in the original image to colours which can be represented by console colours. 
- The program must maintain the original aspect ratio of the image when displaying it in the console / terminal.

### Non-Functional Requirements
- Performance
  - The `GetPixel` and `SetPixel` methods of the `Bitmap` class are very slow when when used in a loop, so the program must use a more efficient method to read and write pixel data. An example of this is the `LockBits` method, which allows you to access the pixel data directly.
- Code quality standards
  - Each class, interface and enum must go into its own file, with the file name matching the class, interface or enum name.
  - Every project must include the `stylecop.json` file from the root of the solution, which contains the StyleCop settings for the project. This should be a link to the file in the root of the solution, not a copy of the file.
  - All projects must include the following code analyser packages:
    - Microsoft.CodeAnalysis.CSharp
    - Microsoft.CodeAnalysis.CSharp.Workspaces
    - Microsoft.CodeAnalysis.NetAnalysers
    - SonarAnalyzer.CSharp
    - StyleCop.Analyzers
  - All compilation and code analysis warnings (including CAxxxx, SAxxxx, etc.) must be resolved. Platform-specific code must not generate compatibility warnings (e.g., CA1416); use runtime checks, conditional compilation, or documented suppression with justification. The solution must build cleanly with zero warnings or errors.
  - All projects must be configured to create XML documentation files, and the XML documentation must be complete.
  - Using directives must be ordered alphabetically, with `System` namespaces first, followed by other namespaces in alphabetical order.
  - Using directives must be within a namespace block, and not in the global namespace.
  - Arithmetic expressions must declare precendence using parentheses, so that the order of operations is clear.
  - All methods in the core library must have names which start with a verb, and be in PascalCase.
  - All variable names and parameter names must be made of complete words, not abbreviations, and be in camelCase.
- Unit testing
  - The core library must be unit tested using xUnit and FluentAssertions.
  - All unit tests must pass.
  - The unit tests must cover 100% of the code in the core library.
  - Unit tests must include coverage for all normal, error, and platform-specific code paths, including exception handling and conditional logic.
  - All platform-specific code must be guarded with runtime checks and throw PlatformNotSupportedException with a clear message if unsupported. Unit tests must verify this behavior.
  - To assist the developer in debugging issues in the unit tests, unit tests which test the conversion of an image to ASCII art must:
    - save the original image to a file
    - save the resulting ASCII art to a HTML file which visually preserves the colour of each character using inline CSS styles (e.g., <span style='color:#RRGGBB;background-color:#RRGGBB;'>char</span>)
      -  Saving the ASCII art as a plain text file is not sufficient; the output must be a valid HTML file that displays the ASCII art with the correct colours when opened in a web browser.
    - write the paths to both files to the unit test output, so that they can be inspected by the developer.
- The core library must make use of interfaces where appropriate, to allow for dependency injection and easier unit testing. It must include implementations of each interface.
- The main entry point into the core library must be a class which implements the `IAsciiArtGenerator` interface, with the following methods:
  - `void RenderImageAsAsciiArt(string imagePath, IConsole console);`

## Definition of Done
- A user story is considered done when:
  - All functional requirements for the user story have been implemented and tested.
  - All non-functional requirements for the user story have been met.
  - The code has been reviewed and approved by the lead developer.
  - The code has been documented with XML comments, and all public members are documented.
  - All unit tests have been written, are passing, and cover all code paths, including error handling and platform-specific logic.
    - Code coverage verification is optional or manual until the automated code coverage tool is implemented.
  - The solution builds cleanly with zero warnings or errors in either the AsciiArt or AsciiArt.Test projects.
- A sprint is not considered complete until all user stories in the sprint meet the definition of done.

## Technical Considerations
- Platform(s)
  - The core library should be cross-platform, and work on Windows, Linux, macOS, iOS and Android where possible.
  - Where platform-specific code is required it must be decorated with a `SupportedOSPlatform` attribute to enforce compile-time checks.
- Dependencies
  - The solution must make use of the existing `ConsoleGems` project and its 'IConsole` interface to interact with the console / terminal, rather than using the `System.Console` class directly.
  - Add any enhancements to the `ConsoleGems` project as required to support the functionality of the ASCII Art library.
- Code coverage
  - Due to issues with the Invoke-UnitTestsWithCodeCoverage cmdlet, automated code coverage analysis may be skipped or performed manually, until the issue is resolved. The Copilot agent should continue with the other Definition of Done requirements.
  - To measure the proportion of code covered by the unit tests, follow these steps:
  - Ensure that the folder containing `UnitTesting.psm1` is present in the `PSModulePath` environment variable.
  - From the root folder of the AsciiArt.Test project, run the PowerShell cmdlet `Invoke-UnitTestsWithCodeAnalysis`.
  - Read the coverage results from the file `coverage.opencover.xml` in the root folder of the AsciiArt.Test project.
  - A visual (HTML) version of the code coverage report is created in the `CodeCoverage` subfolder of the AsciiArt.Test project root, however this is intended for human consumption.
  - The minimum acceptable coverage threshold is 80% of every method and property which is not explicitly excluded from coverage, however we want to achieve 100% coverage where possible.

## Sprints

The solution will be delivered in a series of sprints, each of which implements a small increment on the previous iteration. Each sprint will consist of a set of user stories, which will be implemented and tested in accordance with the requirements outlined above. At the end of each sprint, a retrospective will be held to review the progress made, identify any issues, and plan the next steps.

In order to keep the size of the main Product Requirements Document (this document) manageable, each sprint will be documented in a separate file, which will be linked to from this section of the PRD.

| Sprint | User stories |
|--------|--------------|
| [1](AsciiArt-Sprint1.md) | <ul><li>As a developer, I want to be able to read an image file and get the pixel colours in memory so that I can manipulate them.</li><li>As a developer, I want to map RGB values to console colours, so that I can display the image in the console.</li><li>As a user, I want to be able to run the code delivered in sprint 1, so that I can provide fast feedback to the developers.</li></ul> |
| [2](AsciiArt-Sprint2.md) | <ul><li>As a developer, I want a clean codebase with no warnings, so that I can ensure the quality of the code.</li><li>As a developer, I want the code to be well documented with XML comments on all classes and members, so that I can understand the code and its purpose.</li></ul> |
| [3](AsciiArt-Sprint3.md) | <ul><li>As a developer, I want to ensure that all platform-specific code is guarded with runtime checks and throws <code>PlatformNotSupportedException</code> with a clear message, so that the code is portable and does not crash on unsupported platforms.</li><li>As a developer, I want all commas in the code to be followed by whitespace, so that the code is more readable.</li><li>As a developer, I want all opening braces in the code to be followed by whitespace, so that the code is more readable.</li><li>As a developer, I want all closing braces in the code to be preceded by whitespace, so that the code is more readable.</li></ul> |
| [4](AsciiArt-Sprint4.md) | <ul><li>As a developer, I want to use the <code>SupportedOSPlatform</code> attribute to guard platform-specific code, so that the code is portable and does not generate CA1416 warnings.</li></ul> |
| [5](AsciiArt-Sprint5.md) | <ul><li>As a product owner, I want the program to preserve the aspect ratio of the original image when displaying it in the console, so that the image is displayed correctly.</li><li>As a developer, I want to ensure that the code meets the quality standards for usage of whitespace, so that the code is more readable.</li></ul> |
| [6](AsciiArt-Sprint6.md) | <ul><li>As a user, I want the program to take into account the fact that a character on the console is taller than it is wide, so that it can better preserve the aspect ratio of the original image.</li><li>As a developer, I want all remaining code analysis warnings to be fixed, so that I am working with a clean codebase, and any newly introduced warnings are clearly visible.</li></ul> |

The documentation file for each sprint will use the following format:

```
# Sprint [insert sprint number here]

## User stories - sprint [insert sprint number here]

### As a [role], I want [feature] so that [benefit].
- Given [insert precondition here]
- When [insert action here]
- Then [insert expected outcome here]
- Then [insert additional expected outcome here, if applicable]

[Repeat the above for each user story in the sprint]

## Sprint retrospective - sprint [insert sprint number here]
- What went well:
  - [Let's celebrate our successes!]
- What could be improved:
  - [Things don't always turn out how we'd want them to, so let's learn from our mistakes!]
- What should be done next:
  - [Taking into consideration what went well and what could be improved, what should we do next to ensure that we keep doing things well, and improve on the things that didn't go so well?]
```

The lifecycle of each sprint will be as follows:
- The Copilot agent will create a new sprint documentation file in the `ProductRequirementsDocument` folder within the root folder of the AsciiArt project, using the format outlined above, and will add a row to the table above, linking to the sprint documentation file.
- The team will write the user stories for the sprint in the sprint documentation file, taking into account the "what should be done next" points captured during the retrospective from the previous sprint.
- The Copilot agent will implement the user stories in the sprint documentation file, complying with all requirements set out in this PRD, including ensuring that the code and unit tests compile with no warnings, the unit tests all pass, and that the proportion of the code which is covered by the unit tests is sufficient.
- The lead developer will review the code and the sprint documentation file, ensuring that all requirements have been met, and that the code is of high quality. This may include running the program manually to verify that it behaves as expected.
- The lead developer may request changes to the code, either by direct prompt or by updating the sprint documentation file, and the Copilot agent will make those changes.
- The team will hold a sprint retrospective, reviewing the progress made, identifying any issues, and planning the next steps. The output from the retrospective will be captured in the sprint documentation file.
- The Copilot agent will update the new row in the table above with the user stories from the sprint documentation file.