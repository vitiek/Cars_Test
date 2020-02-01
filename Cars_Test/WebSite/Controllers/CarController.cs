using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using WebSite.Managers;
using WebSite.ViewModels;

namespace WebSite.Controllers
{
    public class CarController : Controller
    {

        private readonly ICarManager carManager;

        public CarController(ICarManager carManager)
        {
            this.carManager = carManager;
        }

        public IActionResult CarReview(ManufacturerEnum? manufacturer, int? year, int? page)
        {

            return View();
        }
        
    }
}