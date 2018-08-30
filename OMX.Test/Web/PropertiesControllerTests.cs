using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OMX.Data;
using OMX.Models;
using OMX.Services;
using OMX.Services.Contracts;
using OMX.Test.Mocks;
using OMX.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using OMX.Common.Property.BindingModels;

namespace OMX.Test.Web
{
    [TestClass]
    public class PropertiesControllerTests
    {
        private const int InvalidPropertyId = 250;
        private const string NotFoundPage = "NotFound";
        private IMapper mapper;
        private IPropertyService propertyService;
        private IUserService userService;
        private OmxDbContext dbContext;
        private UserManager<User> userManager;
        private Mock<IConfiguration> mockedConfig;
        private PropertiesController propertiesController;


        [TestMethod]
        public void Create_Should_Return_ErrorPage_When_User_Is_Null()
        {
            // Arrange

            this.propertiesController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                   {
                        new Claim(ClaimTypes.Authentication, " ")
                    }))
                }
            };

            // Act

            RedirectToActionResult result = (RedirectToActionResult)this.propertiesController.Create();

            // Assert

            Assert.AreEqual(NotFoundPage, result.ActionName);



        }
        [TestMethod]
        public void Create_Should_Return_AccountManage_When_Email_IsNot_Confirmed()
        {
            // Arrange
                       
            this.dbContext.Users.Add(new User()
            {
                Email = "test@gmail.com",
                UserName = "test@gmail.com",
                EmailConfirmed = false,

            });

            this.dbContext.SaveChanges();

            this.propertiesController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Email,"test@gmail.com"),
                        new Claim(ClaimTypes.Role, "Administrator"),
                        new Claim(ClaimTypes.Name, "test@gmail.com"),
                    }))
                }
            };

           
            // Act

            RedirectToActionResult result = (RedirectToActionResult)this.propertiesController.Create();

            // Assert

            Assert.AreEqual("Index", result.ActionName);


        }
        [TestMethod]
        public void Details_Should_Return_ErrorPage_When_PropertyIs_Null()
        {
            // Arrange
            
            ICollection<Image> images = new List<Image>();
            ICollection<PropertyFeature> features = new List<PropertyFeature>();
            var address = new Address();
            this.dbContext.Properties.Add(new Property()
            {
                Id = 251,
                IsFeatured = false,
                AddressId = 1,
                IsApproved = true,
                UserId = new Guid().ToString(),
                Address = address,
                ImageNames = images,
                Features = features,
                Title = "Some Title"


            });
            dbContext.SaveChanges();

            // Act

            RedirectToActionResult result = (RedirectToActionResult)this.propertiesController.Details(InvalidPropertyId);

            // Assert

            Assert.AreEqual(NotFoundPage, result.ActionName);

        }
        [TestMethod]
        public void EditPost_Should_Return_ErrorPage_When_Model_IsNull()
        {
            // Arrange
            PropertyBindingModel model = null;

            // Act

            RedirectToActionResult result = (RedirectToActionResult)this.propertiesController.Edit(model);

            // Assert

            Assert.AreEqual(NotFoundPage, result.ActionName);


        }
        [TestMethod]
        public void EditGet_Should_Return_ErrorPage_When_Property_IsNull()
        {                                          

            RedirectToActionResult result = (RedirectToActionResult)this.propertiesController.Edit(InvalidPropertyId);            

            Assert.AreEqual(NotFoundPage, result.ActionName);

        }
        [TestMethod]
        public void DeletePost_Should_Return_ErrorPage_When_Model_IsNull()
        {
            // Arrange
            PropertyBindingModel model = null;

            // Act

            RedirectToActionResult result = (RedirectToActionResult)this.propertiesController.Delete(model);

            // Assert

            Assert.AreEqual(NotFoundPage, result.ActionName);

        }
        [TestMethod]
        public void DeleteGet_Should_Return_ErrorPage_When_Property_IsNull()
        {
            // Arrange
            int propertyId = InvalidPropertyId;

            // Act

            RedirectToActionResult result = (RedirectToActionResult)this.propertiesController.Delete(propertyId);

            // Assert

            Assert.AreEqual(NotFoundPage, result.ActionName);

        }

        [TestInitialize]
        public void InitializeTests()
        {
            this.dbContext = MockDbContext.GetContext();
            this.mapper = MockAutoMapper.GetAutoMapper();
            this.propertyService = new PropertyService(dbContext, mapper);
            this.userManager = this.TestUserManager<User>();
            this.userService = new UserService(dbContext, mapper, userManager);
            this.mockedConfig =  new Mock<IConfiguration>();
            this.propertiesController = new PropertiesController(propertyService, userService, mapper, userManager, dbContext, mockedConfig.Object);
        }
        private UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }
    }
}
