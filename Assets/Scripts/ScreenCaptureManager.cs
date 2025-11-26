using System;
using System.Collections;
using UnityEngine;

namespace CatGPT
{
    /// <summary>
    /// Captures screenshots of the desktop screen
    /// </summary>
    public class ScreenCaptureManager : MonoBehaviour
    {
        /// <summary>
        /// Capture the current Unity screen
        /// </summary>
        public Texture2D CaptureScreen()
        {
            int width = Screen.width;
            int height = Screen.height;
            
            Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenshot.Apply();
            
            return screenshot;
        }
        
        /// <summary>
        /// Capture screen and encode to base64
        /// </summary>
        public string CaptureScreenBase64()
        {
            Texture2D screenshot = CaptureScreen();
            if (screenshot == null) return null;
            
            byte[] imageBytes = screenshot.EncodeToJPG(75);
            Destroy(screenshot);
            
            return Convert.ToBase64String(imageBytes);
        }
        
        /// <summary>
        /// Save screenshot to file
        /// </summary>
        public void SaveScreenshot(string filename = null)
        {
            if (string.IsNullOrEmpty(filename))
            {
                filename = $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            }
            
            ScreenCapture.CaptureScreenshot(filename);
            Debug.Log($"Screenshot saved: {filename}");
        }
    }
}
