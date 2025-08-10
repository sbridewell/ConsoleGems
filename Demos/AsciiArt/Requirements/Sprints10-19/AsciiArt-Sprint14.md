# Sprint 14

## User stories - sprint 14

### As a developer, I want to implement automated document formatting, so that the code is consistently formatted according to the project's coding standards, and the Copilot agent does not need to attempt to fix document formatting issues as part of its code generation cycle.
- Given the project has a set of coding standards and formatting rules,
- When I run the Build-AndTest.ps1 script,
- When the dotnet-format tool is globally installed,
- Then the dotnet-format tool will be run on the AsciiArt and AsciiArt.Test projects

- Given the project has a set of coding standards and formatting rules,
- When I run the Build-AndTest.ps1 script,
- When the dotnet-format tool is not globally installed,
- Then the dotnet-format tool will be globally installed,
- Then the dotnet-format tool will be run on the AsciiArt and AsciiArt.Test projects

### As a developer, I want to better understand different colour mapping strategies (e.g., CEILUV, CIE76, CIE94, Delta E, CIEDE2000 or CIELAB), so that write appropriate user stories for the ones that I want to implement.

- Given there are different algorithms and strategies for mapping image colours to console colours
- I want to understand the differences between them, so that I can choose the most appropriate ones for my needs.

#### 1. Euclidean Distance in RGB
  - How it works:
    - Calculates the straight-line (Euclidean) distance between the image pixel’s RGB value and each console colour’s RGB value. The closest match is chosen.
  - Pros:
    - Simple and fast to compute
    - Easy to implement
  - Cons:
    - Does not account for human perception (e.g., we perceive green more strongly than blue)
    - Can produce visually inaccurate results for some colours

#### 2. CIE76 (ΔE*ab) in CIELAB
  - How it works:
    - Converts RGB to CIELAB (a perceptually uniform colour space), then uses Euclidean distance to find the closest console colour.
  - Pros:
    - More perceptually accurate than RGB
    - Still relatively simple
  - Cons:
    - Requires RGB-to-LAB conversion (slower than RGB)
    - CIE76 is not perfect for all colour differences

#### 3. CIE94
  - How it works:
    - An improvement over CIE76, CIE94 introduces weighting factors to better model human perception, especially for small colour differences.
  - Pros:
    - More accurate for small colour differences
    - Widely used in industry
  - Cons:
    - More complex than CIE76
    - Still not perfect for all colours

#### 4. CIEDE2000
  - How it works:
    - The most advanced CIELAB-based metric, CIEDE2000 further refines the calculation to better match human perception, especially for blues and neutrals.
  - Pros:
    - Most perceptually accurate of the CIE algorithms
    - Industry standard for colour difference
  - Cons:
    - Computationally expensive
  - More complex to implement

#### 5. CEILUV
  - How it works:
    - Similar to CIELAB, but uses the LUV colour space, which is also designed to be perceptually uniform.
  - Pros:
    - Good for certain applications (e.g., lighting)
  - Cons:
    - Less commonly used than CIELAB
    - Conversion from RGB is less standard

#### 6. Weighted RGB Distance
  - How it works:
    - Like Euclidean RGB, but applies weights to R, G, and B channels to better match human perception (e.g., green is weighted more).
  - Pros:
    - Simple improvement over plain RGB
    - Fast
  - Cons:
    - Still less accurate than CIELAB-based methods

#### 7. Precomputed Lookup Tables
  - How it works:
    - Precompute the best console colour for each possible RGB value (or a subset), then use a fast lookup at runtime.
  - Pros:
    - Extremely fast at runtime
  - Cons:
    - High memory usage if not quantized
    - Less flexible for dynamic palettes

#### 8. Dithering (Floyd–Steinberg, etc.)
 - How it works:
    - Instead of mapping each pixel to the nearest console colour, distributes the error to neighbouring pixels to create the illusion of more colours.
  - Pros:
    - Can improve perceived image quality
  - Cons:
    - More complex
    - Can introduce visual noise

Summary Table:

| Algorithm/Strategy        | Accuracy (Perceptual) | Speed | Complexity | Notes                        |
|---------------------------|-----------------------|-------|------------|------------------------------|
| Euclidean RGB             | Low                   | High  | Low        | Simple, not perceptual       |
| Weighted RGB              | Medium                | High  | Low        | Slightly better than RGB     |
| CIE76 (ΔE*ab)             | Medium                | Med   | Med        | Perceptual, but not perfect  |
| CIE94                     | High                  | Med   | Med-High   | Better for small differences |
| CIEDE2000                 | Very High             | Low   | High       | Best perceptual match        |
| CEILUV                    | Medium                | Med   | Med        | Less common                  |
| Precomputed Lookup Table  | Varies                | Very High | Med-High | Fast, memory tradeoff      |
| Dithering                 | High (visual)         | Low   | High       | Improves perceived quality   |

## Sprint retrospective - sprint 14
- What went well:
  - The team gained a better understanding of different colour mapping strategies, which will inform future user stories and implementations.
- What could be improved:
  - The Copilot agent still struggles with formatting issues, which can slow down development. Attempts to use the dotnet-format tool to automatically format documents have not been successful.
- What should be done next:
  - Write user stories for the other colour mapping algorithms.
  - Implement the colour mapping user story which provides the best trade-off between accuracy and speed.