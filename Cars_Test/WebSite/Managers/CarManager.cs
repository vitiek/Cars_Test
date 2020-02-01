using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.ViewModels;

namespace WebSite.Managers
{
    public class CarManager : ICarManager
    {
        private const int PageSize = 10;

        private readonly ICarService carService;
        private readonly IMapper mapper;

        public CarManager(ICarService carService, IMapper mapper)
        {
            this.carService = carService;
            this.mapper = mapper;
        }

        public List<CarViewModel> Cars(ManufacturerEnum manufacturer, int year, int page)
        {
            int skip = (page - 1) * PageSize;

            var cars = carService.GetPage(skip, PageSize).ToList();

            return mapper.Map<List<Car>, List<CarViewModel>>(cars);

        }

        public void CreateCar(CarViewModel car)
        {
            var newCar = mapper.Map<CarViewModel, Car>(car);

            carService.Add(newCar);
        }

        public void UpdateCar(CarViewModel car)
        {
            var newCar = mapper.Map<CarViewModel, Car>(car);

            carService.Update(newCar);
        }

        public List<CarViewModel> GetAllCars()
        {
            return mapper.Map<List<Car>, List<CarViewModel>>(carService.GetAll().ToList());
        }
    }

    public interface ICarManager
    {
        List<CarViewModel> Cars(ManufacturerEnum manufacturer, int year, int page);
        void CreateCar(CarViewModel car);
        void UpdateCar(CarViewModel car);
        List<CarViewModel> GetAllCars();
    }
}
