using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Guitar_IT_Project.Models
{
    public class ShoppingCart
    {
        [Key]
        public string UserId { get; set; }
        public virtual ICollection<CartItem> cartItems { get;set; } 
    }
}
