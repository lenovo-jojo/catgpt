using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace CatGPT
{
    /// <summary>
    /// Handles AI chat integration with OpenAI-compatible APIs
    /// </summary>
    public class AIChatManager : MonoBehaviour
    {
        [SerializeField] private AIConfig config;
        
        private List<ChatMessage> conversationHistory = new List<ChatMessage>();
        
        [Serializable]
        public class ChatMessage
        {
            public string role;
            public string content;
            
            public ChatMessage(string role, string content)
            {
                this.role = role;
                this.content = content;
            }
        }
        
        [Serializable]
        private class ChatRequest
        {
            public string model;
            public List<ChatMessage> messages;
            public float temperature = 0.7f;
        }
        
        [Serializable]
        private class ChatResponse
        {
            public Choice[] choices;
            
            [Serializable]
            public class Choice
            {
                public ChatMessage message;
            }
        }
        
        private void Start()
        {
            if (config != null)
            {
                conversationHistory.Add(new ChatMessage("system", config.systemPrompt));
            }
            else
            {
                Debug.LogWarning("AIChatManager: AIConfig is not assigned. Please assign it in the Inspector.");
            }
        }
        
        /// <summary>
        /// Set the AI configuration (can be called programmatically)
        /// </summary>
        public void SetConfig(AIConfig newConfig)
        {
            config = newConfig;
            if (config != null && conversationHistory.Count == 0)
            {
                conversationHistory.Add(new ChatMessage("system", config.systemPrompt));
            }
        }
        
        /// <summary>
        /// Send a message to the AI and get a response
        /// </summary>
        public IEnumerator SendMessage(string userMessage, Action<string> onResponse, Action<string> onError)
        {
            if (config == null || string.IsNullOrEmpty(config.apiKey))
            {
                onError?.Invoke("AI configuration or API key is missing");
                yield break;
            }
            
            conversationHistory.Add(new ChatMessage("user", userMessage));
            
            ChatRequest request = new ChatRequest
            {
                model = config.model,
                messages = conversationHistory
            };
            
            string jsonData = JsonUtility.ToJson(request);
            // Manual JSON construction for array support (Unity's JsonUtility doesn't support arrays at root level)
            // TODO: Consider using a proper JSON library like Newtonsoft.Json for production
            jsonData = "{\"model\":\"" + config.model + "\",\"messages\":[";
            for (int i = 0; i < conversationHistory.Count; i++)
            {
                if (i > 0) jsonData += ",";
                jsonData += "{\"role\":\"" + conversationHistory[i].role + "\",\"content\":\"" + EscapeJson(conversationHistory[i].content) + "\"}";
            }
            jsonData += "],\"temperature\":0.7}";
            
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            
            UnityWebRequest www = new UnityWebRequest(config.apiEndpoint, "POST");
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + config.apiKey);
            
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                onError?.Invoke("AI API Error: " + www.error);
            }
            else
            {
                try
                {
                    ChatResponse response = JsonUtility.FromJson<ChatResponse>(www.downloadHandler.text);
                    if (response.choices != null && response.choices.Length > 0)
                    {
                        string aiResponse = response.choices[0].message.content;
                        conversationHistory.Add(new ChatMessage("assistant", aiResponse));
                        onResponse?.Invoke(aiResponse);
                    }
                    else
                    {
                        onError?.Invoke("Empty response from AI");
                    }
                }
                catch (Exception e)
                {
                    onError?.Invoke("Failed to parse AI response: " + e.Message);
                }
            }
        }
        
        private string EscapeJson(string text)
        {
            return text.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
        }
        
        /// <summary>
        /// Clear conversation history
        /// </summary>
        public void ClearHistory()
        {
            conversationHistory.Clear();
            if (config != null)
            {
                conversationHistory.Add(new ChatMessage("system", config.systemPrompt));
            }
        }
    }
}
