using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Vm
{
    public class ProyectVm
    {
        public int Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Required_Skill { get; set; }
        [Required]
        public string Scope { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Adress { get; set; }
        public DateTime CreatedAt { get; set; }
        public string NameCategory { get; set; }
        [Required]
        public int CategoryId { get; set; } 
        public IEnumerable<ProposalVm> Proposal { get; set; }
    }
}
