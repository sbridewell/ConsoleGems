# Sprint 16

## User stories - sprint 16

### As a developer, I want to implement the CIE76 strategy for mapping image colours to console colours, so that I can improve the colour mapping accuracy compared to the Euclidean distance in RGB.
- Given the AsciiArt library has a colour mapping strategy based on Euclidean distance in RGB,
- When the CIE76 strategy is implemented,
- Then the colour mapping will be more accurate, especially for small colour differences, and the CIE76 algorithm will be used to calculate the colour difference between the image pixel’s RGB value and each console colour’s RGB value.
  - How it works:
    - Converts RGB to CIELAB (a perceptually uniform colour space), then uses Euclidean distance to find the closest console colour.
  - Pros:
    - More perceptually accurate than RGB
    - Still relatively simple
  - Cons:
    - Requires RGB-to-LAB conversion (slower than RGB)
    - CIE76 is not perfect for all colour differences

## Sprint retrospective - sprint 16
- What went well:
  - The CIE76 strategy was successfully implemented, improving the colour mapping accuracy compared to the Euclidean distance in RGB.
  - The AsciiArt library now supports multiple colour mapping strategies, allowing for greater flexibility and accuracy in rendering images.
- What could be improved:
  - None of the colour mapping strategies implemented so far provide a good mapping of image colours to console colours, but they are different in their pros and cons.
- What should be done next:
  - Implement the CIE94 strategy for mapping image colours to console colours, as it is an improvement over CIE76 and introduces weighting factors to better model human perception, especially for small colour differences.