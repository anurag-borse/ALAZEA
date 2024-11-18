using System.ComponentModel.DataAnnotations;

namespace ALAZEA.Models
{
    public class OrderPlant
    {
        [Key]
        public Guid OrderPlantID { get; set; }

        // Foreign key to Order
        public Guid OrderID { get; set; }
        public virtual Order Order { get; set; }

        // Foreign key to Plant
        public Guid PlantID { get; set; }
        public virtual Plant Plant { get; set; }

        // Additional fields if needed (e.g., quantity, price at the time of order)
        public int Quantity { get; set; }
        public decimal PriceAtOrder { get; set; }  // Optionally store the price of the plant when ordered

    }
}
