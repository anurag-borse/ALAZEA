using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ALAZEA.Models.AuthorizationAttribute
{
    public class AdminAuthorizeAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the session variable "AdminID" is set
            if (context.HttpContext.Session.GetString("AdminID") == null)
            {
                // If not authenticated, redirect to login
                context.Result = new RedirectToActionResult("AdminLogin", "Auth", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
