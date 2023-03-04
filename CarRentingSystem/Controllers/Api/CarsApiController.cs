using CarRentingSystem.Models.Api.Cars;
using CarRentingSystem.Services.Cars;
using CarRentingSystem.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Controllers.Api
{
    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly ICarService cars;

        public CarsApiController(ICarService cars)
        {
            this.cars = cars;
        }
/*
        [HttpGet]
        public IEnumerable<Car> GetCar()
        {
            return data.Cars.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDetails(int id)
        {
            var car = this.data.Cars.Find(id);
            if(car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetCars()
        {
            var cars = this.data.Cars.ToList();
            if (!cars.Any())
            {
                return NotFound();
            }
            return cars;
        }
*/
        [HttpGet]
        public CarQueryServiceModel All([FromQuery] AllCarsApiRequestModel query)
        {

            return this.cars.All(query.BrandSelcted,
                query.SearchTerm,
                query.CarsSorting);

            
        }

    }
}
