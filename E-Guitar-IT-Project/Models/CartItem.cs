using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Guitar_IT_Project.Models
{

    public class CartItem
    {
        [Key]
        public int id { get; set; }

        public string CartId { get; set; }
        public int GuitarId { get; set; }


        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }
        public virtual Guitar Guitar { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }

    }
    
}