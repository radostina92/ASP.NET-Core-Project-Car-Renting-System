using CarRentingSystem.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;

namespace CarRentingSystem.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRentalDbContext data;

        public CarsController(CarRentalDbContext data) => this.data = data;


        public IActionResult Add() => View(new AddCarFormModel
        {
            Categories = this.GetCarCategories()
        });

        public IActionResult All()
        {
            var cars = this.data
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(
                c => new CarListingViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Year = c.Year,
                    Category = c.Category.Name

                })
                .ToList();

            return View(cars);
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
