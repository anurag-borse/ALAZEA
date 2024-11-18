using System.ComponentModel.DataAnnotations;

namespace ALAZEA.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(15)]
        public string? ContactInfo { get; set; }



        public virtual ICollection<Order>? Orders { get; set; } 

        public virtual ICollection<Review>? Reviews { get; set; }  

    }
}
