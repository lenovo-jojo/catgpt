using UnityEngine;

namespace CatGPT
{
    /// <summary>
    /// Simple animator for VRM placeholder
    /// Provides basic idle animation with rotation and bobbing
    /// </summary>
    public class VRMAnimator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 30f;
        [SerializeField] private float bobSpeed = 2f;
        [SerializeField] private float bobAmount = 0.2f;
        
        private Vector3 startPosition;
        
        private void Start()
        {
            startPosition = transform.position;
        }
        
        private void Update()
        {
            // Gentle rotation
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            
            // Bobbing motion
            float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobAmount;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
    }
}
