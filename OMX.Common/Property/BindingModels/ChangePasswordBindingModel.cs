using OMX.Models;
using System.ComponentModel.DataAnnotations;

namespace OMX.Common.Property.BindingModels
{
    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public User User { get; set; }
    }
}
