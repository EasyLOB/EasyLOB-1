using System;
using System.Web.Mvc;
//using System.Web.Mvc.Ajax;

// AjaxExtensions.ActionLink Method
// https://msdn.microsoft.com/en-us/library/system.web.mvc.ajax.ajaxextensions.actionlink%28v=vs.118%29.aspxv

namespace EasyLOB.Library.Mvc
{
    public static class AjaxHelperExtensions
    {
        public static string ImageLink(this AjaxHelper ajaxHelper,
            string id,
            string uri,
            string updateTargetId,
            string imageClass,
            string imageTitle)
        {
            //return String.Format("<a id=\"{0}\" data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"#{1}\" href=\"{2}\" class=\"{3}\" title=\"{4}\" /></a>",

            return String.Format("<a id=\"{0}\" data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"#{1}\" href=\"{2}\"><img alt=\"{4}\" class=\"{3}\" title=\"{4}\" /></a>",
                id,
                updateTargetId,
                uri,
                imageClass,
                imageTitle);
        }
    }
}