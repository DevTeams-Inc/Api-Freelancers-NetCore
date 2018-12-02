using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }
        [StringLength(255)]
        public string Img { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public IEnumerable<Hability> Habilities { get; set; }
        
    }
}
