# CatGPT Setup Guide

Follow these steps to set up and run the CatGPT application.

## Prerequisites

- Unity 2022.3.10f1 or later installed
- A code editor (Visual Studio, VS Code, or Rider)
- Internet connection for API access

## Step 1: API Keys Setup

### 1.1 Get OpenAI API Key

1. Visit https://platform.openai.com/api-keys
2. Sign up or log in
3. Click "Create new secret key"
4. Copy and save your key (starts with `sk-`)

### 1.2 Get Weather API Key

1. Visit https://openweathermap.org/api
2. Sign up for a free account
3. Navigate to API keys section
4. Copy your API key

## Step 2: Unity Project Setup

### 2.1 Open Project in Unity

1. Launch Unity Hub
2. Click "Open" or "Add"
3. Navigate to the `catgpt` folder
4. Click "Open"
5. Wait for Unity to import assets (first time may take a few minutes)

### 2.2 Create AI Configuration Asset

1. In Unity Project window, navigate to `Assets/Resources`
2. Right-click in the folder
3. Select `Create > CatGPT > AI Configuration`
4. Name it `AIConfig`

### 2.3 Configure API Keys

1. Select the `AIConfig` asset
2. In the Inspector, fill in:
   - **API Key**: Your OpenAI key (sk-...)
   - **Weather API Key**: Your OpenWeatherMap key
   - **City**: Your city name (e.g., "Tokyo", "New York")
   - **RSS Feed URL**: Keep default or use your preferred feed

3. Customize the **System Prompt** if desired

## Step 3: Scene Setup

### 3.1 Create Main Scene

1. File > New Scene
2. Save as `Assets/Scenes/MainScene.unity`

### 3.2 Add CatGPT Manager

1. GameObject > Create Empty
2. Rename to "CatGPT Manager"
3. Add Component > CatGPT Controller
4. In Inspector, assign the `AIConfig` asset to "AI Config" field

### 3.3 Create UI Canvas

1. GameObject > UI > Canvas
2. Set Canvas Scaler to "Scale With Screen Size"
3. Reference Resolution: 1920 x 1080

### 3.4 Add Chat UI

**Chat Output Area:**
1. Right-click Canvas > UI > Scroll View
2. Rename to "Chat Scroll View"
3. Position at top portion of screen
4. Add TextMeshPro Text to Content
5. Rename to "Chat Output Text"
6. Set alignment and font size

**Chat Input Area:**
1. Right-click Canvas > UI > Input Field (TMP)
2. Rename to "Chat Input Field"
3. Position at bottom of screen

**Send Button:**
1. Right-click Canvas > UI > Button (TMP)
2. Rename to "Send Button"
3. Position next to input field
4. Set button text to "Send"

### 3.5 Add Feature Buttons

Create buttons for:
- Camera Button (text: "Camera")
- Screen Capture Button (text: "Screenshot")
- Weather Button (text: "Weather")
- News Button (text: "News")

Position them in a row below the chat input.

### 3.6 Add VRM Control UI

**Expression Control:**
1. UI > Dropdown (TMP) - rename to "Expression Dropdown"
2. Position at top right

**Animation Control:**
1. UI > Dropdown (TMP) - rename to "Animation Dropdown"
2. Position below expression dropdown

**Model Switcher:**
1. UI > Button (TMP) - rename to "Previous Model" (text: "< Prev")
2. UI > Button (TMP) - rename to "Next Model" (text: "Next >")
3. UI > Text (TMP) - rename to "Current Model Text"
4. Arrange horizontally at bottom

### 3.7 Add Camera Preview

1. Right-click Canvas > UI > Raw Image
2. Rename to "Camera Preview"
3. Position at desired location (e.g., top left)
4. Set size to 320x240 or preferred size

### 3.8 Link UI to Controller

1. Select "CatGPT Manager"
2. In Inspector, drag UI elements to corresponding fields:
   - Chat Input Field → Chat Input Field
   - Chat Output Text → Chat Output Text
   - Send Button → Send Button
   - Camera Button → Camera Button
   - Screen Capture Button → Screen Capture Button
   - Weather Button → Weather Button
   - News Button → News Button
   - Camera Preview → Camera Preview
   - Chat Scroll View → Chat Scroll Rect
   - Expression Dropdown → Expression Dropdown
   - Animation Dropdown → Animation Dropdown
   - Next Model Button → Next Model Button
   - Previous Model Button → Previous Model Button
   - Current Model Text → Current Model Text

## Step 4: Add 3D Scene Elements

