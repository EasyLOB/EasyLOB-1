using System;
using System.Web.Mvc;

namespace EasyLOB.Library.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static string ImageLink(this HtmlHelper htmlHelper,
            string id,
            string uri,
            string imageClass,
            string imageTitle)
        {
            //return String.Format("<a id=\"{0}\" alt=\"{3}\" href=\"{1}\" class=\"{2}\" title=\"{3}\"/></a>",

            return String.Format("<a id=\"{0}\" href=\"{1}\"><img alt=\"{3}\" class=\"{2}\" title=\"{3}\"/></a>",
                id,
                uri,
                imageClass,
                imageTitle);
        }

        public static MvcHtmlString OperationResult(this HtmlHelper htmlHelper,
            ZOperationResult operationResult)
        {
            return MvcHtmlString.Create(operationResult.Html);
        }

        public static MvcHtmlString ResolveUrl(this HtmlHelper htmlHelper, string url)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return MvcHtmlString.Create(urlHelper.Content(url));
        }
    }
}