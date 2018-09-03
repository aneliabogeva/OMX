using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OMX.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public abstract class AdminController : Controller
    {
        protected IActionResult ErrorPage()
        {
            return RedirectToAction("NotFound", "Error", new { area = "" });
        }
    }
}