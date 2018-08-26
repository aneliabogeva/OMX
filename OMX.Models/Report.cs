using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMX.Models
{
    public class Report
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsResolved { get; set; } = false;
        public User User { get; set; }
        public string UserId { get; set; }

    }
}
