### As a speaker of en-GB, I want words in class names, method names, property names, variable names, parameter names and comments to be spelled in British English, so that the code is consistent with my language preferences.

- Given a word refers to a colour, such as "color" or "colour",
- Then the word will be spelled "Colour".

- Given a word refers to the colour grey, such as "gray" or "grey",
- Then the word will be spelled "Grey".


### As a developer, I want to add a neural network-based character blender, so that the quality of ASCII art rendering can be further improved and modern AI techniques can be explored.

**User Story:**
- As a developer, I want to implement an ICharacterBlender that uses a neural network to select the best character for each pixel, so that ASCII art output is more visually accurate and flexible.
- As a developer, I want clear guidance on the neural network architecture, input features, training data, integration approach, and user experience, so that the implementation is technically sound and meets project requirements.

**Investigation & Decision Points:**
- What type of neural network architecture should be used (e.g., feed-forward, convolutional, pre-trained, custom)?
- What input features should the model accept (e.g., foreground/background ratio, pixel intensity, local context)?
- What outputs should the model produce (e.g., character index, probability distribution)?
- Is there an existing dataset for training, or should one be created? What is its format and source?
- Should the model be trained within the project (e.g., ML.NET), or should an external model (e.g., ONNX) be loaded for inference?
- Are there any performance or platform constraints to consider?
- How should the user select and configure the neural network blender in the UI/menu?
- What are the expected benchmarks or quality metrics for output? Are there specific test images or scenarios for validation?

**Acceptance Criteria:**
- Given the AsciiArt library supports multiple character blenders,
  When a neural network-based character blender is implemented and integrated,
  Then users can select it as an option and compare its output to traditional blenders.
- Given a suitable dataset and model are available,
  When the neural network is trained and evaluated,
  Then its output quality is validated against existing approaches.
- Given the new blender is available,
  When the user selects the "best quality" mode,
  Then the neural network blender is listed as an option and can be used for rendering.
- Given the new code is added,
  When the project is built and tested,
  Then all new code is covered by unit tests and includes XML documentation comments.
- Given the technical investigation is complete,
  When the design decisions are documented,
  Then implementation can proceed with clear requirements and constraints.

#### What type of neural network architecture should be used (e.g., feed-forward, convolutional, pre-trained, custom)?

##### Feed-Forward Neural Network (Fully Connected / MLP)
- **Description:** Each neuron in one layer is connected to every neuron in the next. Simple and commonly used for tabular or basic image data.
- **Pros:**
  - Easy to implement with libraries like ML.NET or TensorFlow.NET
  - Runs entirely locally
  - Good for simple input features (e.g., ratio, pixel intensity)
- **Cons:**
  - Limited ability to capture spatial relationships in images
  - May not perform as well as more advanced architectures for complex image tasks
- **Complexity:** Low to moderate
- **Dependencies:** Local only

##### Convolutional Neural Network (CNN)
- **Description:** Uses convolutional layers to extract spatial features from images. State-of-the-art for image processing.
- **Pros:**
  - Excellent at capturing spatial and local context in images
  - Can produce high-quality results for image-to-character mapping
  - Can be implemented locally with ML.NET (limited), TensorFlow.NET, or ONNX
- **Cons:**
  - More complex to implement and train
  - Requires more compute resources
- **Complexity:** Moderate to high
- **Dependencies:** Local (if using ONNX or TensorFlow.NET); can use cloud APIs for training/inference if desired

##### Pre-Trained Neural Network
- **Description:** Uses a model trained elsewhere (e.g., on a large dataset) and imported for inference.
- **Pros:**
  - No need to train from scratch
  - Can leverage high-quality models
  - Can run locally if using ONNX or similar formats
- **Cons:**
  - May require conversion and integration work
  - May not be tailored to your specific use case
- **Complexity:** Moderate (integration), low (usage)
- **Dependencies:** Local (ONNX, TensorFlow.NET) or external (if using cloud APIs)

##### Custom Neural Network
- **Description:** Any architecture designed specifically for your use case, possibly combining elements of the above.
- **Pros:**
  - Can be tailored to your exact requirements
  - Flexible
- **Cons:**
  - Requires expertise in neural network design
  - More difficult to implement and test
- **Complexity:** High
- **Dependencies:** Local or external, depending on implementation

##### Recurrent Neural Network (RNN)
- **Description:** Designed for sequential data, not typically used for image processing.
- **Pros:**
  - Good for time-series or sequence data
- **Cons:**
  - Not suitable for static image-to-character mapping
- **Complexity:** Moderate
- **Dependencies:** Local or external

