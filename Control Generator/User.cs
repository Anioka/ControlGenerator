using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Control_Generator
{
    //objekat za mapiranje podataka o Useru
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string Mail { get; set; }
        public bool DataV { get; set; }

        public User() 
        {
            
        }
    }
}
 