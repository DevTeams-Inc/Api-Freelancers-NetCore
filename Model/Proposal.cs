using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Proposal
    {
        public int Id { get; set; }
        public int ProyectId { get; set; }  
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        public decimal Offer { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        [ForeignKey("ProyectId")]
        public Proyect Proyect { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<Answer> Answers { get; set; }

    }
}
