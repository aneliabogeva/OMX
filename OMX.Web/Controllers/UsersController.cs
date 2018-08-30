using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OMX.Models;
using OMX.Services.Contracts;

namespace OMX.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IPropertyService propertyService;

        public UsersController(UserManager<User> userManager, IPropertyService propertyService)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException();
            }
            if (propertyService == null)
            {
                throw new ArgumentNullException();
            }
            this.userManager = userManager;
            this.propertyService = propertyService;
        }

        public IActionResult MyListings()
        {
            var userId = this.userManager.GetUserId(HttpContext.User);
            var properties = this.propertyService.GetUserPropertiesById(userId);

            return View(properties);
        }
    }
}