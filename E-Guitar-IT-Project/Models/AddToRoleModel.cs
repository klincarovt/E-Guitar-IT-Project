using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Guitar_IT_Project.Models
{
    public class AddToRoleModel
    {

        public string Email { get; set; }
        public string selectedRole { get; set; }
        public List<string> Roles { get; set; }
        public AddToRoleModel()
        {
      
            Roles = new List<string>();
        }
    }
}