# Sprint 13

## User stories - sprint 13

### As a developer, I want to understand the performance bottleneck when writing a large number of characters to the console, so that I can write appropriate user stories to resolve the performance issues

- Given the AsciiArt library writes a large number of characters to the console,
- When the performance is measured,
- Then the performance bottleneck will be identified, such as the time taken to write characters to the console or the number of characters written per second.

## Sprint retrospective - sprint 13
- What went well:
  - The performance bottleneck when writing a large number of characters to the console was identified, allowing for targeted improvements.
  - Writing to the console now has better performance when writing a large number of characters.
- What could be improved:
  - The Copilot agent seems to have difficulty resolving code analysis warnings and errors relating to whether or not a blank line is required in a particular place, which considerably slows down the development process.
- What should be done next:
  - Automate StyleCop and Code Analysis Fixes. Since StyleCop rule handling slowed down development, consider integrating automatic code formatting and analysis into your CI pipeline or pre-commit hooks. Tools like dotnet-format or EditorConfig can help enforce style rules automatically.
    - dotnet-format
      - Install the tool globally - `if (-not (dotnet tool list -g | Select-String 'dotnet-format')) { dotnet tool install -g dotnet-format }` if not already installed.
      - Use the tool to format documents - `dotnet format [project or solution name]`
  - Document Coding Standards. Create or update a coding standards document that clearly explains how to handle common StyleCop rules, especially those that have caused confusion (e.g., blank line placement). This will help current and future contributors.
  - Consider adding performance regression tests. Now that console performance has improved, add automated performance regression tests to ensure future changes do not degrade performance.
