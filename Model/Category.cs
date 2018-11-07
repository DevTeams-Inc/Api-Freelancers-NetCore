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
        public int ProyectId { get; set; }
        [Required]
        [StringLength(100)]
        public string Area { get; set; }
        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }
        [StringLength(255)]
        public string Img { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
