using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OMX.Web.Areas.Admin.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using OMX.Test.Mocks;
using OMX.Services;
using OMX.Models;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoMapper;
using OMX.Services.Contracts;
using OMX.Data;
using OMX.Common.Property.BindingModels;

namespace OMX.Test.Web
{
    [TestClass]
    public class AdminUsersController
    {

        private const string NotFoundPage = "NotFound";
        private IMapper mapper;
        private IUserService userService;
        private OmxDbContext dbContext;
        private UserManager<User> userManager;
        private Mock<IConfiguration> mockedConfig;
        private UsersController usersController;


        [TestMethod]
        public async Task MakeModerator_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange
            var invalidUserId = new Guid().ToString();
            // Act
            RedirectToActionResult result = (RedirectToActionResult)await this.usersController.MakeModerator(invalidUserId);

            //Assert
            Assert.AreEqual(NotFoundPage, result.ActionName);

        }
        [TestMethod]
        public async Task Demote_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange
            var invalidUserId = new Guid().ToString();
            // Act
            RedirectToActionResult result = (RedirectToActionResult)await this.usersController.Demote(invalidUserId);

            //Assert
            Assert.AreEqual(NotFoundPage, result.ActionName);
        }
        [TestMethod]
        public void ChangePasswordGet_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange
            var invalidUserId = new Guid().ToString();
            // Act
            RedirectToActionResult result = (RedirectToActionResult)this.usersController.ChangePassword(invalidUserId);

            //Assert
            Assert.AreEqual(NotFoundPage, result.ActionName);
        }
        [TestMethod]
        public async Task ChangePasswordPost_Should_Return_ErrorPage_When_IdIs_OrModel_Is_Invalid()
        {
            // Arrange
            var invalidUserId = new Guid().ToString();
            ChangePasswordBindingModel model = null;
            // Act
            RedirectToActionResult result = (RedirectToActionResult)await this.usersController.ChangePassword(model, invalidUserId);

            //Assert
            Assert.AreEqual(NotFoundPage, result.ActionName);
        }
        [TestMethod]
        public async Task Lock_Should_Return_ErrorPage_When_IdIs_Invalid()
        {

            // Arrange
            var invalidUserId = new Guid().ToString();
            // Act
            RedirectToActionResult result = (RedirectToActionResult)await this.usersController.Lock(invalidUserId);

            //Assert
            Assert.AreEqual(NotFoundPage, result.ActionName);

        }
        [TestInitialize]
        public void InitializeTests()
        {
            this.dbContext = MockDbContext.GetContext();
            this.mapper = MockAutoMapper.GetAutoMapper();
            this.userManager = this.TestUserManager<User>();
            this.userService = new UserService(dbContext, mapper, userManager);
            this.mockedConfig = new Mock<IConfiguration>();
            this.usersController = new UsersController(userService, mapper, userManager, dbContext);
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
