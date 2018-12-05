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
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Avatar { get; set; }
        public string Address { get; set; }
        [Required]
        public int Role { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdateAt { get; set; }
        
    }
}
