using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Reservation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Book")]
        public string BookFK { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime DateTaken { get; set; }
        public DateTime? DateReturned { get; set; }

    }
}
