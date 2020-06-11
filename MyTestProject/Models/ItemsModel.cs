using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace TestProject.Models
{
    [Table("Items")]
    public class ItemsModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Цена")]
        [PosNumberNoZero(ErrorMessage = "Число должно быть больше нуля")]
        public int Price { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }

    }
    public class PosNumberNoZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            int getal;
            if (int.TryParse(value.ToString(), out getal))
            {

                if (getal == 0)
                    return false;

                if (getal > 0)
                    return true;
            }
            return false;

        }
    }
}
