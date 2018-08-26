using AutoMapper;
using Microsoft.AspNetCore.Http;
using OMX.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMX.Models
{
    public class Property
    {
        private const string minimumValue = "0";
        private const string maximumPriceValue = "100000000";
        private const int maximumTitleValue = 100;
        private const int minimumTitleValue = 4;
        private const int defaultBedBathRoomValue = 0;

        public Property()
        {
            this.Features = new List<PropertyFeature>();
            this.ImageNames = new List<Image>();
            this.NumberOfBathrooms = defaultBedBathRoomValue;
            this.NumberOfBedrooms = defaultBedBathRoomValue;
            this.IsFeatured = false;
            this.IsApproved = false;
        }

        public int Id { get; set; }
        [Required]
        [Range(typeof(decimal), minimumValue, maximumPriceValue)]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(maximumTitleValue)]
        [MinLength(minimumTitleValue)]
        public string Title { get; set; }
        [Required]
        public PropertyType PropertyType { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public CurrencyTypes Currency { get; set; }
        public bool IsApproved { get; set; }
        [IgnoreMap]
        public int NumberOfBathrooms { get; set; }
        public int NumberOfBedrooms { get; set; }
        public bool IsFeatured { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public double? IndoorArea { get; set; }
        public double? OutdoorArea { get; set; }
        public double? LandPlotSize { get; set; }
        public int? Floor { get; set; }
        public DateTime PostedOn { get; set; }
        public ICollection<PropertyFeature> Features { get; set; }
        public ICollection<Image> ImageNames { get; set; }


    }
}
