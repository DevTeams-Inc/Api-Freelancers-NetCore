using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class FreelancerHability
    {
        public int Id { get; set; }
        [Required]
        public int FreelancerId { get; set; }
        [Required]
        public int HabilityId { get; set; }
    }
}
