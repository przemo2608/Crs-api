using Crs.Authorization;
using Crs.Model.View;
using Crs.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICar carService;

        public CarsController(ICar carService)
        {   
            this.carService = carService; 
        }

        [HttpPost]
        [Route("createCar")]
        public IActionResult CreateCar(CreateCarModel createCarModel)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            carService.CreateCar(createCarModel.userId, createCarModel.CarBrand, createCarModel.Model, createCarModel.RegistrationNumber);
            return new OkResult();

        }
        [CustomerRole]
        [HttpDelete]
        [Route("deleteCar")]
        public IActionResult DeleteCar(DeleteCarModel deleteCarModel)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            carService.DeleteCar(deleteCarModel.userId, deleteCarModel.carId);

            return new OkResult();
        }
        [CustomerRole]
        [HttpGet]
        [Route("getCars/{userid}")]
        public IEnumerable<CarModel> GetCars(int userid)
        {
            var cars = carService.GetCars(userid);

            return cars.Select(x => new CarModel()
            {
                Id = x.CarId,
                CarBrand = x.CarBrand,
                Model = x.Model,
                RegistrationNumber = x.RegistrationNumber
            });
        }
        
        [HttpGet]
        [Route("getAllCars")]
        public IEnumerable<CarModel> GetAllCars()
        {
            var cars = carService.GetAllCars();

            return cars.Select(x => new CarModel()
            {
                Id = x.CarId,
                CarBrand = x.CarBrand,
                Model = x.Model,
                RegistrationNumber = x.RegistrationNumber,
                UserId = x.UserId
            });
        }
    }
}
