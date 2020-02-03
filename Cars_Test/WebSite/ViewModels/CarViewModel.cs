using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string Model { get; set; }

        [Display(Name = "Image URL")]
        [RegularExpression("(https|http)://(.+)(.jpg|.png)$", ErrorMessage = "Image url not correct")]
        public string ImageUrl { get; set; }

        [Required]
        [CorrectYear]
        public int? Year { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }

}
