using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Hability
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [StringLength(150)]
        [Required]
        public string Image { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public FreelancerHability FreelancerHability { get; set; }

    }
}
