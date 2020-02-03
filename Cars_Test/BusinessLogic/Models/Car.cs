using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class Car
    {
        public int Id { get; set; }

        public ManufacturerEnum Manufacturer { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }

        public DateTime Year { get; set; }

        public double Price { get; set; }
    }

    public enum ManufacturerEnum
    {
        Bmw,
        Mercedes,
        Audi
    }
}
