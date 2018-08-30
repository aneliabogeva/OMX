using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OMX.Web.Areas.Admin.Controllers;
using Moq;
using OMX.Test.Mocks;
using OMX.Services;
using AutoMapper.Configuration;
using AutoMapper;
using OMX.Services.Contracts;
using OMX.Data;

namespace OMX.Test.Web
{


    public class AdminPropertiesController
    {
        private const string NotFoundPage = "NotFound";
        private IMapper mapper;
        private IPropertyService propertyService;
        private OmxDbContext dbContext;       
        private Mock<IConfiguration> mockedConfig;
        private PropertiesController propertiesController;

        [TestMethod]
        public void Delete_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange
            var invalidPropertyId = 250;
            // Act
            RedirectToActionResult result = (RedirectToActionResult) this.propertiesController.Delete(invalidPropertyId);

            //Assert
            Assert.AreEqual(NotFoundPage, result.ActionName);


        }
        [TestMethod]
        public void MakeFeatured_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange
            var invalidPropertyId = 250;
            // Act
            RedirectToActionResult result = (RedirectToActionResult)this.propertiesController.MakeFeatured(invalidPropertyId);

            //Assert
            Assert.AreEqual(NotFoundPage, result.ActionName);


        }
        [TestMethod]
        public void ApproveListing_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange
            var invalidPropertyId = 250;
            // Act
            RedirectToActionResult result = (RedirectToActionResult)this.propertiesController.ApproveListing(invalidPropertyId);

            //Assert
            Assert.AreEqual(NotFoundPage, result.ActionName);


        }
        [TestMethod]
        public void RemoveFeatured_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange
            var invalidPropertyId = 250;
            // Act
            RedirectToActionResult result = (RedirectToActionResult)this.propertiesController.RemoveFeatured(invalidPropertyId);

            //Assert
            Assert.AreEqual(NotFoundPage, result.ActionName);


        }
        [TestInitialize]
        public void InitializeTests()
        {
            this.dbContext = MockDbContext.GetContext();
            this.mapper = MockAutoMapper.GetAutoMapper();            
            this.propertyService = new PropertyService(dbContext, mapper);
            this.mockedConfig = new Mock<IConfiguration>();
            this.propertiesController = new PropertiesController(propertyService, mapper);
        }
        
    }
}
