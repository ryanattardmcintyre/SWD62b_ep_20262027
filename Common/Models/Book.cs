using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Book
    {
        [Key]
        [StringLength(16)]
        public string Isbn { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public int Volume { get; set; }

        [ForeignKey("Category")] //<= you specify the name of the corresponding nav. prop.
        public int CategoryFK { get; set; } //foreign key
        public virtual Category Category { get; set; } //navigational property

        //from mydb.Books in b
        //select b;

        //foreach(var book in listOfBooks)
        //{
        //    <tr><td>book.Title</td><td>book.Category.Name</td>
        //}

        public string Description { get; set; }
        public bool Available { get; set; }

        public string? Cover { get; set; }

    }
}
