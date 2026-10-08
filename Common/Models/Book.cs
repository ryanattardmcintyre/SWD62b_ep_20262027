using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Book
    {
        [Key]
        public string Isbn { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public int Volume { get; set; }
        public int CategoryFK { get; set; }
        public Category Category { get; set; }

        public string Description { get; set; }
        public bool Available { get; set; }

    }
}
