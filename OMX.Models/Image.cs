using System.ComponentModel.DataAnnotations;

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
