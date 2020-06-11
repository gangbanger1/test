using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    [Table("Colors")]
    public class ColorModel
    {
        [Key]
        public int ColorId { get; set; }

        [StringLength(200)]
        [Display(Name = "Цвет")]
        public string Color { get; set; }

    }
}
