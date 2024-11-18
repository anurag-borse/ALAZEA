using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ALAZEA.Models
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; }  // Example: "Paid", "Pending", "Failed"

        // Navigation property to link User
        public virtual User User { get; set; }

        // Navigation property to link plants via a join table
        public virtual ICollection<OrderPlant> OrderPlants { get; set; }  // Each Order can have many OrderPlant associations
    }
}
