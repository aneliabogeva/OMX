using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OMX.Models;
using OMX.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMX.Common.Property.BindingModels
{
    public class PropertyBindingModel
    {   
     
        public int Id { get; set; }

        [Required]
        [Range(typeof(decimal), ConstantValues.MinimumPriceValue, ConstantValues.MaximumPriceValue)]
        public decimal Price { get; set; }
        [Required (ErrorMessage = ConstantValues.TitleErrorMessage)]
        [MaxLength(ConstantValues.MaximumTitleLength)]
        [MinLength(ConstantValues.MinimumTitleLength)]
        public string Title { get; set; }
        public int AddressId { get; set; }        
        [Required]
        public int NumberOfBedrooms { get; set; }
        [Required]
        public int NumberOfBathrooms { get; set; }
        public CurrencyTypes Currency { get; set; }

        public List<SelectListItem> Rooms { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "1" },
            new SelectListItem { Value = "2", Text = "2" },
            new SelectListItem { Value = "3", Text = "3"  },
            new SelectListItem { Value = "4", Text = "4"  },
            new SelectListItem { Value = "5", Text = "5"  },
        };

        public List<SelectListItem> PropertyTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "Sell" },            
            new SelectListItem { Value = "2", Text = "Buy"  },
          
        };
        public List<SelectListItem> CurrencyTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "BGN" },
            new SelectListItem { Value = "1", Text = "USD" },
            new SelectListItem { Value = "2", Text = "EUR"  },

        };
        [Required]
        public Dictionary<int, string> Features { get; set; } = new Dictionary<int, string>();
        public List<Address> Addresses { get; set; } = new List<Address>();
        public bool IsFeatured { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public List<int> SelectedFeatures { get; set; } = new List<int>();
        public PropertyType PropertyType { get; set; }
        [IgnoreMap]
        [DataType(DataType.Upload)]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public string UserId { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage = ConstantValues.DescriptionErrorMessage)]
        [MaxLength(ConstantValues.MaximumDescriptionLength)]
        [MinLength(ConstantValues.MinimumDescriptionLength)]
        public string Description { get; set; }       
        public double? IndoorArea { get; set; }
        public double? OutdoorArea { get; set; }
        public double? LandPlotSize { get; set; }
        public int? Floor { get; set; }
        public ICollection<Image> ImageNames { get; set; } = new List<Image>();
        public DateTime PostedOn { get; set; } = DateTime.UtcNow;
    }
}
