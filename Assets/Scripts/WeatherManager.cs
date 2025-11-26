using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace CatGPT
{
    /// <summary>
    /// Fetches weather information from OpenWeatherMap API
    /// </summary>
    public class WeatherManager : MonoBehaviour
    {
        [Serializable]
        public class WeatherData
        {
            public string cityName;
            public float temperature;
            public string description;
            public int humidity;
            public float windSpeed;
        }
        
        [Serializable]
        private class WeatherResponse
        {
            public string name;
            public Main main;
            public Weather[] weather;
            public Wind wind;
            
            [Serializable]
            public class Main
            {
                public float temp;
                public int humidity;
            }
            
            [Serializable]
            public class Weather
            {
                public string description;
            }
            
            [Serializable]
            public class Wind
            {
                public float speed;
            }
        }
        
        /// <summary>
        /// Fetch current weather data
        /// </summary>
        public IEnumerator FetchWeather(string apiKey, string city, Action<WeatherData> onSuccess, Action<string> onError)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                onError?.Invoke("Weather API key is missing");
                yield break;
            }
            
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                onError?.Invoke("Weather API Error: " + www.error);
                yield break;
            }
            
            try
            {
                WeatherResponse response = JsonUtility.FromJson<WeatherResponse>(www.downloadHandler.text);
                
                WeatherData data = new WeatherData
                {
                    cityName = response.name,
                    temperature = response.main.temp,
                    humidity = response.main.humidity,
                    windSpeed = response.wind.speed,
                    description = response.weather.Length > 0 ? response.weather[0].description : "N/A"
                };
                
                onSuccess?.Invoke(data);
            }
            catch (Exception e)
            {
                onError?.Invoke("Weather Parse Error: " + e.Message);
            }
        }
        
        /// <summary>
        /// Format weather data for AI
        /// </summary>
        public string FormatWeatherForAI(WeatherData data)
        {
            if (data == null)
                return "Weather information unavailable.";
            
            return $"Current weather in {data.cityName}: {data.temperature}°C, {data.description}, " +
                   $"Humidity: {data.humidity}%, Wind: {data.windSpeed} m/s";
        }
    }
}
