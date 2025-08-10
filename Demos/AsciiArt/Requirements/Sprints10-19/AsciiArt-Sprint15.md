# Sprint 15

## User stories - sprint 15

### As a developer, I want to implement the weighted RGB distance strategy for mapping image colours to console colours, so that I can improve the colour mapping accuracy compared to the Euclidean distance in RGB.
- Given the AsciiArt library has a colour mapping strategy based on Euclidean distance in RGB,
- When the weighted RGB distance strategy is implemented,
- Then the colour mapping will be more accurate, especially for small colour differences, and the weighted RGB distance algorithm will be used to calculate the colour difference between the image pixel’s RGB value and each console colour’s RGB value.
  - How it works:
    - Similar to Euclidean distance in RGB, but applies different weights to the R, G, and B channels to better match human perception.
  - Pros:
    - More perceptually accurate than simple RGB
    - Still relatively simple
  - Cons:
    - Requires tuning of weights for best results
    - Not as widely used as CIELAB or CIELUV

## Sprint retrospective - sprint 15
- What went well:
  - The weighted RGB distance strategy was successfully implemented, improving the colour mapping accuracy compared to the Euclidean distance in RGB.
  - The AsciiArt library now supports multiple colour mapping strategies, allowing for greater flexibility and accuracy in rendering images.
- What could be improved:
  - The weighted RGB distance strategy requires tuning of weights for best results, which can be time-consuming.
- What should be done next:
  - Implement one of the other strategies for mapping image colours to console colours, such as CIE76, CIE94, CIEDE2000, or CEILUV, to further improve the colour mapping accuracy.