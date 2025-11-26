using System;
using System.Collections;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

namespace CatGPT
{
    /// <summary>
    /// Fetches and parses RSS feeds
    /// </summary>
    public class RSSFeedManager : MonoBehaviour
    {
        [Serializable]
        public class RSSItem
        {
            public string title;
            public string description;
            public string link;
            public string pubDate;
        }
        
        /// <summary>
        /// Fetch RSS feed and parse items
        /// </summary>
        public IEnumerator FetchRSSFeed(string feedUrl, Action<RSSItem[]> onSuccess, Action<string> onError)
        {
            UnityWebRequest www = UnityWebRequest.Get(feedUrl);
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                onError?.Invoke("RSS Feed Error: " + www.error);
                yield break;
            }
            
            try
            {
                RSSItem[] items = ParseRSS(www.downloadHandler.text);
                onSuccess?.Invoke(items);
            }
            catch (Exception e)
            {
                onError?.Invoke("RSS Parse Error: " + e.Message);
            }
        }
        
        /// <summary>
        /// Parse RSS XML string
        /// </summary>
        private RSSItem[] ParseRSS(string xmlContent)
        {
            System.Collections.Generic.List<RSSItem> items = new System.Collections.Generic.List<RSSItem>();
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);
            
            XmlNodeList itemNodes = xmlDoc.GetElementsByTagName("item");
            
            int maxItems = Mathf.Min(itemNodes.Count, 10); // Limit to 10 items
            for (int i = 0; i < maxItems; i++)
            {
                XmlNode itemNode = itemNodes[i];
                RSSItem item = new RSSItem();
                
                foreach (XmlNode childNode in itemNode.ChildNodes)
                {
                    switch (childNode.Name)
                    {
                        case "title":
                            item.title = childNode.InnerText;
                            break;
                        case "description":
                            item.description = childNode.InnerText;
                            break;
                        case "link":
                            item.link = childNode.InnerText;
                            break;
                        case "pubDate":
                            item.pubDate = childNode.InnerText;
                            break;
                    }
                }
                
                items.Add(item);
            }
            
            return items.ToArray();
        }
        
        /// <summary>
        /// Get formatted RSS summary for AI
        /// </summary>
        public string FormatRSSForAI(RSSItem[] items)
        {
            if (items == null || items.Length == 0)
                return "No news items available.";
            
            string summary = "Latest News Headlines:\n";
            for (int i = 0; i < items.Length; i++)
            {
                summary += $"{i + 1}. {items[i].title}\n";
            }
            
            return summary;
        }
    }
}
