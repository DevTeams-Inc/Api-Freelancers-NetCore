using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Rating
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set;}
        public int FreelancerId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set;}
        public DateTime CreatedAt { get; set; }
        public Freelancer Freelancer { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
