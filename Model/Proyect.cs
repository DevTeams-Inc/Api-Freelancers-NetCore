using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Proyect
    {
        public int Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        [StringLength(40)]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(100)]
        public string Required_Skill { get; set; }
        [Required]
        public string Scope { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Proposal> Proposal { get; set; }

    }
}
