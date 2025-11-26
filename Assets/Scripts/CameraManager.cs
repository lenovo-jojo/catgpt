using System;
using System.Collections;
using UnityEngine;

namespace CatGPT
{
    /// <summary>
    /// Manages camera access and captures images for AI analysis
    /// </summary>
    public class CameraManager : MonoBehaviour
    {
        private WebCamTexture webCamTexture;
        [SerializeField] private int requestedWidth = 640;
        [SerializeField] private int requestedHeight = 480;
        [SerializeField] private int requestedFPS = 30;
        [SerializeField] [Range(1, 100)] private int jpegQuality = 75; // Quality for JPEG encoding (75 is a good balance)
        
        public bool IsInitialized { get; private set; }
        
        private void Start()
        {
            InitializeCamera();
        }
        
        /// <summary>
        /// Initialize the camera
        /// </summary>
        public void InitializeCamera()
        {
            if (webCamTexture != null)
            {
                webCamTexture.Stop();
            }
            
            if (WebCamTexture.devices.Length > 0)
            {
                webCamTexture = new WebCamTexture(WebCamTexture.devices[0].name, requestedWidth, requestedHeight, requestedFPS);
                webCamTexture.Play();
                IsInitialized = true;
                Debug.Log("Camera initialized: " + WebCamTexture.devices[0].name);
            }
            else
            {
                Debug.LogWarning("No camera devices found");
                IsInitialized = false;
            }
        }
        
        /// <summary>
        /// Capture the current camera frame as a Texture2D
        /// </summary>
        public Texture2D CaptureFrame()
        {
            if (webCamTexture == null || !webCamTexture.isPlaying)
            {
                Debug.LogWarning("Camera is not initialized or playing");
                return null;
            }
            
            Texture2D snapshot = new Texture2D(webCamTexture.width, webCamTexture.height);
            snapshot.SetPixels(webCamTexture.GetPixels());
            snapshot.Apply();
            
            return snapshot;
        }
        
        /// <summary>
        /// Capture camera frame and encode to base64
        /// </summary>
        public string CaptureFrameBase64()
        {
            Texture2D frame = CaptureFrame();
            if (frame == null) return null;
            
            byte[] imageBytes = frame.EncodeToJPG(jpegQuality);
            Destroy(frame);
            
            return Convert.ToBase64String(imageBytes);
        }
        
        /// <summary>
        /// Get the current camera texture for display
        /// </summary>
        public WebCamTexture GetCameraTexture()
        {
            return webCamTexture;
        }
        
        private void OnDestroy()
        {
            if (webCamTexture != null)
            {
                webCamTexture.Stop();
            }
        }
    }
}
