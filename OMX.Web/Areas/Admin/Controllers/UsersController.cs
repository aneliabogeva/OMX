using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OMX.Common.Property.BindingModels;
using OMX.Data;
using OMX.Models;
using OMX.Services;
using OMX.Services.Contracts;
using OMX.Web.Areas.Admin.Models.ViewModels;

namespace OMX.Web.Areas.Admin.Controllers
{

    public class UsersController : AdminController
    {
        private const string SUCCESS_PROMOTED_MESSAGE = "The user has been successfully promoted!";
        private const string SUCCESS_DEMOTED_MESSAGE = "The user has been successfully demoted!";
        private const string SUSPENDED_MESSAGE = "The user has been suspended!";
        private const string REACTIVATED_MESSAGE = "The user has been reactivated!";

        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly OmxDbContext context;

        public UsersController(IUserService userService, IMapper mapper, UserManager<User> userManager, OmxDbContext context)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> All(string message = null)
        {
            var users = this.userService.GetAllUsers();
            foreach (var user in users)
            {
                var isSuspended = await userManager.IsLockedOutAsync(user);
                if (isSuspended)
                {
                    user.IsSuspended = true;
                }
            }
            var model = this.mapper.Map<IEnumerable<UsersViewModel>>(users);


            ViewBag.statusMessage = message;
            return View(model);
        }

        public async Task<IActionResult> MakeModerator(string id)
        {
            var user = this.userService.GetUserById(id);

            await this.userManager.AddToRoleAsync(user, "Moderator");
            await userManager.UpdateSecurityStampAsync(user);
            await context.SaveChangesAsync();

            return RedirectToAction("All", "Users",
                new { message = SUCCESS_PROMOTED_MESSAGE });
        }
        public async Task<IActionResult> Demote(string id)
        {
            var user = this.userService.GetUserById(id);

            await this.userManager.RemoveFromRoleAsync(user, "Moderator");
            await userManager.UpdateSecurityStampAsync(user);
            await context.SaveChangesAsync();

            return RedirectToAction("All", "Users",
                new { message = SUCCESS_DEMOTED_MESSAGE });
        }

        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            var user = this.userService.GetUserById(id);
            var model = new ChangePasswordBindingModel()
            {
                User = user
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordBindingModel model, string id)
        {
            var user = this.userService.GetUserById(id);
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }


            var resetToken = await this.userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult passwordChangeResult = await this.userManager.ResetPasswordAsync(user, resetToken, model.Password);

            return RedirectToAction("All", "Users");
        }

        public async Task<IActionResult> Lock(string id)
        {
            this.TempData["_Message"] = @"<div class=""alert alert-primary"" role=""alert"">
  User has been locked out successfully!
</ div > ";
            var user = this.userService.GetUserById(id);

            await userManager.SetLockoutEnabledAsync(user, true);
            await userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(100));
            await userManager.UpdateSecurityStampAsync(user);

            return RedirectToAction("All", "Users");
        }

        public async Task<IActionResult> Unlock(string id)
        {
            var user = this.userService.GetUserById(id);

            await userManager.SetLockoutEnabledAsync(user, false);
            await userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);
            await userManager.UpdateSecurityStampAsync(user);

            return RedirectToAction("All", "Users");
        }

        public IActionResult Details(string id)
        {
            var user = this.userService.GetUserById(id);



            return this.View(user);
        }
    }
}