# API Usage Examples

This document provides examples of how to use the CatGPT APIs programmatically.

## Basic Chat Usage

```csharp
using CatGPT;
using UnityEngine;

public class ChatExample : MonoBehaviour
{
    [SerializeField] private AIChatManager chatManager;
    
    void Start()
    {
        // Send a message
        StartCoroutine(chatManager.SendMessage(
            "Hello, how are you?",
            OnSuccess,
            OnError
        ));
    }
    
    private void OnSuccess(string response)
    {
        Debug.Log($"AI Response: {response}");
    }
    
    private void OnError(string error)
    {
        Debug.LogError($"Error: {error}");
    }
}
```

## Camera Capture Example

```csharp
using CatGPT;
using UnityEngine;

public class CameraExample : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    
    void Start()
    {
        if (cameraManager.IsInitialized)
        {
            CaptureAndAnalyze();
        }
    }
    
    private void CaptureAndAnalyze()
    {
        // Capture as Texture2D
        Texture2D frame = cameraManager.CaptureFrame();
        
        // Or capture as base64
        string base64 = cameraManager.CaptureFrameBase64();
        
        Debug.Log($"Captured frame: {frame.width}x{frame.height}");
    }
}
```

## Weather API Example

```csharp
using CatGPT;
using UnityEngine;

public class WeatherExample : MonoBehaviour
{
    [SerializeField] private WeatherManager weatherManager;
    [SerializeField] private string apiKey = "your-api-key";
    [SerializeField] private string city = "Tokyo";
    
    void Start()
    {
        FetchWeather();
    }
    
    private void FetchWeather()
    {
        StartCoroutine(weatherManager.FetchWeather(
            apiKey,
            city,
            OnWeatherSuccess,
            OnWeatherError
        ));
    }
    
    private void OnWeatherSuccess(WeatherManager.WeatherData data)
    {
        Debug.Log($"Temperature: {data.temperature}°C");
        Debug.Log($"Description: {data.description}");
        Debug.Log($"Humidity: {data.humidity}%");
        Debug.Log($"Wind: {data.windSpeed} m/s");
    }
    
    private void OnWeatherError(string error)
    {
        Debug.LogError($"Weather error: {error}");
    }
}
```

## RSS Feed Example

```csharp
using CatGPT;
using UnityEngine;

public class RSSExample : MonoBehaviour
{
    [SerializeField] private RSSFeedManager rssManager;
    [SerializeField] private string feedUrl = "https://news.google.com/rss";
    
    void Start()
    {
        FetchNews();
    }
    
    private void FetchNews()
    {
        StartCoroutine(rssManager.FetchRSSFeed(
            feedUrl,
            OnRSSSuccess,
            OnRSSError
        ));
    }
    
    private void OnRSSSuccess(RSSFeedManager.RSSItem[] items)
    {
        Debug.Log($"Fetched {items.Length} news items");
        
        foreach (var item in items)
        {
            Debug.Log($"Title: {item.title}");
            Debug.Log($"Link: {item.link}");
        }
    }
    
    private void OnRSSError(string error)
    {
        Debug.LogError($"RSS error: {error}");
    }
}
```

## Expression Control Example

```csharp
using CatGPT;
using UnityEngine;

public class ExpressionExample : MonoBehaviour
{
    [SerializeField] private VRMExpressionController expressionController;
    
    void Start()
    {
        // Set expressions
        SetExpression();
        
        // Play animations
        PlayAnimation();
    }
    
    private void SetExpression()
    {
        // Set to happy expression
        expressionController.SetExpression(
            VRMExpressionController.VRMExpression.Happy,
            1.0f // intensity
        );
        
        // Set from keyword
        expressionController.TriggerExpressionFromKeyword("I'm so happy!");
    }
    
    private void PlayAnimation()
    {
        // Play wave animation
        expressionController.PlayAnimation(
            VRMExpressionController.VRMAnimation.Wave
        );
    }
    
    private void ListenToEvents()
    {
        // Subscribe to events
        expressionController.OnExpressionChanged += (expr) => {
            Debug.Log($"Expression changed to: {expr}");
        };
        
        expressionController.OnAnimationChanged += (anim) => {
            Debug.Log($"Animation changed to: {anim}");
        };
    }
}
```

## Model Management Example

