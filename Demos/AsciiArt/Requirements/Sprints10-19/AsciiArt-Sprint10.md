# Sprint 10

## User stories - sprint 10

### As a developer, I want colour mapping unit tests to be parameterised, so that I can easily add new test cases without duplicating code.

- Given the current unit tests for colour mapping,
- When the tests are refactored to use a parameterised test framework (e.g., xUnit's [Theory]),
- Then the tests can accept multiple sets of RGB values and their expected ConsoleColor mappings in a single test method.

### As a developer, I want the logic for colour mapping to be more thoroughly commented, so that it is easier to understand how the mapping works and why certain decisions were made.

- Given the colour mapping logic is implemented in the ConsoleColorMapper class,
- When some of the code in the class is refactored into a separate method,
- Then the new method will have a clear and concise name that describes its purpose, and will be accompanied by XML documentation comments that explain how it works and why it is necessary.
- Then the new method will not be excessively long, ideally 40 lines or fewer, so that it is easy to read and understand.

### As a developer, I want shades of primary colours to be mapped to a range of ConsoleColor values from black to the brightest shade of the primary colour, with no grey or white at any point, so that the colours are mapped correctly.

- Given the colour of a pixel in the original image has green and blue values of 0,
- When the colour is mapped to a ConsoleColor value,
- Then the resulting ConsoleColor value will will be one of the following: Black, DarkRed, Red

- Given the colour of a pixel in the original image has red and blue values of 0,
- When the colour is mapped to a ConsoleColor value,
- Then the resulting ConsoleColor value will will be one of the following: Black, DarkGreen, Green

- Given the colour of a pixel in the original image has red and green values of 0,
- When the colour is mapped to a ConsoleColor value,
- Then the resulting ConsoleColor value will will be one of the following: Black, DarkBlue, Blue

## Sprint retrospective - sprint 10
- What went well:
  - The unit test for mapping RGB colours to console colours is now parameterised, allowing for easy addition of new test cases without duplicating code.
- What could be improved:
  - The unit tests are not focussed enough - there should be a one to one mapping between unit test classes and the classes they are testing, and the unit tests should be focussed on a single class or method.
  - When the program is run as a console application the colour mapping does not seem to work correctly. Shades of primary colours are mostly mapped to Gray, DarkGray or White instead.
- What should be done next:
  - Refactor the unit tests to ensure that each test class is focused on a single class or method, and that the tests are clear and concise.
  - Debug the unit tests for ConsoleColorMapper to identify why the colour mapping does not work correctly when the program is run as a console application.