using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OMX.Web.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace OMX.Test.Web
{
    [TestClass]
    public class AdminUsersController
    {
        [TestMethod]
        public void MakeModerator_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange

            // Act

            //Assert
        }
        [TestMethod]
        public void Demote_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange

            // Act

            //Assert
        }
        [TestMethod]
        public void ChangePassword_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange

            // Act

            //Assert
        }
        [TestMethod]
        public void Lock_Should_Return_ErrorPage_When_IdIs_Invalid()
        {
            // Arrange
            var controller = new UsersController(null,null,null,null);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Role,"Administrator")
                    }))
                }
            };
            // Act

            //Assert
        }
    }
}
