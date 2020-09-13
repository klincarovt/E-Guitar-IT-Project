using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Guitar_IT_Project.Models
{
    public class CraftsmenViewModel
    {
        public int CrafrsmanID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }

        public string Address { get; set; }

        public List<CheckboxViewModel> Guitars { get;set; }
    }
}