```csharp
using CatGPT;
using UnityEngine;

public class ModelExample : MonoBehaviour
{
    [SerializeField] private VRMModelManager modelManager;
    
    void Start()
    {
        // Switch models
        SwitchModels();
        
        // Add new model
        AddNewModel();
        
        // Listen to events
        ListenToModelChanges();
    }
    
    private void SwitchModels()
    {
        // Next model
        modelManager.NextModel();
        
        // Previous model
        modelManager.PreviousModel();
        
        // Load specific model
        modelManager.LoadModel(0);
    }
    
    private void AddNewModel()
    {
        var newModel = new VRMModelManager.VRMModelData
        {
            modelName = "New Character",
            modelPath = "Assets/StreamingAssets/character.vrm",
            description = "A new character model"
        };
        
        modelManager.AddModel(newModel);
    }
    
    private void ListenToModelChanges()
    {
        modelManager.OnModelChanged += (modelData) => {
            Debug.Log($"Model changed to: {modelData.modelName}");
        };
    }
    
    private void GetModelInfo()
    {
        // Get current model
        var currentModel = modelManager.GetCurrentModelData();
        Debug.Log($"Current model: {currentModel?.modelName}");
        
        // Get all available models
        var allModels = modelManager.GetAvailableModels();
        Debug.Log($"Total models: {allModels.Count}");
    }
}
```

## Screen Capture Example

```csharp
using CatGPT;
using UnityEngine;

public class ScreenCaptureExample : MonoBehaviour
{
    [SerializeField] private ScreenCaptureManager captureManager;
    
    void Start()
    {
        CaptureScreen();
    }
    
    private void CaptureScreen()
    {
        // Capture as Texture2D
        Texture2D screenshot = captureManager.CaptureScreen();
        
        // Or capture as base64
        string base64 = captureManager.CaptureScreenBase64();
        
        // Or save to file
        captureManager.SaveScreenshot("my_screenshot.png");
        
        Debug.Log($"Screenshot captured: {screenshot.width}x{screenshot.height}");
    }
}
```

## Time/Date Example

```csharp
using CatGPT;
using UnityEngine;

public class TimeExample : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    
    void Start()
    {
        GetTimeInfo();
    }
    
    private void GetTimeInfo()
    {
        // Get formatted time for AI
        string timeForAI = timeManager.GetCurrentTimeForAI();
        Debug.Log(timeForAI);
        // Output: "Current time: 2024-01-01 12:30:45 (Monday)"
        
        // Get individual components
        string date = timeManager.GetCurrentDate();
        string time = timeManager.GetCurrentTime();
        string day = timeManager.GetDayOfWeek();
        
        Debug.Log($"Date: {date}");
        Debug.Log($"Time: {time}");
        Debug.Log($"Day: {day}");
    }
}
```

## Complete Integration Example

```csharp
using CatGPT;
using UnityEngine;
using System.Collections;

public class CompleteExample : MonoBehaviour
{
    [SerializeField] private AIConfig config;
    [SerializeField] private AIChatManager chatManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private WeatherManager weatherManager;
    [SerializeField] private VRMExpressionController expressionController;
    
    void Start()
    {
        StartCoroutine(RunCompleteExample());
    }
    
    private IEnumerator RunCompleteExample()
    {
        // 1. Set initial expression
        expressionController.SetExpression(VRMExpressionController.VRMExpression.Happy);
        
        // 2. Fetch weather
        bool weatherDone = false;
        string weatherInfo = "";
        
        yield return weatherManager.FetchWeather(
            config.weatherApiKey,
            config.city,
            (data) => {
                weatherInfo = weatherManager.FormatWeatherForAI(data);
                weatherDone = true;
            },
            (error) => {
                Debug.LogError(error);
                weatherDone = true;
            }
        );
        
        while (!weatherDone) yield return null;
        
        // 3. Capture camera
        string cameraBase64 = "";
        if (cameraManager.IsInitialized)
        {
            cameraBase64 = cameraManager.CaptureFrameBase64();
        }
        
        // 4. Build contextual message
        string message = $"Hello! {weatherInfo}. ";
        if (!string.IsNullOrEmpty(cameraBase64))
        {
            message += "I've also captured an image from the camera.";
        }
        
        // 5. Send to AI
        bool aiDone = false;
        
        yield return chatManager.SendMessage(
            message,
            (response) => {
                Debug.Log($"AI: {response}");
                
                // 6. Trigger expression based on response
                expressionController.ProcessAIResponseForExpression(response);
                
                aiDone = true;
            },
            (error) => {
                Debug.LogError(error);
                aiDone = true;
            }
        );
        
        while (!aiDone) yield return null;
        
        // 7. Play celebration animation
        expressionController.PlayAnimation(VRMExpressionController.VRMAnimation.Celebrate);
        
        Debug.Log("Complete example finished!");
    }
}
```

## Error Handling Best Practices

