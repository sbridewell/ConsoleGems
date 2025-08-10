# Sprint 19

## User stories - sprint 19

### As a developer, I want to implement the precomputed lookup tables strategy for mapping image colours to console colours, so that I can improve the performance of the colour mapping.
- Given the AsciiArt library has a colour mapping strategy based on CIE76,
- When the precomputed lookup tables strategy is implemented,
- Then the colour mapping will be faster, as it will use precomputed tables to quickly find the closest console colour for a given image pixel’s RGB value.
  - How it works:
    - Precompute a lookup table that maps RGB values to the closest console colour, based on a chosen colour difference metric (e.g., CIE76).
  - Pros:
    - Very fast at runtime
    - Can be combined with other strategies for improved accuracy
  - Cons:
    - Requires more memory to store the lookup tables
    - Less flexible than dynamic calculations

### As a developer, I want to implement the dithering strategy for mapping image colours to console colours, so that I can improve the visual quality of the ASCII art.
- Given the AsciiArt library has a colour mapping strategy based on CIE76,
- When the dithering strategy is implemented,
- Then the visual quality of the ASCII art will be improved, as dithering will be used to create the illusion of more colours by mixing available console colours.
  - How it works:
    - Apply dithering algorithms (e.g., Floyd-Steinberg) to distribute quantization error across neighbouring pixels, creating a more visually appealing result.
  - Pros:
    - Improves perceived colour depth
    - Can create smoother gradients
  - Cons:
    - Adds complexity to the rendering process
    - May not be suitable for all types of images

## Sprint retrospective - sprint 19
- What went well:
  - Two more strategies for mapping colours have been implemented.
- What could be improved:
  - Checking for compilation warnings, failing unit tests and unit test coverage is still very manual, with no clear way to automate it, as this project has no CI pipeline.
- What should be done next:
