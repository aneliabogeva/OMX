using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMX.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
