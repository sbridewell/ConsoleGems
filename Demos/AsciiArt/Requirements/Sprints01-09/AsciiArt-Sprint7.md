# Sprint 7

## User stories - sprint 7

### As a developer, I want all compilation warnings to be fixed, so that I am working with a clean codebase, and any newly introduced warnings are clearly visible
- Given the Copilot agent has generated code for the project
- Given I have run the Capture-CompilationWarnings.ps1 PowerShell script and the output is saved to bin/compiler_warnings.txt
- When I prompt the Copilot agent to fix any compilation warnings and errors, including code analysis warnings
- Then the Copilot agent will read the file and fix all warnings and errors reported in that file
- Then if any warnings or errors are not fixed, the Copilot agent will explain why it was not able to fix them
- Then the developer will rerun the Capture-CompilationWarnings.ps1 PowerShell script to ensure that all warnings have been resolved

## Sprint retrospective - sprint 7
- What went well:
  - The Copilot agent was able to read the file containing compilation warnings and errors, and fix all of them.
  - The code analysis tools no longer report any warnings in the AsciiArt project.
- What could be improved:
  - Getting Copilot to fix the warnings is a very iterative and time-consuming process, and it would be better if Copilot could fix all warnings in one go.
  - One of the unit tests is now failing, which is not acceptable according to the non-functional requirements.
- What should be done next:
  - Configure .csproj files to treat warnings as errors, as this appears to be the best way to ensure that Copilot fixes all warnings.
  - Make it clearer to Copilot that unit tests must be run after making any changes to the code, and that any failing unit tests must be fixed before the sprint is considered complete.