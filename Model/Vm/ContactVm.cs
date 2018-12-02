using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Vm
{
    public class ContactVm
    {
        [Required]
        public string EmailDestiny { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string EmailFrom { get; set; }
    }
}
