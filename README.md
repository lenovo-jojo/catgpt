# catgpt

A Unity desktop application featuring a VRM character with AI chat integration, camera access, screen capture, weather information, RSS feed reading, and expressive animations.

## Features

### Core Features
- 🤖 **AI Chat Integration** - Chat with AI assistant using OpenAI-compatible APIs
- 🎭 **VRM Character Support** - Load and display VRM (Virtual Reality Model) characters
- 📸 **Camera Access** - Capture and analyze images from your webcam
- 🖥️ **Screen Capture** - Take screenshots for AI analysis
- 🌤️ **Weather Integration** - Get real-time weather information
- 📰 **RSS Feed Reader** - Stay updated with news feeds
- ⏰ **Time Management** - Access current date and time information

### New Expression & Animation Features
- 😊 **Multiple Expressions** - Happy, Sad, Angry, Surprised, Relaxed, and more
- 💃 **Character Animations** - Wave, Nod, Shake, Dance, Celebrate, and more
- 🔄 **Model Switching** - Load and switch between multiple VRM models
- 🎨 **Auto-Expression** - AI responses automatically trigger appropriate expressions
- 🎮 **Manual Controls** - Trigger expressions and animations via UI controls

## Requirements

- Unity 2022.3.10f1 or later
- Windows/macOS/Linux desktop platform
- Webcam (for camera features)
- Internet connection (for AI, weather, and RSS features)

## Setup Instructions

### 1. API Configuration

Create an AI configuration asset:
1. In Unity, right-click in Project window
2. Select `Create > CatGPT > AI Configuration`
3. Configure the following:
   - **API Key**: Your OpenAI or compatible API key
   - **API Endpoint**: API endpoint (default: OpenAI)
   - **Model**: AI model name (e.g., "gpt-3.5-turbo")
   - **Weather API Key**: OpenWeatherMap API key
   - **City**: Your city for weather info
   - **RSS Feed URL**: Your preferred RSS feed

### 2. Scene Setup

1. Create a new scene or use existing one
2. Add an empty GameObject named "CatGPT Manager"
3. Attach the `CatGPTController` script
4. Assign the AI Config asset to the controller
5. Create UI elements for chat interface (see UI Setup below)

### 3. UI Setup

Required UI elements:
- **Chat Input Field** (TMP_InputField) - For user messages
- **Chat Output Text** (TMP_Text) - For conversation display
- **Send Button** - To send messages
- **Camera Button** - To capture camera images
- **Screen Capture Button** - To take screenshots
- **Weather Button** - To fetch weather info
- **News Button** - To fetch RSS feed
- **Camera Preview** (RawImage) - To display webcam feed

New VRM Control UI elements:
- **Expression Dropdown** (TMP_Dropdown) - Select character expressions
- **Animation Dropdown** (TMP_Dropdown) - Trigger animations
- **Next Model Button** - Switch to next VRM model
- **Previous Model Button** - Switch to previous VRM model
- **Current Model Text** (TMP_Text) - Display active model name

### 4. VRM Model Setup

#### Using Placeholder (Default)
The system creates a placeholder character automatically if no VRM files are provided.

#### Loading Real VRM Models
1. Place VRM files in your project or StreamingAssets folder
2. Add model information to the VRM Model Manager:
   - Model Name
   - File Path
   - Description
   - Thumbnail (optional)

## Available Expressions

The character supports these expressions:
- **Neutral** - Default expression
- **Happy** - Joyful, smiling
- **Sad** - Sorrowful, down
- **Angry** - Mad, upset
- **Surprised** - Shocked, amazed
- **Relaxed** - Calm, peaceful
- **Joy** - Extremely happy
- **Fun** - Playful, enjoying
- **Sorrow** - Deep sadness
- **Blink** - Eye blinking animations

## Available Animations

The character can perform these animations:
- **Idle** - Standing still
- **Wave** - Waving gesture
- **Nod** - Nodding head yes
- **Shake** - Shaking head no
- **ThumbsUp** - Approval gesture
- **Think** - Thinking pose
- **Talk** - Speaking animation
- **Dance** - Dancing motion
- **Celebrate** - Victory celebration

