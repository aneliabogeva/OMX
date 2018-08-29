using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMX.Common.Property.ViewModels;
using OMX.Services.Contracts;

namespace OMX.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator,Moderator")]    
    [Area("Admin")]
    public class PropertiesController : Controller
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
            try
            {
                this.propertyService.DeletePropertyById(id);
            }
            catch (System.Exception)
            {

                return RedirectToAction("NotFound", "Error", new { area = "" });
            }
           

            return RedirectToAction("All", "Properties", new { message = LISTING_DELETED_MESSAGE });
        }
        public IActionResult MakeFeatured(int id)
        {
            try
            {
                this.propertyService.MakePropertyFeatured(id);
            }
            catch (System.Exception)
            {

                return RedirectToAction("NotFound", "Error", new { area = "" });
            }
            

            return RedirectToAction("All", "Properties", new { message = LISTING_FEATURED_MESSAGE });
        }
        public IActionResult ApproveListing(int id)
        {
            
            try
            {
                this.propertyService.ApproveProperty(id);
            }
            catch (System.Exception)
            {

                return RedirectToAction("NotFound", "Error", new { area = "" });
            }
            return RedirectToAction("All", "Properties", new { message = LISTING_APPROVED_MESSAGE });
        }
        public IActionResult RemoveFeatured(int id)
        {
            try
            {
                this.propertyService.RemovePropertyFeatured(id);
            }
            catch (System.Exception)
            {
                return RedirectToAction("NotFound", "Error", new { area = "" });
            }            

            return RedirectToAction("All", "Properties", new { message = LISTING_REMOVED_FROM_FEATURED });
        }
        
    }
}