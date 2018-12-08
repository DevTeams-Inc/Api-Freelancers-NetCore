using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Rating
    {
        public int Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set;}
        [Required]
        public int FreelancerId { get; set; }
        [Required]
        public int Rate { get; set; }
        public string Comment { get; set;}
        public DateTime CreatedAt { get; set; }

        public Freelancer Freelancer { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
