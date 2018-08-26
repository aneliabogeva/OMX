using OMX.Common.Property.BindingModels;
using OMX.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMX.Services.Contracts
{
    public interface IPropertyService
    {
        ICollection<Feature> GetAllFeatures();
        IEnumerable<int> GetAllSelectedFeatures(int propertyID);
        Property CreateProperty(PropertyBindingModel model, string userId);
        Property GetPropertyById(int id);
        ICollection<Property> GetAllProperties();
        void DeletePropertyById(int id);
        void MakePropertyFeatured(int id);
        void ApproveProperty(int id);
        ICollection<Property> GetAllFeaturedProperties();
        void RemovePropertyFeatured(int id);
        ICollection<Address> GetAllAddresses();
        void EditProperty(int id, PropertyBindingModel model);
        ICollection<Property> GetUserPropertiesById(string id);


    }
}
