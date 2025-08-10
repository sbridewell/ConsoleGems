# Sprint 4

## User stories - sprint 4

### As a developer, I want to use the `SupportedOSPlatform` attribute to guard platform-specific code, so that the code is portable and does not generate CA1416 warnings.
- Given I have written platform-specific code in the project
- When any method uses platform-specific functionality, regardless of whether it is publicly accessible or not
- Then the method must be decorated with the `SupportedOSPlatform` attribute to indicate which platforms it supports

## Sprint retrospective - sprint 4
- What went well:
  - The code now uses the `SupportedOSPlatform` attribute to guard platform-specific code, which resolves the CA1416 warnings.
- What could be improved:
  - The code analysis tools still report 148 warnings, which is not acceptable according to the non-functional requirements.
  - We have now spent 3 sprints trying to embed a culture of quality into the codebase, but we haven't delivered any new functionality in this time. The product owner isn't happy about this. I have explained to her the importance of embedding quality over writing lots of code which is riddled with warnings and other issues, and she agrees with the principle, but her patience is limited.
- What should be done next:
  - Write a Definition of Done for the code analysis and quality requirements, so that we can ensure that the code meets the quality standards before we move on to implementing new functionality.
