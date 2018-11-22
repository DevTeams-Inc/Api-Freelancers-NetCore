using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    //Clase para registrar al usuario
    public class UserVm
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }        
    }
}
