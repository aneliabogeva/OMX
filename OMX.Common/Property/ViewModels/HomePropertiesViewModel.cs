using Microsoft.AspNetCore.Http;
using OMX.Models;
using OMX.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMX.Common.Property.ViewModels
{
    public class HomePropertiesViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public CurrencyTypes Currency { get; set; }
        public bool IsFeatured { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public PropertyType PropertyType { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public double? IndoorArea { get; set; }
        public double? OutdoorArea { get; set; }
        public double? LandPlotSize { get; set; }
        public int? Floor { get; set; }
        public Address Address { get; set; }
        public ICollection<Image> ImageNames { get; set; } = new List<Image>();
        public ICollection<PropertyFeature> Features { get; set; } = new List<PropertyFeature>();

        public string Message { get; set; }
    }
}
