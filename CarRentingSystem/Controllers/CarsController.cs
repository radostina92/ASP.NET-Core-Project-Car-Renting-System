using CarRentingSystem.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;
using CarRentingSystem.Services.Cars;

namespace CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {

        private readonly ICarService cars;
        private readonly CarRentalDbContext data;

        public CarsController(CarRentalDbContext data, ICarService cars)
        {
            this.data = data;
            this.cars = cars;
        }

        public IActionResult Add() => View(new AddCarFormModel
        {
            Categories = this.GetCarCategories()
        });

        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
           var queryResult = this.cars.All(query.BrandSelcted, query.SearchTerm, query.CarsSorting);

            var carBrands = this.cars.AllCarBrands();

           query.Cars = queryResult.Cars;

            return View(query);
        }

        [HttpPost]
        public IActionResult Add(AddCarFormModel carFormModel)
        {


            if(!this.data.Categories.Any(c => c.Id == carFormModel.CategoryId))
            {
                this.ModelState.AddModelError(nameof(carFormModel.CategoryId), "Category does not exist!");
            }



            if (!ModelState.IsValid)
            {
                carFormModel.Categories = this.GetCarCategories();
                return View(carFormModel);
            }



            var car = new Car
            {
                Brand = carFormModel.Brand,
                Model = carFormModel.Model,
                Description = carFormModel.Description,
                ImageUrl = carFormModel.ImageUrl,
                Year = carFormModel.Year,
                CategoryId = carFormModel.CategoryId
            };

            this.data.Cars.Add(car);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        } 

        private IEnumerable<CarCategoryViewModel> GetCarCategories()
        => this.data
            .Categories
            .Select(c => new CarCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }
}
