using System;
using System.Collections.Generic;
using System.Web;

namespace EasyLOB.Library.Mvc
{
    /// <summary>
    /// MVC Helper
    /// </summary>
    public static partial class MvcHelper
    {
        #region Methods URL Dictionary

        /// <summary>
        /// Clear URL Dictionary.
        /// </summary>
        /// <param name="httpContext">HTTP Context</param>
        public static void ClearUrlDictionary(HttpContextBase httpContext)
        {
            httpContext.Session[MvcDefaults.UrlDictionarySessionName] = null;
        }

        /// <summary>
        /// Set URL Dictionary in session.
        /// </summary>
        /// <param name="httpContext">HTTP Context</param>
        /// <param name="dictionary">Dictionary</param>
        private static void SetUrlDictionary(HttpContextBase httpContext, Dictionary<string, string> dictionary)
        {
            httpContext.Session[MvcDefaults.UrlDictionarySessionName] = dictionary;
        }

        /// <summary>
        /// Get URL Dictionary from session.
        /// </summary>
        /// <param name="httpContext">HTTP Context</param>
        /// <returns></returns>
        private static Dictionary<string, string> GetUrlDictionary(HttpContextBase httpContext)
        {
            if (httpContext.Session[MvcDefaults.UrlDictionarySessionName] == null)
            {
                httpContext.Session[MvcDefaults.UrlDictionarySessionName] = new Dictionary<string, string>();
            }

            return (Dictionary<string, string>)httpContext.Session[MvcDefaults.UrlDictionarySessionName];
        }

        /// <summary>
        /// Write URL to URL Dictionary.
        /// </summary>
        /// <param name="httpContext">HTTP Context</param>
        /// <param name="controller">Controller</param>
        /// <param name="url">URL</param>
        public static void WriteUrlDictionary(HttpContextBase httpContext, string controller, string url = null)
        {
            url = String.IsNullOrEmpty(url) ? httpContext.Request.Url.AbsoluteUri : url;

            Dictionary<string, string> dictionary = GetUrlDictionary(httpContext);

            if (!dictionary.ContainsKey(controller))
            {
                dictionary.Add(controller, url);
            }
            else
            {
                dictionary[controller] = url;
            }

            SetUrlDictionary(httpContext, dictionary);
        }

        /// <summary>
        /// Read URL from URL Dictionary.
        /// </summary>
        /// <param name="httpContext">HTTP Context</param>
        /// <param name="controller">Controller</param>
        /// <returns></returns>
        public static string ReadUrlDictionary(HttpContextBase httpContext, string controller)
        {
            string result = "";

            Dictionary<string, string> dictionary = GetUrlDictionary(httpContext);
            if (dictionary.ContainsKey(controller))
            {
                result = (string)dictionary[controller];
            }

            return result;
        }

        /// <summary>
        /// Render URL Dictionary.
        /// </summary>
        /// <param name="httpContext">HTTP Context</param>
        /// <returns></returns>
        public static string RenderUrlDictionary(HttpContextBase httpContext)
        {
            string result = "";

            Dictionary<string, string> urlDictionary = GetUrlDictionary(httpContext);
            int dictionaryIndex = 0;
            foreach (KeyValuePair<string, string> pair in urlDictionary)
            {
                result += ("[ " + dictionaryIndex++.ToString() + " ][ " + pair.Key + " ] => " + pair.Value + "<br />");
            }

            return (String.IsNullOrEmpty(result) ? result : "<br >" + result);
        }

        #endregion Methods URL Dictionary
    }
}