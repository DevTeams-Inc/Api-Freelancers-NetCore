using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(35)]
        public string LastName { get; set; }
    }
}
