using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    public class CarService : BaseService<Car>, ICarService
    {

        public CarService(IDataContext dataContext) : base(dataContext)
        {
        }
    }

    public interface ICarService : IService<Car>
    {

    }
}
