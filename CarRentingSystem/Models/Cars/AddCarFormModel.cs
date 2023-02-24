using System.ComponentModel.DataAnnotations;
using CarRentingSystem.Data;


namespace CarRentingSystem.Models.Cars
{
    public class AddCarFormModel
    {
        [Required]
        [StringLength(DataConstants.CarBrandMaxLength, MinimumLength = DataConstants.CarBrandMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(DataConstants.CarModelMaxLength, MinimumLength = DataConstants.CarModelMinLength)]
        public string Model { get; init; }

        [Required]
        [StringLength(DataConstants.CarDescriptionMaxLength, MinimumLength = DataConstants.CarDescriptionMinLength)]
        public string Description { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Range(DataConstants.CarYearMinValue, DataConstants.CarYearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryViewModel>? Categories { get; set; }
    }
}
