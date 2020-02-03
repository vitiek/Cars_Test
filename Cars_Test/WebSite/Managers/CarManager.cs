using Abp.Linq.Extensions;
using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Extensions;
using WebSite.ViewModels;
using static WebSite.Extensions.DataTableParamHelper;

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

        public DatatableResultViewModel<CarViewModel> GetCars(DataTableParamModel param)
        {
            var result = new DatatableResultViewModel<CarViewModel>();

            var cars = carService.GetAll();

            //Get search values
            var YearSearch = GetSearchValueInt(nameof(Car.Year), param);
            var ManufacturerSearch = GetSearchValueString(nameof(Car.Manufacturer), param);

            //Apply search
            cars = cars
                .WhereIf(YearSearch != null, c => c.Year.Year == YearSearch.Value)
                .WhereIf(ManufacturerSearch != null, c => c.Manufacturer == (ManufacturerEnum)Enum.Parse(typeof(ManufacturerEnum), ManufacturerSearch));

            cars = OrderBys(cars, typeof(Car), param);

            result.RecordsFiltered = cars.Count();

            cars = cars
                .Skip(param.Start)
                .Take(param.Length);

            result.Result = mapper.Map<List<Car>, List<CarViewModel>>(cars.ToList());

            return result;
        }

        public CarViewModel GetCarById(int id)
        {
            var car = carService.GetById(id);

            return mapper.Map<Car, CarViewModel>(car);
        }

        public int GetCarCount()
        {
            return carService.GetAll().Count();
        }

        public void DeleteCar(int id)
        {
            carService.Delete(c => c.Id == id);
        }

        public void CreateRandomCars()
        {
            List<Car> cars = new List<Car>();

            for (int i = 0; i < 50; i++)
            {
                Random rnd = new Random();

                cars.Add(new Car()
                {
                    Manufacturer = (ManufacturerEnum)rnd.Next(0, 2),
                    Model = "Model-" + rnd.Next(0, 999),
                    Price = rnd.Next(1, 10000),
                    Year = new DateTime(rnd.Next(1990, 2020), 1, 1)
                });
            }

            carService.AddMany(cars);
        }
    }

    public interface ICarManager
    {
        void CreateCar(CarViewModel car);
        void UpdateCar(CarViewModel car);
        List<CarViewModel> GetAllCars();
        DatatableResultViewModel<CarViewModel> GetCars(DataTableParamModel param);
        CarViewModel GetCarById(int id);
        int GetCarCount();
        void DeleteCar(int id);
        void CreateRandomCars();
    }
}
