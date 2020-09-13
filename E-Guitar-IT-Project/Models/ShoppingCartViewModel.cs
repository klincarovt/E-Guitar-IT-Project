using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Guitar_IT_Project.Models
{
    public class ShoppingCartViewModel
    {
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public Guitar guitar { get; set; }
        public ShoppingCartViewModel()
        {
            CartItems = new List<CartItem>();
        }
    }
}