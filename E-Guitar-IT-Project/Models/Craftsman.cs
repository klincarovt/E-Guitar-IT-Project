using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Guitar_IT_Project.Models
{
    public class Craftsman
    {
        [Key]
        public int id { get; set; }
        
        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public virtual ICollection<GuitarToCraftsman> GuitarToCraftsman { get; set; }


    }
}