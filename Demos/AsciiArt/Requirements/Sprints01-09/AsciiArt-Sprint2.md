# Sprint 2

## User stories - sprint 2

### As a developer, I want a clean codebase with no warnings, so that I can ensure the quality of the code.
- Given I have asked GitHub Copilot to generate code for the project
- When I run the code analysis tools on the project
- Then there should be no warnings or errors reported by the code analysis tools, including StyleCop, SonarAnalyzer, and Microsoft.CodeAnalysis.

### As a developer, I want the code to be well documented with XML comments on all classes and members, so that I can understand the code and its purpose.
- Given I have asked GitHub Copilot to generate code for the project
- When I look at the code in the project
- Then all classes, interfaces, enums, and methods should have XML documentation comments which explain their purpose and usage.

## Sprint retrospective - sprint 2
- What went well:
  - The XML documentation comments were added to all classes, interfaces, enums, and methods.
- What could be improved:
  - The code analysis tools reported 160 warnings, which is not acceptable according to the non-functional requirements.
  - Arithmetic expressions do not use parentheses to declare precedence, making the order of operations unclear.
  - There are numerous CA1416 warnings, indicating that platform-specific code is not guarded with runtime checks and does not throw `PlatformNotSupportedException` with a clear message.
  - There are numerous SA1001, SA1012, and SA1013 warnings, indicating that the code does not follow the conventions for using whitespace to make code more readable.
- What should be done next:
  - Write user stories to address the specific code analysis warnings, including:
    - CA1416: Platform-specific code must be guarded with runtime checks and throw `PlatformNotSupportedException` with a clear message.
    - SA1001: Commas should be followed by whitespace.
    - SA1012: Opening brace should be followed by a space.
    - SA1013: Closing brace should be preceded by a space.
