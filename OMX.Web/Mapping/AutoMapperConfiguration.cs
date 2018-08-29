using AutoMapper;
using OMX.Common.Property.BindingModels;
using OMX.Common.Property.ViewModels;
using OMX.Models;
using OMX.Web.Areas.Admin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMX.Web.Mapping
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            this.CreateMap<PropertyBindingModel, Property>();  
            
            this.CreateMap<Property, PropertyBindingModel>()
                .ForMember(x=> x.Features, x=> x.Ignore());

            this.CreateMap<Property, HomePropertiesViewModel>()
                .ForMember(x=> x.Message, x=> x.Ignore());


        }
    }

}
