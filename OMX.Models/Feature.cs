using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMX.Models
{
    public  class Feature
    {
        public Feature()
        {
            this.Properties = new List<PropertyFeature>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<PropertyFeature> Properties { get; set; } 

    }
}
