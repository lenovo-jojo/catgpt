using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CatGPT
{
    /// <summary>
    /// Manages multiple VRM models and switching between them
    /// </summary>
    public class VRMModelManager : MonoBehaviour
    {
        [Header("Model Settings")]
        [SerializeField] private List<VRMModelData> availableModels = new List<VRMModelData>();
        [SerializeField] private Transform modelSpawnPoint;
        [SerializeField] private int currentModelIndex = 0;
        
        private GameObject currentModelInstance;
        private VRMExpressionController expressionController;
        
        [Serializable]
        public class VRMModelData
        {
            public string modelName;
            public string modelPath;
            public Sprite thumbnail;
            public string description;
        }
        
        // Events
        public event Action<VRMModelData> OnModelChanged;
        
        private void Start()
        {
            expressionController = GetComponent<VRMExpressionController>();
            if (expressionController == null)
            {
                expressionController = gameObject.AddComponent<VRMExpressionController>();
            }
            
            // Load first model if available
            if (availableModels.Count > 0)
            {
                LoadModel(0);
            }
            else
            {
                LoadPlaceholderModel();
            }
        }
        
        /// <summary>
        /// Load a VRM model by index
        /// </summary>
        public void LoadModel(int index)
        {
            if (index < 0 || index >= availableModels.Count)
            {
                Debug.LogError($"Invalid model index: {index}");
                return;
            }
            
            currentModelIndex = index;
            VRMModelData modelData = availableModels[index];
            
            LoadModelFromPath(modelData.modelPath, modelData.modelName);
            
            OnModelChanged?.Invoke(modelData);
        }
        
        /// <summary>
        /// Load a VRM model from file path
        /// </summary>
        public void LoadModelFromPath(string filePath, string modelName = "VRM Model")
        {
            // Clear existing model
            if (currentModelInstance != null)
            {
                Destroy(currentModelInstance);
            }
            
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                Debug.Log($"Loading VRM model from: {filePath}");
                // In real implementation with UniVRM:
                // byte[] bytes = File.ReadAllBytes(filePath);
                // currentModelInstance = await LoadVRMAsync(bytes);
                
                // For now, create placeholder
                CreatePlaceholderModel(modelName);
            }
            else
            {
                CreatePlaceholderModel(modelName);
            }
            
            // Set instance in expression controller
            if (expressionController != null && currentModelInstance != null)
            {
                expressionController.SetVRMInstance(currentModelInstance);
            }
        }
        
        /// <summary>
        /// Create a placeholder model
        /// </summary>
        private void CreatePlaceholderModel(string modelName)
        {
            currentModelInstance = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            currentModelInstance.name = modelName;
            
            Vector3 spawnPosition = modelSpawnPoint != null ? modelSpawnPoint.position : Vector3.zero;
            Quaternion spawnRotation = modelSpawnPoint != null ? modelSpawnPoint.rotation : Quaternion.identity;
            
            currentModelInstance.transform.position = spawnPosition;
            currentModelInstance.transform.rotation = spawnRotation;
            currentModelInstance.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
            
            // Add animator component
            VRMAnimator animator = currentModelInstance.AddComponent<VRMAnimator>();
            
            Debug.Log($"Placeholder model created: {modelName}");
        }
        
        /// <summary>
        /// Load a placeholder model with default settings
        /// </summary>
        private void LoadPlaceholderModel()
        {
            CreatePlaceholderModel("Default VRM");
            
            if (expressionController != null && currentModelInstance != null)
            {
                expressionController.SetVRMInstance(currentModelInstance);
            }
        }
        
        /// <summary>
        /// Switch to next model
        /// </summary>
        public void NextModel()
        {
            if (availableModels.Count == 0) return;
            
            currentModelIndex = (currentModelIndex + 1) % availableModels.Count;
            LoadModel(currentModelIndex);
        }
        
        /// <summary>
        /// Switch to previous model
        /// </summary>
        public void PreviousModel()
        {
            if (availableModels.Count == 0) return;
            
            currentModelIndex--;
            if (currentModelIndex < 0)
                currentModelIndex = availableModels.Count - 1;
            
            LoadModel(currentModelIndex);
        }
        
        /// <summary>
        /// Add a new model to the available models list
        /// </summary>
        public void AddModel(VRMModelData modelData)
        {
            availableModels.Add(modelData);
            Debug.Log($"Added model: {modelData.modelName}");
        }
        
        /// <summary>
        /// Remove a model from the available models list
        /// </summary>
        public void RemoveModel(int index)
        {
            if (index >= 0 && index < availableModels.Count)
            {
                availableModels.RemoveAt(index);
                
                // If current model was removed, load another
                if (index == currentModelIndex)
                {
                    if (availableModels.Count > 0)
                        LoadModel(0);
                    else
                        LoadPlaceholderModel();
                }
            }
        }
        
        /// <summary>
        /// Get current model instance
        /// </summary>
        public GameObject GetCurrentModelInstance()
        {
            return currentModelInstance;
        }
        
        /// <summary>
        /// Get current model data
        /// </summary>
        public VRMModelData GetCurrentModelData()
        {
            if (currentModelIndex >= 0 && currentModelIndex < availableModels.Count)
                return availableModels[currentModelIndex];
            
            return null;
        }
        
        /// <summary>
        /// Get list of all available models
        /// </summary>
        public List<VRMModelData> GetAvailableModels()
        {
            return new List<VRMModelData>(availableModels);
        }
        
        /// <summary>
        /// Get current model index
        /// </summary>
        public int GetCurrentModelIndex()
        {
            return currentModelIndex;
        }
    }
}
