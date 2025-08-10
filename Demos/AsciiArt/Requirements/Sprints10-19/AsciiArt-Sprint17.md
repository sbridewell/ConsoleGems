# Sprint 17

## User stories - sprint 17

### As a developer, I want to implement the CIE94 strategy for mapping image colours to console colours, so that I can improve the colour mapping accuracy compared to the CIE76 strategy.
- Given the AsciiArt library has a colour mapping strategy based on CIE76,
- When the CIE94 strategy is implemented,
- Then the colour mapping will be more accurate for small colour differences, and the CIE94 algorithm will be used to calculate the colour difference between the image pixel’s RGB value and each console colour’s RGB value.
  - How it works:
    - An improvement over CIE76, CIE94 introduces weighting factors to better model human perception, especially for small colour differences.
  - Pros:
    - More accurate for small colour differences
    - Widely used in industry
  - Cons:
    - More complex than CIE76
    - Still not perfect for all colours

## Sprint retrospective - sprint 17
- What went well:
  - The CIE94 strategy was successfully implemented, improving the colour mapping accuracy compared to the CIE76 strategy.
  - The AsciiArt library now supports multiple colour mapping strategies, allowing for greater flexibility and accuracy in rendering images.
- What could be improved:
  - The CEI94 strategy doesn't result in a good mapping of image colours to console colours.
- What should be done next:
  - Implement the remaining colour mapping strategies and compare them to determine which one provides the best overall colour mapping accuracy.
  - Implement a way for the user to select at runtime which colour mapping strategy to use, so that they can choose the one that best suits their needs.