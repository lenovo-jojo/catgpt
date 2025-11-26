using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CatGPT
{
    /// <summary>
    /// Main controller that coordinates all systems
    /// </summary>
    public class CatGPTController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AIConfig aiConfig;
        [SerializeField] private AIChatManager chatManager;
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private ScreenCaptureManager screenCaptureManager;
        [SerializeField] private RSSFeedManager rssFeedManager;
        [SerializeField] private WeatherManager weatherManager;
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private VRMLoader vrmLoader;
        [SerializeField] private VRMModelManager modelManager;
        [SerializeField] private VRMExpressionController expressionController;
        
        [Header("UI References")]
        [SerializeField] private TMP_InputField chatInputField;
        [SerializeField] private TMP_Text chatOutputText;
        [SerializeField] private Button sendButton;
        [SerializeField] private Button cameraButton;
        [SerializeField] private Button screenCaptureButton;
        [SerializeField] private Button weatherButton;
        [SerializeField] private Button newsButton;
        [SerializeField] private RawImage cameraPreview;
        [SerializeField] private ScrollRect chatScrollRect;
        
        [Header("VRM Control UI")]
        [SerializeField] private Button nextModelButton;
        [SerializeField] private Button previousModelButton;
        [SerializeField] private TMP_Dropdown expressionDropdown;
        [SerializeField] private TMP_Dropdown animationDropdown;
        [SerializeField] private TMP_Text currentModelText;
        
        private string chatLog = "";
        
        private void Start()
        {
            InitializeComponents();
            SetupUI();
        }
        
        private void InitializeComponents()
        {
            // Initialize all managers if not assigned
            if (chatManager == null) chatManager = gameObject.AddComponent<AIChatManager>();
            if (cameraManager == null) cameraManager = gameObject.AddComponent<CameraManager>();
            if (screenCaptureManager == null) screenCaptureManager = gameObject.AddComponent<ScreenCaptureManager>();
            if (rssFeedManager == null) rssFeedManager = gameObject.AddComponent<RSSFeedManager>();
            if (weatherManager == null) weatherManager = gameObject.AddComponent<WeatherManager>();
            if (timeManager == null) timeManager = gameObject.AddComponent<TimeManager>();
            if (vrmLoader == null) vrmLoader = gameObject.AddComponent<VRMLoader>();
            if (modelManager == null) modelManager = gameObject.AddComponent<VRMModelManager>();
            if (expressionController == null) expressionController = gameObject.AddComponent<VRMExpressionController>();
            
            // Assign config to chat manager if available
            if (aiConfig != null && chatManager != null)
            {
                // Note: In production, add a SetConfig method to AIChatManager
                // For now, config is accessed via the inspector-assigned field
            }
        }
        
        private void SetupUI()
        {
            if (sendButton != null)
                sendButton.onClick.AddListener(OnSendMessage);
            
            if (cameraButton != null)
                cameraButton.onClick.AddListener(OnCameraCapture);
            
            if (screenCaptureButton != null)
                screenCaptureButton.onClick.AddListener(OnScreenCapture);
            
            if (weatherButton != null)
                weatherButton.onClick.AddListener(OnWeatherRequest);
            
            if (newsButton != null)
                newsButton.onClick.AddListener(OnNewsRequest);
            
            // VRM Controls
            if (nextModelButton != null)
                nextModelButton.onClick.AddListener(OnNextModel);
            
            if (previousModelButton != null)
                previousModelButton.onClick.AddListener(OnPreviousModel);
            
            if (expressionDropdown != null)
            {
                expressionDropdown.ClearOptions();
                expressionDropdown.AddOptions(new System.Collections.Generic.List<string>(
                    System.Enum.GetNames(typeof(VRMExpressionController.VRMExpression))));
                expressionDropdown.onValueChanged.AddListener(OnExpressionChanged);
            }
            
            if (animationDropdown != null)
            {
                animationDropdown.ClearOptions();
                animationDropdown.AddOptions(new System.Collections.Generic.List<string>(
                    System.Enum.GetNames(typeof(VRMExpressionController.VRMAnimation))));
                animationDropdown.onValueChanged.AddListener(OnAnimationChanged);
            }
            
            // Update camera preview
            if (cameraPreview != null && cameraManager != null)
            {
                StartCoroutine(UpdateCameraPreview());
            }
            
            UpdateModelUI();
        }
        
        private IEnumerator UpdateCameraPreview()
        {
            while (true)
            {
                if (cameraManager.IsInitialized)
                {
                    cameraPreview.texture = cameraManager.GetCameraTexture();
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        private void OnSendMessage()
        {
            if (chatInputField == null || string.IsNullOrWhiteSpace(chatInputField.text))
                return;
            
            string userMessage = chatInputField.text;
            chatInputField.text = "";
            
            AddToChatLog($"You: {userMessage}");
            
            // Add context information
            string contextualMessage = BuildContextualMessage(userMessage);
            
            StartCoroutine(chatManager.SendMessage(
                contextualMessage,
                OnAIResponse,
                OnAIError
            ));
        }
        
        private string BuildContextualMessage(string userMessage)
        {
            string context = userMessage + "\n\n[Context Information]\n";
            context += timeManager.GetCurrentTimeForAI() + "\n";
            return context;
        }
        
        private void OnCameraCapture()
        {
            if (cameraManager == null || !cameraManager.IsInitialized)
            {
                AddToChatLog("System: Camera not available");
                return;
            }
            
            AddToChatLog("System: Capturing camera image...");
            
            string imageBase64 = cameraManager.CaptureFrameBase64();
            if (!string.IsNullOrEmpty(imageBase64))
            {
                string message = "I'm sending you an image from the camera. Please describe what you see in the image. [Camera image captured]";
                StartCoroutine(chatManager.SendMessage(message, OnAIResponse, OnAIError));
            }
        }
        
        private void OnScreenCapture()
        {
            if (screenCaptureManager == null)
            {
                AddToChatLog("System: Screen capture not available");
                return;
            }
            
            AddToChatLog("System: Capturing screen...");
            
            string screenBase64 = screenCaptureManager.CaptureScreenBase64();
            if (!string.IsNullOrEmpty(screenBase64))
            {
                string message = "I'm sending you a screenshot of my screen. Please describe what you see. [Screenshot captured]";
                StartCoroutine(chatManager.SendMessage(message, OnAIResponse, OnAIError));
            }
        }
        
        private void OnWeatherRequest()
        {
            if (weatherManager == null || aiConfig == null)
            {
                AddToChatLog("System: Weather service not available");
                return;
            }
            
            AddToChatLog("System: Fetching weather information...");
            
            StartCoroutine(weatherManager.FetchWeather(
                aiConfig.weatherApiKey,
                aiConfig.city,
                (weatherData) =>
                {
                    string weatherInfo = weatherManager.FormatWeatherForAI(weatherData);
                    string message = $"Here's the current weather information:\n{weatherInfo}\n\nCan you tell me about the weather?";
                    StartCoroutine(chatManager.SendMessage(message, OnAIResponse, OnAIError));
                },
                (error) =>
                {
                    AddToChatLog($"System: {error}");
                }
            ));
        }
        
        private void OnNewsRequest()
        {
            if (rssFeedManager == null || aiConfig == null)
            {
                AddToChatLog("System: News service not available");
                return;
            }
            
            AddToChatLog("System: Fetching latest news...");
            
            StartCoroutine(rssFeedManager.FetchRSSFeed(
                aiConfig.rssFeedUrl,
                (items) =>
                {
                    string newsInfo = rssFeedManager.FormatRSSForAI(items);
                    string message = $"Here are the latest news headlines:\n{newsInfo}\n\nCan you summarize the top news stories?";
                    StartCoroutine(chatManager.SendMessage(message, OnAIResponse, OnAIError));
                },
                (error) =>
                {
                    AddToChatLog($"System: {error}");
                }
            ));
        }
        
        private void OnAIResponse(string response)
        {
            AddToChatLog($"AI: {response}");
            
            // Trigger expression based on AI response
            if (expressionController != null)
            {
                expressionController.ProcessAIResponseForExpression(response);
            }
        }
        
        private void OnAIError(string error)
        {
            AddToChatLog($"Error: {error}");
        }
        
        private void AddToChatLog(string message)
        {
            chatLog += message + "\n\n";
            
            if (chatOutputText != null)
            {
                chatOutputText.text = chatLog;
                
                // Auto-scroll to bottom
                if (chatScrollRect != null)
                {
                    Canvas.ForceUpdateCanvases();
                    chatScrollRect.verticalNormalizedPosition = 0f;
                }
            }
            
            Debug.Log(message);
        }
        
        // VRM Control Methods
        private void OnNextModel()
        {
            if (modelManager != null)
            {
                modelManager.NextModel();
                UpdateModelUI();
                AddToChatLog("System: Switched to next model");
            }
        }
        
        private void OnPreviousModel()
        {
            if (modelManager != null)
            {
                modelManager.PreviousModel();
                UpdateModelUI();
                AddToChatLog("System: Switched to previous model");
            }
        }
        
        private void OnExpressionChanged(int index)
        {
            if (expressionController != null)
            {
                VRMExpressionController.VRMExpression expression = 
                    (VRMExpressionController.VRMExpression)index;
                expressionController.SetExpression(expression);
                AddToChatLog($"System: Expression changed to {expression}");
            }
        }
        
        private void OnAnimationChanged(int index)
        {
            if (expressionController != null)
            {
                VRMExpressionController.VRMAnimation animation = 
                    (VRMExpressionController.VRMAnimation)index;
                expressionController.PlayAnimation(animation);
                AddToChatLog($"System: Playing animation {animation}");
            }
        }
        
        private void UpdateModelUI()
        {
            if (currentModelText != null && modelManager != null)
            {
                var modelData = modelManager.GetCurrentModelData();
                if (modelData != null)
                {
                    currentModelText.text = $"Model: {modelData.modelName}";
                }
                else
                {
                    currentModelText.text = "Model: Default";
                }
            }
        }
    }
}
