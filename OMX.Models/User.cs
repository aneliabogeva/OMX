using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMX.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Properties = new List<Property>();
            this.Reports = new List<Report>();
            this.IsSuspended = false;
        }

        [Required]
        public string FullName { get; set; }
        public bool IsSuspended { get; set; }
        public ICollection<Property> Properties { get; set; }
        public ICollection<Report> Reports { get; set; }

    }
}
