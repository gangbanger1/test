using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    [Table("Categories")]
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(200)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Размер")]
        public int SizeId { get; set; }
        public SizeModel Size { get; set; }

        [Display(Name = "Цвет")]
        public int ColorId { get; set; }
        public ColorModel Color { get; set; }

    }
}
