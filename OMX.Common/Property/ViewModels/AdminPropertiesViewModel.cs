using OMX.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMX.Common.Property.ViewModels
{
    public class AdminPropertiesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User User { get; set; }
        public bool IsApproved { get; set; }
        public bool IsFeatured { get; set; }

    }
}
