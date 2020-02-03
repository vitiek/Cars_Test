using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using WebSite.Extensions;
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

        public IActionResult CarReview()
        {

            return View();
        }

        [HttpPost]
        public IActionResult GetCarsDataTable([FromBody]DataTableParamModel param)
        {
            var cars = carManager.GetCars(param);

            return Json(new
            {
                draw = param.Draw++,
                recordsTotal = cars.RecordsFiltered,
                recordsFiltered = cars.RecordsFiltered,
                data = cars.Result
            });
        }

        [HttpGet]
        public IActionResult CreateUpdateCar(int id)
        {
            var car = new CarViewModel();

            if (id != 0)
            {
                car = carManager.GetCarById(id);
            }

            return View(car);
        }

        [HttpPost]
        public IActionResult CreateUpdateCar(CarViewModel car)
        {

            if (!ModelState.IsValid)
            {
                return View("CreateUpdateCar", car);
            }

            if (car.Id == 0)
            {
                carManager.CreateCar(car);
            }
            else
            {
                carManager.UpdateCar(car);
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult DeleteCar(int id)
        {
            carManager.DeleteCar(id);

            return Json(new { success = true });
        }

        public IActionResult ConfirmDeleteCar(int id)
        {
            var car = carManager.GetCarById(id);

            return View(car);
        }

        [HttpPost]
        public IActionResult CreateRandomCars()
        {
            carManager.CreateRandomCars();

            return Json(new { success = true });
        }

    }
}