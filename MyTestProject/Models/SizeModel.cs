using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    [Table("Sizes")]
    public class SizeModel
    {
        [Key]
        public int SizeId { get; set; }

        [StringLength(200)]
        [Display(Name = "Размер")]
        public string Size { get; set; }

    }
}

