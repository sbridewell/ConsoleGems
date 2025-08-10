# Sprint 3

## User stories - sprint 3

### As a developer, I want to ensure that all platform-specific code is guarded with runtime checks and throws `PlatformNotSupportedException` with a clear message, so that the code is portable and does not crash on unsupported platforms.
- Given I have written platform-specific code in the project
- When a line of code uses platform-specific functionality
- Then the code must be guarded with a runtime check to determine if the current platform supports the functionality, and if not, it must throw a `PlatformNotSupportedException` with a clear message indicating that the platform is not supported
- Then the compiler will not return any CA1416 warnings

### As a developer, I want all commas in the code to be followed by whitespace, so that the code is more readable.
- Given I have written code in the project
- When the line of code contains a comma
- Then the comma must be followed by either a single space character or a line break

### As a developer, I want all opening braces in the code to be followed by whitespace, so that the code is more readable.
- Given I have written code in the project
- When the line of code contains an opening brace
- Then the opening brace must be followed by a single space character or a line break

### As a developer, I want all closing braces in the code to be preceded by whitespace, so that the code is more readable.
- Given I have written code in the project
- When the line of code contains a closing brace
- Then the closing brace must be preceded by a single space character or a line break

## Sprint retrospective - sprint 3
- What went well:
- What could be improved:
  - The code analysis tools reported 162 warnings, which is not acceptable according to the non-functional requirements.
  - The code does not follow the conventions for using whitespace to make code more readable.
  - The approach to guarding platform-specific code with runtime checks and throwing `PlatformNotSupportedException` doesn't actually address the CA1416 warnings, as the code is still not guarded correctly.
- What should be done next:
  - Write a more specific user story for guarding platform-specific code with compile-time checks using the `SupportedOSPlaform` attribute rather than runtime checks.
