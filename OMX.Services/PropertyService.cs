using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OMX.Common.Property.BindingModels;
using OMX.Data;
using OMX.Models;
using OMX.Services.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OMX.Services
{
    public class PropertyService : BaseService, IPropertyService
    {
        public PropertyService(OmxDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public ICollection<Feature> GetAllFeatures()
        {
            var features = this.DbContext.Features.ToList();

            return features;
        }

        public IEnumerable<int> GetAllSelectedFeatures(int propertyID)
        {
            var selectedFeatures = this.DbContext.PropertiesFeatures
                                       .Where(pf => pf.PropertyId == propertyID)
                                       .Select(f => f.FeatureId);

            return selectedFeatures;
        }
        public ICollection<Address> GetAllAddresses()
        {
            var addresses = this.DbContext.Addresses.ToList();

            return addresses;
        }

        public Property CreateProperty(PropertyBindingModel model, string userId)
        {
            var property = this.Mapper.Map<Property>(model);
            property.NumberOfBathrooms = model.NumberOfBathrooms;
            property.UserId = userId;
            using (DbContext)
            {
                DbContext.Properties.Add(property);

                this.DbContext.SaveChanges();


                if (model.SelectedFeatures.Any())
                {
                    foreach (var featureId in model.SelectedFeatures)
                    {
                        AddFeaturesToProperty(property, featureId);
                    }
                }

                if (model.Images.Any())
                {
                    AddImagesToProperty(model, property);
                }

                this.DbContext.SaveChanges();
            }

            return property;

        }
        public void EditProperty(int id, PropertyBindingModel model)
        {
            var property = this.GetPropertyById(id);

            property.Title = model.Title;
            property.Price = model.Price;
            property.NumberOfBathrooms = model.NumberOfBathrooms;
            property.NumberOfBedrooms = model.NumberOfBedrooms;            
            property.Description = model.Description;
            property.IndoorArea = model.IndoorArea;                  
            property.Floor = model.Floor;
            property.Currency = model.Currency;
            property.AddressId = model.AddressId;
            property.PropertyType = model.PropertyType;

            
            if (model.SelectedFeatures.Any())
            {
                property.Features.Clear();
                this.DbContext.SaveChanges();
                foreach (var featureId in model.SelectedFeatures)
                {
                    AddFeaturesToProperty(property, featureId);
                }
            }

            if (model.Images.Any())
            {
                DeletePropertyImages(property.Id);
                property.ImageNames.Clear();
                DbContext.SaveChanges();
                AddImagesToProperty(model, property);
            }

            this.DbContext.SaveChanges();
        }


        public ICollection<Property> GetAllProperties()
        {
            return this.DbContext.Properties
                .Include(e => e.User)
                .Include(e => e.ImageNames)
                .Include(e => e.Features)
                .ToList();
        }
        public ICollection<Property> GetAllFeaturedProperties()
        {
            var properties = this.DbContext.Properties
                .Include(e => e.Features)
                .Include(e => e.User)
                .Include(e => e.Address)
                .Include(e => e.ImageNames)
                .Where(e => e.IsApproved && e.IsFeatured).ToList();

            var features = this.DbContext.Features;

            foreach (var pr in properties)
            {
                foreach (var item in pr.Features)
                {
                    var feature = features.Find(item.FeatureId);
                    item.Feature = feature;
                }
            }
            return properties;
        }
       
        public Property GetPropertyById(int id)
        {
            
            var property = this.DbContext.Properties
                .Include(e => e.Features)
                .Include(e => e.ImageNames)
                .Include(e => e.Address)
                .Include(e=> e.User)
                .FirstOrDefault(e => e.Id == id);

            var features = this.DbContext.Features;

            if (property != null)
            {
                foreach (var item in property.Features)
                {
                    var feature = features.Find(item.FeatureId);
                    item.Feature = feature;
                }
            }

            return property;
        }
        
        public void DeletePropertyById(int id)
        {
            var property = this.GetPropertyById(id);
            this.DbContext.Properties.Remove(property);

            DeletePropertyImages(id);

            DbContext.SaveChanges();
        }

        public void MakePropertyFeatured(int id)
        {
            var property = this.GetPropertyById(id);
            property.IsFeatured = true;
            property.IsApproved = true;
            DbContext.SaveChanges();
        }
        public void RemovePropertyFeatured(int id)
        {
            var property = this.GetPropertyById(id);
            property.IsFeatured = false;
            DbContext.SaveChanges();
        }
        public void ApproveProperty(int id)
        {
            var property = this.GetPropertyById(id);
            property.IsApproved = true;
            DbContext.SaveChanges();
        }

        public ICollection<Property> GetUserPropertiesById(string id)
        {
            var properties = this.DbContext.Properties
                .Where(e => e.UserId == id)
                .Include(e => e.ImageNames)
                .Include(e=> e.Address)
                .ToList();

            return properties;
        }

        private void DeletePropertyImages(int id)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"{id}");

            DirectoryInfo di = new DirectoryInfo(filePath);
            if (di.Exists)
                di.Delete(true);
        }
        private void AddFeaturesToProperty(Property property, int featureId)
        {          

            var feature = this.DbContext.Features.FirstOrDefault(e => e.Id == featureId);
            var pf = new PropertyFeature()
            {
                Feature = feature,
                Property = property
            };
            this.DbContext.PropertiesFeatures.Add(pf);
            DbContext.SaveChanges();
        }

        private void AddImagesToProperty(PropertyBindingModel model, Property property)
        {
            foreach (var img in model.Images)
            {
                if (img.ContentType=="image/jpeg")
                {
                    var imgName = img.FileName;
                    var image = new Image()
                    {
                        Name = imgName,

                    };

                    property.ImageNames.Add(image);
                }
            }
            this.DbContext.SaveChanges();
        }

    }
}
