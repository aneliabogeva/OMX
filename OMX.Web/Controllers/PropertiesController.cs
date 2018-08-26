using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OMX.Common.Property.BindingModels;
using OMX.Common.Property.ViewModels;
using OMX.Data;
using OMX.Models;
using OMX.Services.Contracts;

namespace OMX.Web.Controllers
{
    [Authorize]
    public class PropertiesController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly OmxDbContext dbContext;

        public PropertiesController(IPropertyService propertyService,
            IMapper mapper,
            UserManager<User> userManager,
            OmxDbContext dbContext)
        {
            this.propertyService = propertyService;
            this.mapper = mapper;
            this.userManager = userManager;

            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var loggedInUser = HttpContext.User.Identity.Name;
            var user = dbContext.Users.FirstOrDefault(e => e.Email == loggedInUser);
            if (!user.EmailConfirmed)
            {
                return RedirectToAction("Index", "Identity/Account/Manage");
            }


            var model = new PropertyBindingModel
            {
                Features = propertyService.GetAllFeatures().ToDictionary(x => x.Id, x => x.Name),
                Addresses = propertyService.GetAllAddresses().ToList()

            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PropertyBindingModel model)
        {


            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", model);
            }


            var userId = this.userManager.GetUserId(HttpContext.User);
            var property = this.propertyService.CreateProperty(model, userId);

            SaveImagesToRoot(model, property);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int id)
        {
            var property = this.propertyService.GetPropertyById(id);

            var model = this.mapper.Map<HomePropertiesViewModel>(property);

            return this.View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var property = this.propertyService.GetPropertyById(id);
            var model = this.mapper.Map<PropertyBindingModel>(property);
            var userId = this.userManager.GetUserId(HttpContext.User);
            var isAdmin = this.User.IsInRole("Administrator");
            var isModerator = this.User.IsInRole("Moderator");


            if (!isAdmin)
            {
                if (!isModerator)
                {
                    if (property.UserId != userId)
                    {
                        return RedirectToAction("MyListings", "Users");
                    }
                }
            }

            model.Features = propertyService.GetAllFeatures().ToDictionary(x => x.Id, x => x.Name);
            model.Addresses = propertyService.GetAllAddresses().ToList();
            model.SelectedFeatures = propertyService.GetAllSelectedFeatures(id).ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(PropertyBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            this.propertyService.EditProperty(model.Id, model);


            return RedirectToAction("Edit", new { id = model.Id });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var property = this.propertyService.GetPropertyById(id);
            var model = this.mapper.Map<PropertyBindingModel>(property);
            var userId = this.userManager.GetUserId(HttpContext.User);
            var isAdmin = this.User.IsInRole("Administrator");
            var isModerator = this.User.IsInRole("Moderator");


            if (!isAdmin)
            {
                if (!isModerator)
                {
                    if (property.UserId != userId)
                    {
                        return RedirectToAction("MyListings", "Users");
                    }
                }


            }

            model.Features = propertyService.GetAllFeatures().ToDictionary(x => x.Id, x => x.Name);
            model.Addresses = propertyService.GetAllAddresses().ToList();


            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(PropertyBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            this.propertyService.EditProperty(model.Id, model);


            return RedirectToAction("All", "Properties");
        }

        private void SaveImagesToRoot(PropertyBindingModel model, Property property)
        {
            foreach (var image in model.Images.Take(4))
            {

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"{property.Id}");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var fileStream = new FileStream(Path.Combine(filePath, image.FileName), FileMode.Create);

                image.CopyTo(fileStream);

            }
        }
    }
}