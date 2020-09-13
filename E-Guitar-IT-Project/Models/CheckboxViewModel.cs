using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Guitar_IT_Project.Models
{
    public class CheckboxViewModel
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set;}
        public bool Checked { get; set; }
           
    }
}