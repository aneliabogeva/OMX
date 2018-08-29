using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OMX.Data;
using OMX.Models;
using OMX.Services;
using OMX.Test.Mocks;
using OMX.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OMX.Test.Services
{
    [TestClass]
    public class PropertiesServiceTest
    {
        private OmxDbContext dbContext;
        private IMapper mapper;


        [TestMethod]
        public void Property_Should_Be_Made_Featured()
        {
            // Arrange           

            ICollection<Image> images = new List<Image>();
            ICollection<PropertyFeature> features = new List<PropertyFeature>();
            var address = new Address();

            this.dbContext.Properties
                .Add(new Property
                {
                    Id = 1,
                    IsFeatured = false,
                    AddressId = 1,
                    IsApproved = true,
                    UserId = new Guid().ToString(),
                    Address = address,
                    ImageNames = images,
                    Features = features
                });

            this.dbContext.Properties
               .Add(new Property
               {
                   Id = 2,
                   IsFeatured = false,
                   AddressId = 1,
                   IsApproved = true,
                   UserId = new Guid().ToString(),
                   Address = address,
                   ImageNames = images,
                   Features = features
               });

            this.dbContext.SaveChanges();

            var propertyService = new PropertyService(this.dbContext, this.mapper);

            // Act

            propertyService.MakePropertyFeatured(1);
            var property = propertyService.GetPropertyById(1);
            var isFeatured = property.IsFeatured;
            //Assert

            Assert.IsNotNull(property);
            Assert.IsTrue(isFeatured);

        }
        [TestMethod]
        public void Property_Should_Be_Deleted()
        {
            // Arrange
            ICollection<Image> images = new List<Image>();
            ICollection<PropertyFeature> features = new List<PropertyFeature>();
            var address = new Address();

            this.dbContext.Properties
                .Add(new Property
                {
                    Id = 1,
                    IsFeatured = false,
                    AddressId = 1,
                    IsApproved = true,
                    UserId = new Guid().ToString(),
                    Address = address,
                    ImageNames = images,
                    Features = features
                });
            this.dbContext.Properties
               .Add(new Property
               {
                   Id = 2,
                   IsFeatured = false,
                   AddressId = 1,
                   IsApproved = true,
                   UserId = new Guid().ToString(),
                   Address = address,
                   ImageNames = images,
                   Features = features
               });

            this.dbContext.SaveChanges();
            var propertyService = new PropertyService(this.dbContext, this.mapper);
            // Act
            propertyService.DeletePropertyById(1);
            this.dbContext.SaveChanges();
            var allProperties =  dbContext.Properties.ToList();
            var property = this.dbContext.Properties.First();
            //Assert
            Assert.AreEqual(1, allProperties.Count);
            Assert.AreEqual(2, property.Id);
        }
        [TestMethod]
        public void The_Correct_Property_Should_Be_Returned()
        {
            // Arrange
            ICollection<Image> images = new List<Image>();
            ICollection<PropertyFeature> features = new List<PropertyFeature>();
            var address = new Address();

            this.dbContext.Properties
                .Add(new Property
                {
                    Id = 1,
                    IsFeatured = false,
                    AddressId = 1,
                    IsApproved = true,
                    UserId = new Guid().ToString(),
                    Address = address,
                    ImageNames = images,
                    Features = features
                });

            this.dbContext.Properties
               .Add(new Property
               {
                   Id = 2,
                   IsFeatured = false,
                   AddressId = 1,
                   IsApproved = true,
                   UserId = new Guid().ToString(),
                   Address = address,
                   ImageNames = images,
                   Features = features
               });

            this.dbContext.SaveChanges();

            var propertyService = new PropertyService(this.dbContext, this.mapper);
            // Act
            var property = propertyService.GetPropertyById(2);
            //Assert
            Assert.IsNotNull(property);
            Assert.AreEqual(2, property.Id);
        }
        [TestMethod]
        public void Listing_Should_Be_Approved()
        {
            // Arrange
            ICollection<Image> images = new List<Image>();
            ICollection<PropertyFeature> features = new List<PropertyFeature>();
            var address = new Address();

            this.dbContext.Properties
                .Add(new Property
                {
                    Id = 1,
                    IsFeatured = false,
                    AddressId = 1,
                    IsApproved = false,
                    UserId = new Guid().ToString(),
                    Address = address,
                    ImageNames = images,
                    Features = features
                });
            
            this.dbContext.SaveChanges();

            var propertyService = new PropertyService(this.dbContext, this.mapper);
            // Act
            var property = propertyService.GetPropertyById(1);
            propertyService.ApproveProperty(1);
            //Assert
            Assert.IsTrue(property.IsApproved);
        }
        [TestMethod]
        public void Approved_And_Featured_Listings_Should_Be_Returned()
        {
            // Arrange

            ICollection<Image> images = new List<Image>();
            ICollection<PropertyFeature> features = new List<PropertyFeature>();
            var address = new Address();

            this.dbContext.Properties
                .Add(new Property
                {
                    Id = 1,
                    IsFeatured = false,
                    AddressId = 1,
                    IsApproved = true,
                    UserId = new Guid().ToString(),
                    Address = address,
                    ImageNames = images,
                    Features = features
                }
                );

            this.dbContext.Properties
                .Add(new Property
                {
                    Id = 2,
                    IsFeatured = true,
                    AddressId = 1,
                    IsApproved = true,
                    UserId = new Guid().ToString(),
                    Address = address,
                    ImageNames = images,
                    Features = features
                }
                );
            this.dbContext.Properties
               .Add(new Property
               {
                   Id = 3,
                   IsFeatured = true,
                   AddressId = 1,
                   IsApproved = false,
                   UserId = new Guid().ToString(),
                   Address = address,
                   ImageNames = images,
                   Features = features
               }
               );
            this.dbContext.Properties
               .Add(new Property
               {
                   Id = 4,
                   IsFeatured = true,
                   AddressId = 1,
                   IsApproved = true,
                   UserId = new Guid().ToString(),
                   Address = address,
                   ImageNames = images,
                   Features = features
               }
               );

            this.dbContext.SaveChanges();
            var allProperties = this.dbContext.Properties.ToListAsync();
            var propertyService = new PropertyService(this.dbContext, this.mapper);

            // Act
            var listingsCount = propertyService.GetAllFeaturedProperties().Count;
            //Assert
            Assert.AreEqual(2, listingsCount);
        }

        [TestInitialize]
        public void InitializeTests()
        {
            this.dbContext = MockDbContext.GetContext();
            this.mapper = MockAutoMapper.GetAutoMapper();
        }
    }
}
