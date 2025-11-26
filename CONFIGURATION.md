# CatGPT Configuration Guide

This guide explains how to configure and set up the CatGPT application.

## Quick Start

1. **Get API Keys**
   - OpenAI API: https://platform.openai.com/api-keys
   - Weather API: https://openweathermap.org/api

2. **Create AI Config**
   - Right-click in Unity Project window
   - Create > CatGPT > AI Configuration
   - Name it "AIConfig"

3. **Configure Settings**
   - Fill in your API keys
   - Set your preferred city
   - Customize system prompt

## Detailed Configuration

### AI Settings

```
API Key: sk-your-openai-key-here
API Endpoint: https://api.openai.com/v1/chat/completions
Model: gpt-3.5-turbo (or gpt-4)
```

**Supported Models:**
- gpt-3.5-turbo (fastest, cheapest)
- gpt-4 (most capable)
- gpt-4-turbo (balanced)
- Any OpenAI-compatible API endpoint

### Weather Settings

```
Weather API Key: your-openweathermap-key
Weather API Endpoint: https://api.openweathermap.org/data/2.5/weather
City: Tokyo (or your city)
```

**Supported Cities:**
- Use city name: "Tokyo", "New York", "London"
- Use city ID: "1850144"
- Use coordinates: "lat=35.6762&lon=139.6503"

### RSS Feed Settings

```
RSS Feed URL: https://news.google.com/rss
```

**Popular RSS Feeds:**
- Google News: https://news.google.com/rss
- BBC News: http://feeds.bbci.co.uk/news/rss.xml
- TechCrunch: https://techcrunch.com/feed/
- Hacker News: https://news.ycombinator.com/rss

### System Prompt

Customize how the AI behaves:

```
You are a helpful AI assistant that can see through a camera, 
read news, check weather, and take screenshots. Respond in a 
friendly and conversational manner.
```

**Example Prompts:**
- Friendly assistant: "You are a cheerful AI companion..."
- Professional assistant: "You are a professional AI assistant..."
- Character roleplay: "You are a curious cat character who loves to chat..."

## VRM Model Configuration

### Adding Models

1. **Prepare VRM Files**
   - Place in StreamingAssets folder
   - Or use absolute paths

2. **Configure in VRMModelManager**
   ```csharp
   Model Name: "Character 1"
   Model Path: "Assets/StreamingAssets/character1.vrm"
   Description: "Default character"
   ```

3. **Multiple Models**
   - Add multiple VRMModelData entries
   - Switch between them using Next/Previous buttons

### Model Requirements

- VRM 0.0 or VRM 1.0 format
- Should include blend shapes for expressions
- Humanoid rig recommended
- Optimized for desktop performance

## Expression Configuration

### Expression Mapping

Expressions are triggered by keywords in AI responses:

| Expression | Keywords |
|-----------|----------|
| Happy | happy, joy, glad, wonderful |
| Sad | sad, sorry, unfortunate, disappointed |
| Angry | angry, mad, upset, frustrated |
| Surprised | surprise, wow, amazing, incredible |
| Fun | fun, enjoy, exciting, awesome |

### Custom Expression Triggers

To add custom triggers, edit `VRMExpressionController.cs`:

```csharp
public void TriggerExpressionFromKeyword(string keyword)
{
    keyword = keyword.ToLower();
    
    if (keyword.Contains("your_keyword"))
        SetExpression(VRMExpression.YourExpression);
}
```

## Animation Configuration

### Animation Settings

- Duration: 2 seconds (default)
- Speed: Adjustable per animation
- Looping: Disabled by default

### Animation Types

Each animation has different parameters:

**Wave Animation:**
- Rotation speed: 8 Hz
- Rotation angle: 15 degrees

**Nod Animation:**
- Rotation speed: 6 Hz
- Rotation angle: 10 degrees

**Dance Animation:**
- Rotation speed: 180 deg/sec
- Vertical movement: 0.3 units

### Custom Animations

Add new animations in `VRMAnimationPlayer.cs`:

```csharp
private void PlayYourAnimation()
{
    // Your animation code here
}
```

## UI Configuration

### Layout Recommendations

