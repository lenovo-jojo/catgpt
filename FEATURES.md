# CatGPT Features Documentation

Complete feature reference for the CatGPT Unity application.

## Table of Contents
1. [Core Features](#core-features)
2. [VRM Character System](#vrm-character-system)
3. [AI Integration](#ai-integration)
4. [Camera System](#camera-system)
5. [Data Sources](#data-sources)
6. [User Interface](#user-interface)

---

## Core Features

### 1. AI Chat System

**Description**: Real-time conversational AI using OpenAI-compatible APIs.

**Features**:
- Natural language processing
- Context-aware responses
- Conversation history management
- Custom system prompts
- Error handling and retry logic

**Supported APIs**:
- OpenAI (GPT-3.5, GPT-4)
- Azure OpenAI
- Any OpenAI-compatible endpoint

**Configuration**:
```
API Key: Your API key
Endpoint: https://api.openai.com/v1/chat/completions
Model: gpt-3.5-turbo
```

**Usage**:
```csharp
chatManager.SendMessage("Hello!", OnResponse, OnError);
```

---

### 2. VRM Character System

**Description**: Load and animate VRM (Virtual Reality Model) characters.

**Supported Formats**:
- VRM 0.0
- VRM 1.0 (with UniVRM)

**Character Features**:
- Expression system (12 expressions)
- Animation system (9 animations)
- Model switching
- Auto-expression from AI
- Visual feedback (color tinting)

**Expressions Available**:
1. Neutral - Default state
2. Happy - Joyful expression
3. Sad - Sorrowful expression
4. Angry - Mad expression
5. Surprised - Shocked expression
6. Relaxed - Calm expression
7. Joy - Extreme happiness
8. Fun - Playful expression
9. Sorrow - Deep sadness
10. Blink - Eye blink
11. BlinkLeft - Left eye blink
12. BlinkRight - Right eye blink

**Animations Available**:
1. Idle - Standing still
2. Wave - Greeting gesture
3. Nod - Head nod (yes)
4. Shake - Head shake (no)
5. ThumbsUp - Approval
6. Think - Thinking pose
7. Talk - Speaking animation
8. Dance - Dancing motion
9. Celebrate - Victory pose

---

### 3. Camera Integration

**Description**: Access webcam for visual analysis.

**Capabilities**:
- Live camera feed preview
- Frame capture (Texture2D)
- Base64 encoding for AI
- Resolution control
- FPS control

**Default Settings**:
```
Resolution: 640x480
FPS: 30
Format: JPEG
Quality: 75%
```

**Use Cases**:
- Visual question answering
- Object detection
- Scene description
- Real-time monitoring

**API**:
```csharp
// Preview
Texture texture = cameraManager.GetCameraTexture();

// Capture
Texture2D frame = cameraManager.CaptureFrame();
string base64 = cameraManager.CaptureFrameBase64();
```

---

### 4. Screen Capture

**Description**: Capture screenshots for analysis.

**Features**:
- Full screen capture
- Unity window capture
- Base64 encoding
- Save to file
- Quality control

**Formats**:
- PNG (lossless)
- JPEG (compressed)

**API**:
```csharp
// Capture
Texture2D screenshot = captureManager.CaptureScreen();
string base64 = captureManager.CaptureScreenBase64();

// Save
captureManager.SaveScreenshot("screenshot.png");
```

---

### 5. Weather Integration

**Description**: Real-time weather information via OpenWeatherMap API.

**Data Retrieved**:
- Temperature (Celsius)
- Weather description
- Humidity percentage
- Wind speed (m/s)
- City name

**Configuration**:
```
API Key: OpenWeatherMap key
Endpoint: https://api.openweathermap.org/data/2.5/weather
City: Your city name
```

**Update Frequency**: Recommended 10-30 minutes

**API**:
```csharp
weatherManager.FetchWeather(apiKey, city, 
    (data) => {
        Debug.Log($"{data.temperature}°C");
    },
    (error) => Debug.LogError(error)
);
```

---

### 6. RSS Feed Reader

**Description**: Fetch and parse RSS news feeds.

**Supported Formats**:
- RSS 2.0
- Atom (basic)

**Data Extracted**:
- Article title
- Description
- Link
- Publication date

**Default Feeds**:
- Google News
- BBC News
- TechCrunch
- Custom feeds

**Item Limit**: 10 items per fetch (configurable)

**API**:
```csharp
rssFeedManager.FetchRSSFeed(feedUrl,
    (items) => {
        foreach (var item in items) {
            Debug.Log(item.title);
        }
    },
    (error) => Debug.LogError(error)
);
```

---

### 7. Time/Date System

**Description**: Access current time and date information.

**Features**:
- Current date
- Current time
- Day of week
- Formatted strings for AI

**Output Formats**:
- ISO 8601: "2024-01-01 12:30:45"
- Date only: "2024-01-01"
- Time only: "12:30:45"
- Day: "Monday"

**API**:
```csharp
string time = timeManager.GetCurrentTimeForAI();
// Output: "Current time: 2024-01-01 12:30:45 (Monday)"
```

---

## VRM Character System

### Expression System

**How It Works**:
1. User or AI triggers expression
2. Expression controller updates character
3. Visual feedback (color tint on placeholder)
4. Blend shape animation (with real VRM)

**Triggering Methods**:

**1. Manual Trigger**:
```csharp
expressionController.SetExpression(
    VRMExpression.Happy, 
    intensity: 1.0f
);
```

**2. Keyword Trigger**:
```csharp
expressionController.TriggerExpressionFromKeyword("I'm so happy!");
```

**3. Auto Trigger from AI**:
```csharp
expressionController.ProcessAIResponseForExpression(aiResponse);
```

**Expression Mapping**:
| Keyword | Expression |
|---------|-----------|
| happy, joy, glad, wonderful | Happy |
| sad, sorry, unfortunate | Sad |
| angry, mad, upset, frustrated | Angry |
| surprise, wow, amazing | Surprised |
| fun, enjoy, exciting | Fun |

**Visual Feedback Colors**:
- Happy: Yellow tint
- Sad: Blue tint
- Angry: Red tint
- Surprised: Pink tint
- Relaxed: Green tint
- Neutral: White (no tint)

---

### Animation System

**How It Works**:
1. Animation requested
2. VRMAnimationPlayer starts
3. 2-second animation plays
4. Returns to idle state

**Animation Parameters**:

**Wave**:
- Duration: 2 seconds
- Rotation: ±15 degrees
- Frequency: 8 Hz

**Nod**:
- Duration: 2 seconds
- Rotation: ±10 degrees
- Frequency: 6 Hz

**Dance**:
- Duration: 2 seconds
- Rotation: 180°/sec
- Vertical movement: ±0.3 units

**Celebrate**:
- Duration: 2 seconds
- Jump height: 0.5 units
- Rotation: 360°/sec

**Triggering**:
```csharp
expressionController.PlayAnimation(VRMAnimation.Wave);
```

---

### Model Management

**Features**:
- Load multiple VRM models
- Switch between models
- Model metadata storage
- Thumbnail support
- Model descriptions

**Model Data Structure**:
```csharp
VRMModelData {
    string modelName;
    string modelPath;
    Sprite thumbnail;
    string description;
}
```

**Operations**:

**Add Model**:
```csharp
var model = new VRMModelData {
    modelName = "Character",
    modelPath = "path/to/model.vrm",
    description = "Description"
};
modelManager.AddModel(model);
```

**Switch Models**:
```csharp
modelManager.NextModel();
modelManager.PreviousModel();
modelManager.LoadModel(index);
```

**Get Info**:
```csharp
var current = modelManager.GetCurrentModelData();
var all = modelManager.GetAvailableModels();
int index = modelManager.GetCurrentModelIndex();
```

---

## AI Integration

### Conversation Flow

1. **User Input** → Text message
2. **Context Building** → Add time, weather, etc.
3. **API Request** → Send to AI
4. **Response Processing** → Parse JSON
5. **Expression Trigger** → Auto-set expression
6. **Display** → Show in UI

### Context Information

Messages can include:
- Current time/date
- Weather data
- News headlines
- Camera images (base64)
- Screenshots (base64)

### Conversation History

- Stored in memory
- Includes system, user, and assistant messages
- Limited to prevent token overflow
- Can be cleared manually

### Error Handling

**Common Errors**:
- Invalid API key
- Network timeout
- Rate limit exceeded
- Invalid JSON response
- Empty response

**Retry Strategy**:
```csharp
// Exponential backoff
int attempts = 0;
float delay = 1f;
while (attempts < maxAttempts) {
    // Try request
    if (success) break;
    
    yield return new WaitForSeconds(delay);
    delay *= 2;
    attempts++;
}
```

---

## Camera System

### Initialization

**Auto-initialization**:
- On Start()
- Finds first available camera
- Sets resolution and FPS

**Manual initialization**:
```csharp
cameraManager.InitializeCamera();
```

### Camera Preview

**Real-time preview**:
- Updates 10 times per second
- Displays in RawImage UI
- Low overhead

**Implementation**:
```csharp
IEnumerator UpdatePreview() {
    while (true) {
        if (cameraManager.IsInitialized) {
            rawImage.texture = cameraManager.GetCameraTexture();
        }
        yield return new WaitForSeconds(0.1f);
    }
}
```

### Frame Capture

**Capture Methods**:

**1. Texture2D**:
```csharp
Texture2D frame = cameraManager.CaptureFrame();
// Use frame for processing
Destroy(frame); // Clean up when done
```

**2. Base64 String**:
```csharp
string base64 = cameraManager.CaptureFrameBase64();
// Send to API
```

### Performance

**Optimization Tips**:
- Lower resolution for faster processing
- Reduce FPS for lower CPU usage
- Use JPEG compression for smaller data
- Capture only when needed

---

## Data Sources

### Weather API

**Provider**: OpenWeatherMap

**Endpoints**:
- Current weather: `/data/2.5/weather`
- Forecast: `/data/2.5/forecast` (not implemented)

**Rate Limits**:
- Free tier: 60 calls/minute
- 1,000,000 calls/month

**Caching**:
- Recommended: 10-30 minutes
- Reduces API calls
- Improves response time

### RSS Feeds

**Supported Sources**:
- News sites (BBC, CNN, etc.)
- Blogs
- Podcasts
- Custom feeds

**Parsing**:
- XML parsing via System.Xml
- Extracts: title, description, link, date
- Limits to 10 items by default

**Update Frequency**:
- News: 5-15 minutes
- Blogs: 30-60 minutes
- Podcasts: 1-24 hours

---

## User Interface

### Layout Components

**Chat Area**:
- Input field (bottom)
- Output text (scrollable)
- Send button
- Auto-scroll to bottom

**Feature Buttons**:
- Camera capture
- Screen capture
- Weather fetch
- News fetch

**VRM Controls**:
- Expression dropdown
- Animation dropdown
- Model switcher (prev/next)
- Current model display

**Camera Preview**:
- Live feed display
- Positioned top-left
- 320x240 default size

### UI Best Practices

**Responsive Design**:
- Use Canvas Scaler
- Scale with screen size
- Reference: 1920x1080

**Accessibility**:
- Clear button labels
- Keyboard shortcuts
- Color contrast
- Font size options

**User Feedback**:
- Loading indicators
- Success messages
- Error notifications
- Status displays

---

## Technical Specifications

### Performance

**Target Framerate**: 60 FPS
**Camera FPS**: 30 FPS
**UI Update**: 10 Hz

### Memory Usage

**Typical**: 200-500 MB
**With camera**: +50-100 MB
**Per VRM model**: +10-50 MB

### Platform Support

**Desktop**:
- ✅ Windows 10/11
- ✅ macOS 10.15+
- ✅ Linux (Ubuntu 20.04+)

**Mobile** (not currently supported):
- ❌ iOS
- ❌ Android

### API Requirements

**Required**:
- OpenAI API key (or compatible)

**Optional**:
- Weather API key
- RSS feed URL (public)

---

## Future Enhancements

### Planned Features

1. **Voice Integration**
   - Speech recognition
   - Text-to-speech
   - Voice cloning

2. **Advanced VRM**
   - Full blend shape support
   - IK (Inverse Kinematics)
   - Physics simulation

3. **Mobile Support**
   - iOS build
   - Android build
   - Touch controls

4. **VR/AR Mode**
   - VR headset support
   - Hand tracking
   - Spatial audio

5. **Multiplayer**
   - Multiple characters
   - Shared conversations
   - Real-time sync

6. **Plugin System**
   - Custom data sources
   - Custom expressions
   - Custom animations

---

## Troubleshooting

### Common Issues

**Camera Issues**:
- No devices found → Check connections
- Black screen → Check permissions
- Low FPS → Reduce resolution

**AI Issues**:
- No response → Check API key
- Slow response → Check internet
- Error 429 → Rate limit hit

**Character Issues**:
- Not visible → Check spawn point
- No animation → Check components
- No expressions → Check controller

**UI Issues**:
- Buttons not working → Check EventSystem
- Text not updating → Check references
- Layout broken → Check Canvas Scaler

---

## Performance Optimization

### Camera Optimization
```csharp
Resolution: 640x480 (instead of 1920x1080)
FPS: 15-30 (instead of 60)
Quality: 60-75 (instead of 100)
```

### AI Optimization
```csharp
Max tokens: 150-300 (instead of 1000+)
Temperature: 0.7 (balanced)
History limit: 10 messages
Cache responses when possible
```

### Character Optimization
```csharp
Polygon count: <20,000
Texture size: 1024x1024 (instead of 4096)
Use LOD system
GPU skinning: enabled
```

---

For more information, see:
- [README.md](README.md) - Overview
- [SETUP.md](SETUP.md) - Setup guide
- [CONFIGURATION.md](CONFIGURATION.md) - Configuration
- [API_EXAMPLES.md](API_EXAMPLES.md) - Code examples