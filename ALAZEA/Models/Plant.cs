using ALAZEA.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ALAZEA.Models
{
    public class Plant
    {
        [Key]
        public Guid PlantID { get; set; }

        [Required(ErrorMessage = "Plant name is required.")]
        [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name should contain characters only.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Category is required.")]
        public PlantCategory Category { get; set; }  // Use Enum here

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a numeric and positive value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Availability is required.")]
        public bool Availability { get; set; }

        // Navigation property to link Order via a join table
        public virtual ICollection<OrderPlant>? OrderPlants { get; set; }  // Each Plant can be in many Orders via OrderPlant


        public string Tags { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }  // Each Plant can have many Reviews



        public string? ImagePath { get; set; }
    }
}
