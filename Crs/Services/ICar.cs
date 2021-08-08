using Crs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Services
{
    public interface ICar
    {
        void CreateCar(int userId, string carBrand, string model, string registrationNumber);
        void DeleteCar(int userId, int carId);
        IEnumerable<CarDto> GetCars(int userId);
        IEnumerable<CarDto> GetAllCars();

    }
}
