using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TKOH.DTOs;
using TKOH.Extensions;

namespace TKOH.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = HttpContext.Session.GetObject<UserDTO>("CurrentUser");
            ViewBag.CurrentUser = user;
            base.OnActionExecuting(context);
        }
    }
}
