using System.ComponentModel.DataAnnotations;

namespace ALAZEA.Models
{
    public class Plant
    {
        [Key]
        public Guid PlantID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Required]
        public bool Availability { get; set; }

        // Navigation property to link Order via a join table
        public virtual ICollection<OrderPlant> OrderPlants { get; set; }  // Each Plant can be in many Orders via OrderPlant


        public string Tags { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }  // Each Plant can have many Reviews



        [Required]
        public string ImagePath { get; set; }
    }
}
