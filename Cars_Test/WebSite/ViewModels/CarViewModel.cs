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

        public ManufacturerEnum Manufacturer { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public int? Year { get; set; }

        public double Price { get; set; }
    }

}
