using System;
using System.Collections.Generic;
using System.Text;

namespace OMX.Models
{
   public  class Feature
    {
        public Feature()
        {
            this.Properties = new List<PropertyFeature>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PropertyFeature> Properties { get; set; } 

    }
}
