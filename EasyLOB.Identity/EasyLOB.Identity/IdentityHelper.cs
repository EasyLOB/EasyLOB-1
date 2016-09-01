using Microsoft.AspNet.Identity;
using System.Web;

namespace EasyLOB.Identity
{
    public static partial class IdentityHelper
    {
        #region Properties

        public static bool IsAdministrator
        {
            get
            {
                try
                {
                    return "|ADM|ADMIN|ADMINISTRATOR|ADMINISTRADOR|".Contains("|" + UserName.ToUpper() + "|");
                }
                catch
                {
                    return false;
                }
            }
        }

        public static string UserId
        {
            get
            {
                string userId = "";

                try
                {
                    userId = HttpContext.Current.User.Identity.GetUserId();
                }
                catch { }

                return userId;
            }
        }

        public static string UserName
        {
            get
            {
                string userName = "";

                try
                {
                    userName = HttpContext.Current.User.Identity.GetUserName();
                }
                catch { }

                return userName;
            }
        }

        #endregion Properties

        #region Methods

        public static bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
            //return UserManager.IsInRole(UserId, role); //
        }

        #endregion Methods
    }
}