##### External API (Cloud-based Inference)
- **Description:** Use a 3rd party service (e.g., Azure, AWS, Google) to run neural network inference.
- **Pros:**
  - No local compute required
  - Can use very advanced models
- **Cons:**
  - Requires internet connection
  - May incur costs
  - Data privacy concerns
- **Complexity:** Low (usage), moderate (integration)
- **Dependencies:** External

---
**Summary Table:**

| Architecture         | Quality | Complexity | Local | External Dependency |
|---------------------|---------|------------|-------|--------------------|
| Feed-Forward (MLP)  | Medium  | Low-Med    | Yes   | No                 |
| CNN                 | High    | Med-High   | Yes   | Optional           |
| Pre-Trained         | High    | Med        | Yes   | Optional           |
| Custom              | Varies  | High       | Yes   | Optional           |
| RNN                 | Low     | Med        | Yes   | Optional           |
| External API        | High    | Low-Med    | No    | Yes                |

---
**Recommendation:**
- For most local, simple use cases, start with a Feed-Forward Neural Network (MLP).
- For best image quality and if you have the resources, consider a CNN or a pre-trained model.
- Use external APIs only if local compute is insufficient or you need advanced features.

#### Libraries and tools (ONNX, TensorFlow.NET, ML.NET etc)

##### ML.NET
- **Description:** ML.NET is a machine learning framework for .NET, developed by Microsoft. It allows you to build, train, and deploy custom machine learning models directly in C#.
- **Benefits:**
  - Native .NET integration
  - Supports training and inference for various ML models, including neural networks
  - Good documentation and community support
- **Type:** NuGet package
- **Compatibility:** Fully compatible with C#/.NET projects
- **Unit Testing:** Code using ML.NET is easily unit testable; you can mock data and test model predictions

##### TensorFlow.NET
- **Description:** TensorFlow.NET is a .NET binding for TensorFlow, enabling you to use TensorFlow's deep learning capabilities in C#.
- **Benefits:**
  - Access to advanced deep learning features
  - Can load and run TensorFlow models (including pre-trained ones)
  - Supports both training and inference
- **Type:** NuGet package
- **Compatibility:** Compatible with C#/.NET projects
- **Unit Testing:** Code using TensorFlow.NET is unit testable, but may require more setup for mocking model behaviour

##### ONNX Runtime
- **Description:** ONNX Runtime is a cross-platform inference engine for models in the ONNX (Open Neural Network Exchange) format. ONNX models can be exported from many frameworks (PyTorch, TensorFlow, etc.) and run efficiently in .NET.
- **Benefits:**
  - Fast inference for pre-trained models
  - Supports models from many ML frameworks
  - Lightweight and efficient
- **Type:** NuGet package
- **Compatibility:** Compatible with C#/.NET projects
- **Unit Testing:** Code using ONNX Runtime is unit testable; you can mock inputs and outputs for inference

##### Keras.NET
- **Description:** Keras.NET is a .NET binding for Keras, a high-level neural network API. It allows you to build and train models using Keras in C#.
- **Benefits:**
  - Simple API for building neural networks
  - Can leverage Keras's ease of use
- **Type:** NuGet package
- **Compatibility:** Compatible with C#/.NET projects
- **Unit Testing:** Unit testing is possible, but may require more setup for model training and inference

##### Command-Line Tools (ONNX Converter, TensorFlow CLI, etc.)
- **Description:** Tools for converting models between formats (e.g., TensorFlow to ONNX) or managing model files.
- **Benefits:**
  - Enable interoperability between frameworks
  - Useful for preparing models for use in .NET projects
- **Type:** Command-line tools
- **Compatibility:** Used outside C# code, but results (converted models) are compatible with .NET via ONNX Runtime
- **Unit Testing:** Not directly testable in C#, but converted models can be tested in code

---
**Summary Table:**

| Tool/Library     | Type           | .NET Compatible | Unit Testable | Use Case                       |
|------------------|----------------|-----------------|---------------|--------------------------------|
| ML.NET           | NuGet package  | Yes             | Yes           | Training/inference in .NET     |
| TensorFlow.NET   | NuGet package  | Yes             | Yes           | Advanced deep learning in .NET |
| ONNX Runtime     | NuGet package  | Yes             | Yes           | Fast inference, pre-trained    |
| Keras.NET        | NuGet package  | Yes             | Yes           | Simple neural networks         |
| ONNX Converter   | CLI tool       | Indirect        | Indirect      | Model format conversion        |

---
**Recommendation:**
- For most .NET projects, ML.NET and ONNX Runtime are the easiest to integrate and test.
- Use TensorFlow.NET or Keras.NET for more advanced or custom deep learning needs.
- Use command-line tools for model conversion and preparation.
