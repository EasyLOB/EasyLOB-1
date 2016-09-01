using EasyLOB.Extensions.Ini;
using System.Globalization;
using System.Web;

namespace EasyLOB.Library.Mvc
{
    public static class MenuResourcesHelper
    {
        private static IIniManager iniManager = new IniManager(HttpContext.Current.Server.MapPath("~/EasyLOB/INI/MenuResources.ini"));

        public static string GetResource(string resourceKey)
        {
            string resourceValue = iniManager.Read(CultureInfo.CurrentCulture.Name, resourceKey);

            if (string.IsNullOrEmpty(resourceValue))
            {
                resourceValue = iniManager.Read("culture", resourceKey);
            }

            if (string.IsNullOrEmpty(resourceValue))
            {
                resourceValue = "? " + resourceKey.Trim() + " ?";
            }

            return resourceValue;
        }
    }
}