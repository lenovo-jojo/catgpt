using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatGPT
{
    /// <summary>
    /// Manages VRM character expressions and animations
    /// </summary>
    public class VRMExpressionController : MonoBehaviour
    {
        [Header("Expression Settings")]
        [SerializeField] private VRMExpression currentExpression = VRMExpression.Neutral;
        [SerializeField] private float expressionTransitionSpeed = 2f;
        
        [Header("Animation Settings")]
        [SerializeField] private VRMAnimation currentAnimation = VRMAnimation.Idle;
        
        // Available expressions (VRM BlendShapes)
        public enum VRMExpression
        {
            Neutral,
            Happy,
            Sad,
            Angry,
            Surprised,
            Relaxed,
            Joy,
            Fun,
            Sorrow,
            Blink,
            BlinkLeft,
            BlinkRight
        }
        
        // Available animations
        public enum VRMAnimation
        {
            Idle,
            Wave,
            Nod,
            Shake,
            ThumbsUp,
            Think,
            Talk,
            Dance,
            Celebrate
        }
        
        private Dictionary<VRMExpression, float> expressionWeights = new Dictionary<VRMExpression, float>();
        private Animator animator;
        private GameObject vrmInstance;
        
        // Events
        public event Action<VRMExpression> OnExpressionChanged;
        public event Action<VRMAnimation> OnAnimationChanged;
        
        private void Awake()
        {
            InitializeExpressionWeights();
        }
        
        private void InitializeExpressionWeights()
        {
            foreach (VRMExpression expr in Enum.GetValues(typeof(VRMExpression)))
            {
                expressionWeights[expr] = expr == VRMExpression.Neutral ? 1f : 0f;
            }
        }
        
        /// <summary>
        /// Set the VRM instance to control
        /// </summary>
        public void SetVRMInstance(GameObject vrm)
        {
            vrmInstance = vrm;
            animator = vrm.GetComponent<Animator>();
            
            if (animator == null)
            {
                animator = vrm.AddComponent<Animator>();
            }
        }
        
        /// <summary>
        /// Set expression with optional intensity
        /// </summary>
        public void SetExpression(VRMExpression expression, float intensity = 1f)
        {
            if (currentExpression == expression) return;
            
            currentExpression = expression;
            OnExpressionChanged?.Invoke(expression);
            
            Debug.Log($"Setting expression: {expression} (intensity: {intensity})");
            
            // In a real VRM implementation, this would set blend shapes
            // For now, we'll just log and trigger visual feedback
            TriggerExpressionVisual(expression, intensity);
        }
        
        /// <summary>
        /// Play animation
        /// </summary>
        public void PlayAnimation(VRMAnimation animation)
        {
            if (currentAnimation == animation) return;
            
            currentAnimation = animation;
            OnAnimationChanged?.Invoke(animation);
            
            Debug.Log($"Playing animation: {animation}");
            
            // In a real implementation, this would trigger animator states
            if (animator != null)
            {
                // animator.SetTrigger(animation.ToString());
            }
            
            TriggerAnimationVisual(animation);
        }
        
        /// <summary>
        /// Trigger expression based on emotion keyword
        /// </summary>
        public void TriggerExpressionFromKeyword(string keyword)
        {
            keyword = keyword.ToLower();
            
            if (keyword.Contains("happy") || keyword.Contains("joy") || keyword.Contains("glad"))
                SetExpression(VRMExpression.Happy);
            else if (keyword.Contains("sad") || keyword.Contains("sorry") || keyword.Contains("unfortunate"))
                SetExpression(VRMExpression.Sad);
            else if (keyword.Contains("angry") || keyword.Contains("mad") || keyword.Contains("upset"))
                SetExpression(VRMExpression.Angry);
            else if (keyword.Contains("surprise") || keyword.Contains("wow") || keyword.Contains("amazing"))
                SetExpression(VRMExpression.Surprised);
            else if (keyword.Contains("fun") || keyword.Contains("enjoy") || keyword.Contains("exciting"))
                SetExpression(VRMExpression.Fun);
            else
                SetExpression(VRMExpression.Neutral);
        }
        
        /// <summary>
        /// Visual feedback for expressions (placeholder)
        /// </summary>
        private void TriggerExpressionVisual(VRMExpression expression, float intensity)
        {
            if (vrmInstance == null) return;
            
            // Change color tint based on expression (visual feedback)
            Renderer[] renderers = vrmInstance.GetComponentsInChildren<Renderer>();
            Color expressionColor = GetExpressionColor(expression);
            
            foreach (Renderer renderer in renderers)
            {
                if (renderer.material != null)
                {
                    renderer.material.color = Color.Lerp(Color.white, expressionColor, intensity * 0.3f);
                }
            }
        }
        
        /// <summary>
        /// Visual feedback for animations (placeholder)
        /// </summary>
        private void TriggerAnimationVisual(VRMAnimation animation)
        {
            if (vrmInstance == null) return;
            
            // Add simple motion based on animation type
            VRMAnimationPlayer player = vrmInstance.GetComponent<VRMAnimationPlayer>();
            if (player == null)
            {
                player = vrmInstance.AddComponent<VRMAnimationPlayer>();
            }
            
            player.PlayAnimation(animation);
        }
        
        /// <summary>
        /// Get color representation for expression
        /// </summary>
        private Color GetExpressionColor(VRMExpression expression)
        {
            switch (expression)
            {
                case VRMExpression.Happy:
                case VRMExpression.Joy:
                case VRMExpression.Fun:
                    return new Color(1f, 0.9f, 0.3f); // Yellow
                case VRMExpression.Sad:
                case VRMExpression.Sorrow:
                    return new Color(0.4f, 0.5f, 0.8f); // Blue
                case VRMExpression.Angry:
                    return new Color(1f, 0.3f, 0.3f); // Red
                case VRMExpression.Surprised:
                    return new Color(1f, 0.7f, 1f); // Pink
                case VRMExpression.Relaxed:
                    return new Color(0.6f, 0.9f, 0.6f); // Green
                default:
                    return Color.white;
            }
        }
        
        /// <summary>
        /// Get current expression
        /// </summary>
        public VRMExpression GetCurrentExpression()
        {
            return currentExpression;
        }
        
        /// <summary>
        /// Get current animation
        /// </summary>
        public VRMAnimation GetCurrentAnimation()
        {
            return currentAnimation;
        }
        
        /// <summary>
        /// Reset to neutral expression
        /// </summary>
        public void ResetExpression()
        {
            SetExpression(VRMExpression.Neutral);
        }
        
        /// <summary>
        /// Auto-detect and trigger expression from AI response
        /// </summary>
        public void ProcessAIResponseForExpression(string aiResponse)
        {
            TriggerExpressionFromKeyword(aiResponse);
        }
    }
    
    /// <summary>
    /// Plays simple animations for VRM placeholder
    /// </summary>
    public class VRMAnimationPlayer : MonoBehaviour
    {
        private VRMExpressionController.VRMAnimation currentAnimation;
        private float animationTime = 0f;
        private Vector3 originalPosition;
        private Quaternion originalRotation;
        private bool isPlaying = false;
        
        private void Start()
        {
            originalPosition = transform.localPosition;
            originalRotation = transform.localRotation;
        }
        
        public void PlayAnimation(VRMExpressionController.VRMAnimation animation)
        {
            currentAnimation = animation;
            animationTime = 0f;
            isPlaying = true;
        }
        
        private void Update()
        {
            if (!isPlaying) return;
            
            animationTime += Time.deltaTime;
            
            switch (currentAnimation)
            {
                case VRMExpressionController.VRMAnimation.Wave:
                    PlayWaveAnimation();
                    break;
                case VRMExpressionController.VRMAnimation.Nod:
                    PlayNodAnimation();
                    break;
                case VRMExpressionController.VRMAnimation.Shake:
                    PlayShakeAnimation();
                    break;
                case VRMExpressionController.VRMAnimation.Dance:
                    PlayDanceAnimation();
                    break;
                case VRMExpressionController.VRMAnimation.Celebrate:
                    PlayCelebrateAnimation();
                    break;
                default:
                    isPlaying = false;
                    break;
            }
            
            // Stop animation after duration
            if (animationTime > 2f)
            {
                isPlaying = false;
                transform.localPosition = originalPosition;
                transform.localRotation = originalRotation;
            }
        }
        
        private void PlayWaveAnimation()
        {
            float wave = Mathf.Sin(animationTime * 8f) * 15f;
            transform.localRotation = originalRotation * Quaternion.Euler(0, 0, wave);
        }
        
        private void PlayNodAnimation()
        {
            float nod = Mathf.Sin(animationTime * 6f) * 10f;
            transform.localRotation = originalRotation * Quaternion.Euler(nod, 0, 0);
        }
        
        private void PlayShakeAnimation()
        {
            float shake = Mathf.Sin(animationTime * 10f) * 15f;
            transform.localRotation = originalRotation * Quaternion.Euler(0, shake, 0);
        }
        
        private void PlayDanceAnimation()
        {
            float dance = Mathf.Sin(animationTime * 5f) * 0.3f;
            transform.localPosition = originalPosition + new Vector3(0, dance, 0);
            transform.localRotation = originalRotation * Quaternion.Euler(0, animationTime * 180f, 0);
        }
        
        private void PlayCelebrateAnimation()
        {
            float jump = Mathf.Abs(Mathf.Sin(animationTime * 8f)) * 0.5f;
            transform.localPosition = originalPosition + new Vector3(0, jump, 0);
            float spin = animationTime * 360f;
            transform.localRotation = originalRotation * Quaternion.Euler(0, spin, 0);
        }
    }
}
