using System.Net;
using System.Web.Mvc;

namespace EasyLOB.Library.Mvc
{
    /// <summary>
    /// Validate ModelState Attribute
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        #region Methods

        /// <summary>
        /// OnAcyionExecuting.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.IsValid == false)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        #endregion Methods
    }
}