using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace E_Guitar_IT_Project.Models
{
    public class Guitar
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Image URL:")]
        public string UrlImg { get; set; }


        [Required]
        [Display(Name= "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Range(1,1000000000)]
        public int Price { get; set; }

        [Required]
        [Display(Name="Samples")]
        [Range(1,99)]
        public int Samples { get; set; }

        public int CategoryID { get; set; }

         
  
        public virtual Category Category { get; set; }

        public virtual ICollection<GuitarToCraftsman> GuitarToCraftsman { get; set; }



    }
}