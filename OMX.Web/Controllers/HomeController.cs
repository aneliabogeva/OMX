using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OMX.Common.Property.ViewModels;
using OMX.Models;
using OMX.Services.Contracts;
using OMX.Web.Models;

namespace OMX.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public HomeController(IPropertyService propertyService, IMapper mapper, UserManager<User> userManager)
        {
            if (propertyService == null)
            {
                throw new ArgumentNullException();
            }
            if (mapper == null)
            {
                throw new ArgumentNullException();
            }
            if (userManager == null)
            {
                throw new ArgumentNullException();
            }

            this.propertyService = propertyService;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public IActionResult Index(string message = null)
        {
            var properties = this.propertyService.GetAllFeaturedProperties();
            var model = mapper.Map<ICollection<HomePropertiesViewModel>>(properties);
            if (message != null)
            {
                ViewBag.statusMessage = message;
            }
            
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
