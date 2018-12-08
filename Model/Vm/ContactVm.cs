using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Vm
{
    public class ContactVm
    {
        public int Id { get; set; }
        [Required]
        public string EmailDestiny { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string EmailFrom { get; set; }

        public string FromId { get; set; }
        public int FreelancerId { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