```csharp
using CatGPT;
using UnityEngine;
using System;

public class ErrorHandlingExample : MonoBehaviour
{
    [SerializeField] private AIChatManager chatManager;
    
    void Start()
    {
        SendMessageWithRetry("Hello!", maxRetries: 3);
    }
    
    private void SendMessageWithRetry(string message, int maxRetries = 3)
    {
        StartCoroutine(SendWithRetryCoroutine(message, maxRetries));
    }
    
    private System.Collections.IEnumerator SendWithRetryCoroutine(string message, int maxRetries)
    {
        int attempts = 0;
        bool success = false;
        
        while (attempts < maxRetries && !success)
        {
            attempts++;
            Debug.Log($"Attempt {attempts}/{maxRetries}");
            
            bool done = false;
            
            yield return chatManager.SendMessage(
                message,
                (response) => {
                    Debug.Log($"Success: {response}");
                    success = true;
                    done = true;
                },
                (error) => {
                    Debug.LogWarning($"Attempt {attempts} failed: {error}");
                    done = true;
                }
            );
            
            while (!done) yield return null;
            
            if (!success && attempts < maxRetries)
            {
                Debug.Log("Retrying in 2 seconds...");
                yield return new WaitForSeconds(2f);
            }
        }
        
        if (!success)
        {
            Debug.LogError($"Failed after {maxRetries} attempts");
        }
    }
}
```

## Performance Optimization Example

```csharp
using CatGPT;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptimizationExample : MonoBehaviour
{
    // Cache system
    private Dictionary<string, CachedData> cache = new Dictionary<string, CachedData>();
    
    private class CachedData
    {
        public string data;
        public float timestamp;
    }
    
    [SerializeField] private WeatherManager weatherManager;
    [SerializeField] private float cacheTimeout = 600f; // 10 minutes
    
    public IEnumerator GetWeatherCached(string city, System.Action<string> callback)
    {
        string cacheKey = $"weather_{city}";
        
        // Check cache
        if (cache.ContainsKey(cacheKey))
        {
            var cached = cache[cacheKey];
            if (Time.time - cached.timestamp < cacheTimeout)
            {
                Debug.Log("Using cached weather data");
                callback?.Invoke(cached.data);
                yield break;
            }
        }
        
        // Fetch fresh data
        bool done = false;
        string result = "";
        
        yield return weatherManager.FetchWeather(
            "your-api-key",
            city,
            (data) => {
                result = weatherManager.FormatWeatherForAI(data);
                
                // Cache the result
                cache[cacheKey] = new CachedData {
                    data = result,
                    timestamp = Time.time
                };
                
                done = true;
            },
            (error) => {
                Debug.LogError(error);
                done = true;
            }
        );
        
        while (!done) yield return null;
        
        callback?.Invoke(result);
    }
}
```

## Custom System Prompt Example

```csharp
using CatGPT;
using UnityEngine;

public class CustomPromptExample : MonoBehaviour
{
    void Start()
    {
        // Create custom prompts for different personalities
        string friendlyPrompt = @"You are a cheerful AI cat companion who loves to chat! 
            You respond with enthusiasm and often use cat-related expressions. 
            You can see through a camera, check weather, read news, and take screenshots.
            Always be positive and encouraging!";
        
        string professionalPrompt = @"You are a professional AI assistant with access to 
            camera vision, weather data, news feeds, and screen capture capabilities. 
            Provide clear, concise, and helpful responses. Maintain a professional tone 
            while being friendly and approachable.";
        
        string characterPrompt = @"You are a curious anime character exploring the digital world! 
            You have magical powers that let you see through cameras, predict weather, 
            read mystical news scrolls, and capture screen memories. 
            Respond in a playful, anime-style manner with occasional emoticons! ^_^";
        
        // Apply to AI Config
        // config.systemPrompt = friendlyPrompt;
    }
}
```

## Tips and Best Practices

### 1. Always Check Initialization
```csharp
if (cameraManager != null && cameraManager.IsInitialized)
{
    // Use camera
}
```

### 2. Use Coroutines for API Calls
```csharp
StartCoroutine(chatManager.SendMessage(...));
```

### 3. Handle Errors Gracefully
```csharp
private void OnError(string error)
{
    // Log error
    Debug.LogError(error);
    
    // Show user-friendly message
    ShowMessage("Something went wrong. Please try again.");
    
    // Optional: Retry logic
}
```

### 4. Dispose Resources
```csharp
private void OnDestroy()
{
    // Clean up camera
    if (cameraManager != null)
    {
        // Camera will auto-cleanup
    }
}
```

### 5. Optimize API Calls
- Cache data when possible
- Limit conversation history
- Compress images before sending
- Use appropriate image quality settings

### 6. Secure API Keys
- Never hardcode API keys
- Use ScriptableObjects for configuration
- Add API key files to .gitignore
- Consider environment variables for builds