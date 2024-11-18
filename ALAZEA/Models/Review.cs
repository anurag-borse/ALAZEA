using System.ComponentModel.DataAnnotations;

namespace ALAZEA.Models
{
    public class Review
    {
        [Key]
        public Guid ReviewID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public Guid PlantID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Feedback { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public virtual User User { get; set; }  // Navigation property to link User
        public virtual Plant Plant { get; set; }  // Navigation property to link Plant
    }
}
