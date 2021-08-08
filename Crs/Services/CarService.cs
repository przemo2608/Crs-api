using Crs.Data;
using Crs.Exceptions;
using Crs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Services
{
    public class CarService : ICar
    {
        private readonly ICrsContext db;


        public CarService(ICrsContext db)
        {

            this.db = db;
        }

        public void CreateCar(int userId, string carBrand, string model, string registrationNumber)
        {
            var car = db.Cars.FirstOrDefault(x => x.RegistrationNumber == registrationNumber);
            if (car != null)
            {
                throw new Exception("Istnieje taki samochód z tym numerem rejestracyjnym");
            }
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                throw new UserNotFoundException();
            

             car = new CarDb { 
                 UserId = user.Id,
            CarBrand = carBrand,
            Model = model,
            RegistrationNumber = registrationNumber,
            };
            db.Cars.Add(car);
            db.SaveChanges();

            var carFromDb = db.Cars.FirstOrDefault(x => x.RegistrationNumber == registrationNumber);

            var userCar = new UserCarDb { 
            Car = carFromDb,
            CarId = carFromDb.Id,
            User = user,
            UserId = user.Id
            };
            db.UserCars.Add(userCar);
            db.SaveChanges();
        }

        public void DeleteCar(int userId, int carId)
        {
            var car = db.Cars.FirstOrDefault(x => x.Id == carId);
            if (car == null)
                throw new ObjectNotFoundException();

            var userCar = db.UserCars.FirstOrDefault(x => x.UserId == userId && x.CarId == carId);
            if (userCar == null)
                throw new ObjectNotFoundException();

            db.Cars.Remove(car);
            db.UserCars.Remove(userCar);
            db.SaveChanges();
        }

        public IEnumerable<CarDto> GetAllCars()
        {
            var cars = db.Cars.ToList();
            if (cars == null)
                throw new ObjectNotFoundException();



            return cars.Select(x => new CarDto()
            {
               CarId = x.Id,
               CarBrand = x.CarBrand,
               Model = x.Model,
               RegistrationNumber = x.RegistrationNumber,
               UserId = x.UserId
               
            });
        
        }

        public IEnumerable<CarDto> GetCars(int userId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                throw new UserNotFoundException();

            var userCarsIds = db.UserCars.Where(x => x.UserId == userId);
            if (userCarsIds == null || userCarsIds.Count() == 0)
                return new List<CarDto>();

            var userCars = db.Cars.Where(x => userCarsIds.Any(y => y.CarId == x.Id)).ToList();
            return userCars.Select(x => new CarDto()
            {
                CarId = x.Id,
                CarBrand = x.CarBrand,
                Model = x.Model,
                RegistrationNumber = x.RegistrationNumber
            });
        }
    }
}
