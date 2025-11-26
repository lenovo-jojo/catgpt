# CatGPT Quick Reference

## Common Commands & Shortcuts

### Chat Commands

```
"What's the weather?"          → Triggers weather fetch
"Show me the news"             → Fetches RSS feed
"What do you see?"             → Captures camera image
"Take a screenshot"            → Captures screen
"What time is it?"             → Gets current time
```

### Expression Keywords

The AI automatically triggers expressions based on these keywords in its responses:

| Keyword | Expression |
|---------|-----------|
| happy, joy, glad | Happy |
| sad, sorry, unfortunate | Sad |
| angry, mad, upset | Angry |
| surprise, wow, amazing | Surprised |
| fun, enjoy, exciting | Fun |

### Manual Expression Control

Use the Expression dropdown to manually trigger:
- Neutral (default)
- Happy
- Sad
- Angry
- Surprised
- Relaxed
- Joy
- Fun
- Sorrow
- Blink

### Animation Control

Use the Animation dropdown to trigger:
- Idle (standing)
- Wave (greeting)
- Nod (yes)
- Shake (no)
- ThumbsUp (approval)
- Think (contemplating)
- Talk (speaking)
- Dance (dancing)
- Celebrate (victory)

## Keyboard Shortcuts (in Unity Editor)

```
F11          - Maximize game view
Ctrl+P       - Play/Stop game
Ctrl+Shift+P - Pause game
Ctrl+S       - Save scene
Ctrl+D       - Duplicate selected
Delete       - Delete selected
```

## API Response Format

### Chat Response
```json
{
  "role": "assistant",
  "content": "Response text here"
}
```

### Weather Response
```json
{
  "cityName": "Tokyo",
  "temperature": 20.5,
  "description": "clear sky",
  "humidity": 60,
  "windSpeed": 3.5
}
```

### RSS Item
```json
{
  "title": "News headline",
  "description": "Article summary",
  "link": "https://...",
  "pubDate": "Thu, 01 Jan 2024..."
}
```

## Common Issues & Quick Fixes

| Issue | Quick Fix |
|-------|-----------|
| Camera not working | Check permissions, restart Unity |
| API error | Verify API key, check internet |
| Character not visible | Check spawn point, camera position |
| UI not responding | Verify EventSystem exists |
| Build fails | Fix all script errors first |

## Performance Tips

### Optimize Camera
```csharp
Resolution: 640x480
FPS: 30
Quality: 75
```

### Optimize API Calls
- Cache weather for 10 min
- Cache news for 5 min
- Limit history to 10 messages

### Optimize Character
- Use LOD for models
- Limit polygon count < 20K
- Enable GPU skinning

## File Locations

```
Scripts:      Assets/Scripts/
Scenes:       Assets/Scenes/
Resources:    Assets/Resources/
VRM Models:   Assets/StreamingAssets/
Config:       Assets/Resources/AIConfig.asset
```

## Component References

### CatGPTController
Main controller that coordinates all systems.

**Key Methods:**
- `OnSendMessage()` - Send chat message
- `OnCameraCapture()` - Capture camera
- `OnScreenCapture()` - Capture screen
- `OnWeatherRequest()` - Fetch weather
- `OnNewsRequest()` - Fetch news

### VRMExpressionController
Manages character expressions and animations.

**Key Methods:**
- `SetExpression(expr)` - Set expression
- `PlayAnimation(anim)` - Play animation
- `TriggerExpressionFromKeyword(keyword)` - Auto-trigger

### VRMModelManager
Handles multiple models and switching.

**Key Methods:**
- `LoadModel(index)` - Load specific model
- `NextModel()` - Switch to next
- `PreviousModel()` - Switch to previous

### AIChatManager
Manages AI API communication.

**Key Methods:**
- `SendMessage(msg, onResponse, onError)` - Send to AI
- `ClearHistory()` - Reset conversation

### CameraManager
Handles webcam access.

**Key Methods:**
- `CaptureFrame()` - Get Texture2D
- `CaptureFrameBase64()` - Get base64 string

### WeatherManager
Fetches weather data.

**Key Methods:**
- `FetchWeather(apiKey, city, onSuccess, onError)`
- `FormatWeatherForAI(data)` - Format for display

### RSSFeedManager
Reads RSS feeds.

**Key Methods:**
- `FetchRSSFeed(url, onSuccess, onError)`
- `FormatRSSForAI(items)` - Format for display

## Configuration Parameters

### AI Config
```
API Key:            Your OpenAI key
API Endpoint:       https://api.openai.com/v1/chat/completions
Model:              gpt-3.5-turbo
Weather API Key:    Your weather key
City:               Your city
RSS Feed URL:       Your RSS feed
System Prompt:      AI behavior definition
```

### Camera Settings
```
Requested Width:    640
Requested Height:   480
Requested FPS:      30
```

### Expression Settings
```
Transition Speed:   2.0
Current Expression: Neutral
```

### Animation Settings
```
Animation Duration: 2.0 seconds
```

## Testing Checklist

- [ ] Chat sends and receives messages
- [ ] Camera preview shows webcam feed
- [ ] Camera capture button works
- [ ] Screen capture button works
- [ ] Weather button fetches data
- [ ] News button fetches RSS
- [ ] Expression dropdown changes character
- [ ] Animation dropdown triggers animations
- [ ] Next/Previous model buttons work
- [ ] Current model name displays
- [ ] Auto-expressions trigger from AI

## Development Workflow

1. **Edit Scripts** → Save
2. **Return to Unity** → Wait for compilation
3. **Press Play** → Test changes
4. **Check Console** → Fix any errors
5. **Stop Play** → Make more changes
6. **Repeat**

## Debugging Commands

```csharp
// In scripts, add debug logs:
Debug.Log("Message");
Debug.LogWarning("Warning");
Debug.LogError("Error");

// Check component:
Debug.Log(component != null);

// Check value:
Debug.Log($"Value: {variable}");
```

## Unity Console Symbols

```
(i)  Information
(!)  Warning
(x)  Error
```

## Build Process

1. File > Build Settings
2. Add Open Scenes
3. Select Platform
4. Player Settings (configure)
5. Build
6. Test executable

## Resource Links

- [Unity Manual](https://docs.unity3d.com/Manual/)
- [C# Reference](https://learn.microsoft.com/dotnet/csharp/)
- [OpenAI API](https://platform.openai.com/docs)
- [VRM Specification](https://vrm.dev/en/)
- [UniVRM](https://github.com/vrm-c/UniVRM)

## Version Info

```
Unity Version:  2022.3.10f1
C# Version:     8.0
.NET:           Standard 2.1
Platform:       Desktop (Win/Mac/Linux)
```

## Support

For detailed information, refer to:
- README.md - Overview and features
- SETUP.md - Complete setup guide
- CONFIGURATION.md - Configuration details

---

**Quick Start**: Create AIConfig → Set API keys → Play scene → Start chatting! 🚀