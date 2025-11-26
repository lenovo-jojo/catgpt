using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace CatGPT
{
    /// <summary>
    /// Configuration for the AI Chat system
    /// </summary>
    [CreateAssetMenu(fileName = "AIConfig", menuName = "CatGPT/AI Configuration")]
    public class AIConfig : ScriptableObject
    {
        [Header("API Settings")]
        public string apiKey = "";
        public string apiEndpoint = "https://api.openai.com/v1/chat/completions";
        public string model = "gpt-3.5-turbo";
        
        [Header("Weather API")]
        public string weatherApiKey = "";
        public string weatherApiEndpoint = "https://api.openweathermap.org/data/2.5/weather";
        public string city = "Tokyo";
        
        [Header("RSS Feed")]
        public string rssFeedUrl = "https://news.google.com/rss";
        
        [Header("System Prompts")]
        [TextArea(5, 15)]
        public string systemPrompt = "You are a helpful AI assistant that can see through a camera, read news, check weather, and take screenshots. Respond in a friendly and conversational manner.";
    }
}
