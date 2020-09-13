using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Guitar_IT_Project.Models
{
    public class GuitarToCraftsman
    {
      
        [Key]
        public int GuitarToCraftsmanID { get; set; }
       
        public int GuitarID { get; set; }
        public int CraftsmanID { get; set; }

        public virtual Guitar Guitar { get; set; }
        public virtual Craftsman Craftsman { get; set; }
    }
}