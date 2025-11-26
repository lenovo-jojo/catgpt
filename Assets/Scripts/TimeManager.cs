using System;
using UnityEngine;

namespace CatGPT
{
    /// <summary>
    /// Manages time and date information
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        /// <summary>
        /// Get current time and date formatted for AI
        /// </summary>
        public string GetCurrentTimeForAI()
        {
            DateTime now = DateTime.Now;
            return $"Current time: {now:yyyy-MM-dd HH:mm:ss} ({now.DayOfWeek})";
        }
        
        /// <summary>
        /// Get current date
        /// </summary>
        public string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        
        /// <summary>
        /// Get current time
        /// </summary>
        public string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        
        /// <summary>
        /// Get day of week
        /// </summary>
        public string GetDayOfWeek()
        {
            return DateTime.Now.DayOfWeek.ToString();
        }
    }
}
