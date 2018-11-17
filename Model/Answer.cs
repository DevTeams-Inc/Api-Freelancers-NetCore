using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Answer
    {
        public int Id { get; set; }
        [Required]
        public int ProposalId { get; set; }
        [Required]
        [StringLength(255)]
        public string Message { get; set; }

        //faltan answers

    }
}
