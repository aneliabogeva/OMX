using OMX.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMX.Models
{
    public class Property
    {
        private const string MINIMUM_VALUE = "0";
        private const string MAXIMUM_PRICE_VALUE = "100000000";
        private const int MAXIMUM_TITLE_VALUE = 100;
        private const int MINIMUM_TITLE_VALUE = 4;
        private const int DEFAULT_BEDROOM_BATHROOM_VALUE = 0;

        public Property()
        {
            this.Features = new List<PropertyFeature>();
            this.ImageNames = new List<Image>();
            this.NumberOfBathrooms = DEFAULT_BEDROOM_BATHROOM_VALUE;
            this.NumberOfBedrooms = DEFAULT_BEDROOM_BATHROOM_VALUE;
            this.IsFeatured = false;
            this.IsApproved = false;
            this.PostedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }
        [Required]
        [Range(typeof(decimal), MINIMUM_VALUE, MAXIMUM_PRICE_VALUE)]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(MAXIMUM_TITLE_VALUE)]
        [MinLength(MINIMUM_TITLE_VALUE)]
        public string Title { get; set; }
        [Required]
        public PropertyType PropertyType { get; set; }        
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public CurrencyTypes Currency { get; set; }
        public bool IsApproved { get; set; }      
        public int NumberOfBathrooms { get; set; }
        public int NumberOfBedrooms { get; set; }
        public bool IsFeatured { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
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
