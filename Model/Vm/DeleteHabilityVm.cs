using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Vm
{
    public class DeleteHabilityVm
    {
        [Required]
        public int Freelancer { get; set; }
        [Required]

        public int Hability { get; set; }
    }
}
