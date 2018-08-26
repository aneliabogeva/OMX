using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMX.Common.Property.BindingModels;
using OMX.Common.Property.ViewModels;
using OMX.Services.Contracts;

namespace OMX.Web.Areas.Admin.Controllers
{
    public class PropertiesController : AdminController
    {
        private const string LISTING_DELETED_MESSAGE = "The listing has been successfully deleted!";
        private const string LISTING_FEATURED_MESSAGE = "The listing is now featured!";
        private const string LISTING_APPROVED_MESSAGE = "The listing is now approved!";
        private const string LISTING_REMOVED_FROM_FEATURED = "The listing is no longer featured!";

        private readonly IPropertyService propertyService;
        private readonly IMapper mapper;

        public PropertiesController(IPropertyService propertyService, IMapper mapper)
        {
            this.propertyService = propertyService;
            this.mapper = mapper;
        }

        public IActionResult All(string message = null)
        {
            var properties = this.propertyService.GetAllProperties();
            var model = mapper.Map<ICollection<AdminPropertiesViewModel>>(properties);
            ViewBag.statusMessage = message;
            return View(model);
        }

        public IActionResult Delete(int id)
        {
           this.propertyService.DeletePropertyById(id);

            return RedirectToAction("All", "Properties", new { message = LISTING_DELETED_MESSAGE });
        }
        public IActionResult MakeFeatured(int id)
        {
            this.propertyService.MakePropertyFeatured(id);

            return RedirectToAction("All", "Properties", new { message = LISTING_FEATURED_MESSAGE });
        }
        public IActionResult ApproveListing(int id)
        {
            this.propertyService.ApproveProperty(id);

            return RedirectToAction("All", "Properties", new { message = LISTING_APPROVED_MESSAGE });
        }
        public IActionResult RemoveFeatured(int id)
        {
            this.propertyService.RemovePropertyFeatured(id);

            return RedirectToAction("All", "Properties", new { message = LISTING_REMOVED_FROM_FEATURED });
        }
        
    }
}