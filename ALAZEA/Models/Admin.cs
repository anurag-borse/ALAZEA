using System.ComponentModel.DataAnnotations;

namespace ALAZEA.Models
{
    public class Admin
    {
        [Key]
        public Guid AdminID { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