## Usage

### Basic Chat
1. Type your message in the input field
2. Click Send or press Enter
3. The AI will respond and the character will show appropriate expressions

### Camera Analysis
1. Click the Camera button
2. The system captures a frame from your webcam
3. AI analyzes and describes what it sees

### Screen Capture
1. Click the Screen Capture button
2. The system takes a screenshot
3. AI analyzes the screen content

### Weather Check
1. Click the Weather button
2. Current weather information is fetched
3. AI provides weather commentary

### News Reading
1. Click the News button
2. Latest RSS feed items are fetched
3. AI summarizes the news

### Expression Control
1. Use the Expression dropdown to manually set expressions
2. Or let the AI automatically set expressions based on conversation

### Animation Control
1. Use the Animation dropdown to trigger animations
2. Animations play for approximately 2 seconds

### Model Switching
1. Click Next/Previous buttons to cycle through available models
2. Current model name is displayed in the UI
3. Expressions and animations apply to the active model

## Project Structure

```
Assets/
├── Scripts/
│   ├── AIConfig.cs                    # AI configuration
│   ├── AIChatManager.cs               # AI chat handling
│   ├── CameraManager.cs               # Webcam access
│   ├── ScreenCaptureManager.cs        # Screenshot capture
│   ├── RSSFeedManager.cs              # RSS feed reading
│   ├── WeatherManager.cs              # Weather API
│   ├── TimeManager.cs                 # Time/date utilities
│   ├── VRMLoader.cs                   # VRM model loading (legacy)
│   ├── VRMExpressionController.cs     # Expression & animation system
│   ├── VRMModelManager.cs             # Model switching & management
│   └── CatGPTController.cs            # Main controller
├── Scenes/                            # Unity scenes
├── Resources/                         # Runtime resources
├── Prefabs/                           # Reusable prefabs
└── StreamingAssets/                   # VRM files and data
```

## API Keys

You'll need:
1. **OpenAI API Key** or compatible AI service
   - Get from: https://platform.openai.com/api-keys
   
2. **OpenWeatherMap API Key** (for weather)
   - Get from: https://openweathermap.org/api

## Dependencies

The project uses these Unity packages:
- TextMeshPro (for UI text)
- Universal Render Pipeline (URP)
- UniVRM (for full VRM support - optional)

## Notes

- The current implementation uses placeholder VRM characters
- For full VRM support, integrate UniVRM package from https://github.com/vrm-c/UniVRM
- Camera permissions may be required on some platforms
- API rate limits apply based on your service tier

## Extending the System

### Adding New Expressions
1. Add expression enum to `VRMExpressionController.VRMExpression`
2. Implement blend shape mapping in `SetExpression()`
3. Add color mapping in `GetExpressionColor()` for visual feedback

### Adding New Animations
1. Add animation enum to `VRMExpressionController.VRMAnimation`
2. Implement animation logic in `VRMAnimationPlayer.Update()`
3. Create animation method (e.g., `PlayNewAnimation()`)

### Adding New VRM Models
1. Place VRM file in project
2. Create `VRMModelData` entry in VRMModelManager
3. Set model path and metadata
4. Models can now be switched via UI

## Troubleshooting

**Issue**: Camera not working
- Check camera permissions
- Verify webcam is connected and not in use
- Check Unity console for initialization errors

**Issue**: AI not responding
- Verify API key is set correctly
- Check internet connection
- Verify API endpoint URL
- Check Unity console for error messages

**Issue**: Expressions not showing
- Placeholder models show expressions via color tints
- For full expressions, integrate UniVRM and use real VRM models

**Issue**: Models not switching
- Verify model paths are correct
- Check that VRM files exist at specified paths
- Ensure VRMModelManager is properly configured

## License

This project is open source. Please ensure you comply with:
- Unity's licensing terms
- OpenAI's usage policies
- VRM model licensing (if using 3rd party models)
- Any API service terms of use

## Contributing

Contributions are welcome! Areas for improvement:
- Full UniVRM integration
- More animation types
- Voice synthesis integration
- Mobile platform support
- VR/AR mode support