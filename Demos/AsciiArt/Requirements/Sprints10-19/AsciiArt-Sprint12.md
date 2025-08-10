# Sprint 12

## User stories - sprint 12

### As a developer, I want the ConsoleColorMapper class to be refactored to reduce its complexity, so that it is easier to maintain and understand.
- Given the ConsoleColorMapper class has high Cyclomatic complexity and NPath complexity,
- When the class is refactored,
- Then the class will be broken down into smaller methods or classes, each with a single responsibility, and the complexity metrics will be reduced to acceptable levels (e.g., Cyclomatic complexity below 10, NPath complexity below 100).

### As a developer, I want the colour mapping to implement an interface, so that it is easier to implement different colour mapping strategies in the future.

- Given the colour mapping logic is implemented in the ConsoleColorMapper class,
- Then the class will implement an interface (e.g., IConsoleColorMapper) that defines the contract for mapping RGB values to ConsoleColor values.
- And the interface will allow for different implementations of colour mapping strategies, such as perceptual colour distance metrics (e.g., CIEDE2000 or CIELAB).

## Sprint retrospective - sprint 12
- What went well:
  - The ConsoleColorMapper class was successfully refactored, reducing its complexity and making it easier to maintain and understand.
  - The implementation of an interface for colour mapping allows for future extensibility and different colour mapping strategies.
  - Unit test coverage for exception and error-handling paths in AsciiArtGenerator was increased, improving code reliability and maintainability.
- What could be improved:
  - There is a performance bottleneck when writing a large number of characters to the console, which could be optimised further.
- What should be done next:
  - Investigate options for improving the performance of writing to the console, such as buffering output or using asynchronous methods.