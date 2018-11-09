using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Freelancer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Lenguaje { get; set; }
        [RegularExpression("^[0-9]+(\\,[0-9]{1,2})?$")]
        public decimal PriceHour { get; set; }
        [StringLength(255)]
        public string Biography { get; set; }
        [StringLength(100)]
        public string Interest { get; set; }
        public int Level { get; set; }
        [StringLength(255)]
        public string Historial { get; set; }
        public int Rating { get; set; }
        [StringLength(255)]
        public string Testimony { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public string ApplicationUserId { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }

        public FreelancerHability FreelancerHability { get; set; }

    }
}