**Chat Area:**
- Input field at bottom
- Output text area with scroll
- Send button next to input

**Feature Buttons:**
- Camera button with preview
- Screen capture button
- Weather button
- News button

**VRM Controls:**
- Expression dropdown (top)
- Animation dropdown (below)
- Model switcher buttons (bottom)
- Current model display

### UI Scaling

For different resolutions:
- Use Canvas Scaler with "Scale with Screen Size"
- Reference resolution: 1920x1080
- Match: 0.5 (width/height blend)

## Performance Optimization

### Camera Settings

```
Resolution: 640x480 (balance quality/performance)
FPS: 30 (smooth preview)
Format: JPEG (compressed)
Quality: 75 (good quality, small size)
```

### API Call Optimization

- Cache weather data for 10 minutes
- Cache RSS feed for 5 minutes
- Limit conversation history to last 10 messages
- Compress images before sending to AI

### Model Performance

- Use LOD (Level of Detail) for VRM models
- Limit polygon count to <20K for mobile
- Use texture atlasing
- Enable GPU skinning

## Security Best Practices

### API Key Security

⚠️ **Never commit API keys to version control!**

**Secure Storage:**
1. Use Unity's PlayerPrefs (encrypted)
2. Store in external config file (gitignored)
3. Use environment variables
4. Use secure key management service

**Example .gitignore entries:**
```
*.apikey
config.json
.env
```

### Data Privacy

- Don't send sensitive screen content to AI
- Ask user permission for camera access
- Inform users about data being sent to APIs
- Comply with GDPR/privacy regulations

## Platform-Specific Settings

### Windows

- Camera: DirectShow
- Screen Capture: Full support
- VRM: Full support

### macOS

- Camera: AVFoundation (requires camera permission)
- Screen Capture: Requires screen recording permission
- VRM: Full support

### Linux

- Camera: V4L2 (requires camera access)
- Screen Capture: X11 or Wayland
- VRM: Full support

## Debugging

### Enable Debug Logging

In Unity:
1. Player Settings > Other Settings
2. Enable "Development Build"
3. Check console for detailed logs

### Common Issues

**"No camera devices found"**
- Check camera is connected
- Grant camera permissions
- Restart Unity

**"API key is missing"**
- Verify AI Config asset is assigned
- Check API key is filled in
- Ensure no extra spaces in key

**"Failed to parse AI response"**
- Check API endpoint is correct
- Verify API key is valid
- Check internet connection

## Advanced Configuration

### Custom AI Endpoints

To use other AI services:

```csharp
// In AIConfig
API Endpoint: https://your-custom-api.com/v1/chat
```

Ensure the API uses OpenAI-compatible format.

### Multi-Language Support

Configure system prompt for different languages:

```
Japanese: "あなたは親切なAIアシスタントです..."
Spanish: "Eres un asistente de IA útil..."
French: "Vous êtes un assistant IA utile..."
```

### Custom RSS Parsers

For non-standard RSS feeds, modify `RSSFeedManager.cs`:

```csharp
private RSSItem[] ParseRSS(string xmlContent)
{
    // Custom parsing logic
}
```

## Testing Configuration

### Test Mode

Enable test mode for development:

```csharp
// In CatGPTController.cs
private bool testMode = true;
```

This:
- Uses mock responses
- Skips API calls
- Faster testing

### Mock Data

Create mock responses for testing:

```csharp
string mockWeather = "Temperature: 20°C, Clear sky";
string mockNews = "1. Test News Headline\n2. Another Test";
```

## Deployment

### Build Settings

1. File > Build Settings
2. Platform: PC, Mac & Linux Standalone
3. Architecture: x86_64
4. Configuration: Release

### Distribution Checklist

- [ ] Remove test API keys
- [ ] Include setup instructions
- [ ] Add example config file
- [ ] Include required DLLs
- [ ] Test on clean machine

### Package Contents

```
CatGPT/
├── CatGPT.exe (or .app)
├── Data/
├── README.txt
├── CONFIG_TEMPLATE.json
└── LICENSE.txt
```

## Support

For issues and questions:
- Check documentation first
- Review Unity console logs
- Check API service status
- Report bugs with logs and reproduction steps