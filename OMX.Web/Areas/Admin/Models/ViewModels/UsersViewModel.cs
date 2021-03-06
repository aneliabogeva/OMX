﻿using AutoMapper;

namespace OMX.Web.Areas.Admin.Models.ViewModels
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        [IgnoreMap]
        public bool IsSuspended { get; set; } = false;

    }
}