### 4.1 Add Camera

1. GameObject > Camera (if not already present)
2. Position: (0, 1.5, -5)
3. Rotation: (0, 0, 0)

### 4.2 Add Lighting

1. GameObject > Light > Directional Light
2. Adjust intensity and color as desired

### 4.3 Add VRM Spawn Point

1. GameObject > Create Empty
2. Rename to "VRM Spawn Point"
3. Position: (0, 0, 0)
4. Drag to "VRM Spawn Point" field in VRMLoader component

## Step 5: Testing

### 5.1 Test in Editor

1. Click Play button
2. Wait for initialization
3. Type a message in chat input
4. Click Send
5. Verify AI responds

### 5.2 Test Camera

1. Click Camera button
2. Allow camera permissions if prompted
3. Verify preview shows webcam feed
4. AI should describe what it sees

### 5.3 Test Other Features

- Click Weather button → Should fetch and display weather
- Click News button → Should fetch and display news
- Use Expression dropdown → Character should change expression
- Use Animation dropdown → Character should play animation

### 5.4 Test Model Switching

1. Click Next/Previous Model buttons
2. Character should change (if models configured)
3. Current model name should update

## Step 6: Build (Optional)

### 6.1 Configure Build Settings

1. File > Build Settings
2. Click "Add Open Scenes"
3. Select platform (PC, Mac & Linux Standalone)
4. Click "Player Settings"
5. Set Company Name, Product Name
6. Configure other settings as needed

### 6.2 Build Project

1. Click "Build"
2. Choose output folder
3. Wait for build to complete
4. Test the built executable

## Troubleshooting

### Camera Not Working

**Problem**: "No camera devices found"
**Solutions**:
- Check webcam is connected
- Grant camera permissions to Unity
- Restart Unity Editor
- Check Windows/macOS privacy settings

### API Errors

**Problem**: "API key is missing" or "API Error"
**Solutions**:
- Verify API keys are correct (no extra spaces)
- Check AIConfig asset is assigned to controller
- Verify internet connection
- Check API service status

### UI Not Responding

**Problem**: Buttons don't work
**Solutions**:
- Verify EventSystem exists in scene
- Check buttons have OnClick events assigned
- Verify controller script is active
- Check console for errors

### Character Not Showing

**Problem**: No character visible
**Solutions**:
- Check VRM Spawn Point is set
- Verify camera can see spawn point
- Check lighting in scene
- Look for errors in console

### Build Errors

**Problem**: Build fails
**Solutions**:
- Check for script errors (they must all be resolved)
- Verify all scenes are added to build
- Check platform compatibility
- Review build logs for specific errors

## Advanced Setup

### Adding Real VRM Models

1. Download VRM files from:
   - VRoid Hub: https://hub.vroid.com/
   - Booth: https://booth.pm/
   - Other VRM resources

2. Place VRM files in `Assets/StreamingAssets/`

3. In Unity, select CatGPT Manager

4. Find VRM Model Manager component

5. Add model entries:
   - Model Name: "Character Name"
   - Model Path: "Assets/StreamingAssets/model.vrm"
   - Description: "Description"

6. Test by clicking Next/Previous Model buttons

### Customizing Expressions

Edit `VRMExpressionController.cs`:

1. Add new expression to enum
2. Add keyword triggers
3. Add color mapping (for placeholder)
4. For real VRM, map to blend shapes

### Adding Custom Animations

Edit `VRMAnimationPlayer.cs`:

1. Add new animation to enum
2. Create PlayYourAnimation() method
3. Add to switch statement in Update()
4. Configure animation parameters

## Next Steps

- Customize UI appearance
- Add more expressions and animations
- Integrate UniVRM for full VRM support
- Add voice synthesis
- Implement emotion analysis
- Create custom system prompts
- Add more data sources

## Support Resources

- Unity Documentation: https://docs.unity3d.com/
- OpenAI API Docs: https://platform.openai.com/docs
- VRM Specification: https://vrm.dev/
- Project README: See README.md
- Configuration Guide: See CONFIGURATION.md

## Tips for Best Experience

1. **Use good API prompts**: Customize system prompt for better responses
2. **Test incrementally**: Test each feature as you add it
3. **Monitor console**: Watch for errors and warnings
4. **Save often**: Save scene and project frequently
5. **Backup API keys**: Store keys securely outside project
6. **Optimize performance**: Reduce camera resolution if laggy
7. **Read documentation**: Refer to docs when stuck

Enjoy using CatGPT! 🐱