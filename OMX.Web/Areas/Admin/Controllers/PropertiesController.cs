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
        private readonly IPropertyService propertyService;
        private readonly IMapper mapper;

        public PropertiesController(IPropertyService propertyService, IMapper mapper)
        {
            this.propertyService = propertyService;
            this.mapper = mapper;
        }

        public IActionResult All()
        {
            var properties = this.propertyService.GetAllProperties();
            var model = mapper.Map<ICollection<AdminPropertiesViewModel>>(properties);

            return View(model);
        }

        public IActionResult Delete(int id)
        {
           this.propertyService.DeletePropertyById(id);

            return RedirectToAction("All", "Properties");
        }
        public IActionResult MakeFeatured(int id)
        {
            this.propertyService.MakePropertyFeatured(id);

            return RedirectToAction("All", "Properties");
        }
        public IActionResult ApproveListing(int id)
        {
            this.propertyService.ApproveProperty(id);

            return RedirectToAction("All", "Properties");
        }
        public IActionResult RemoveFeatured(int id)
        {
            this.propertyService.RemovePropertyFeatured(id);

            return RedirectToAction("All", "Properties");
        }
        
    }
}