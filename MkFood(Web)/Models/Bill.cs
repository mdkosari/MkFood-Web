using System.ComponentModel.DataAnnotations;

namespace MKFood.DB.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }

        public double? Fee { get; set; }
        public double? Tax { get; set; }

        public double? Cost { get; set; }

        public bool Success { get; set; } = false;

        public bool Open { get; set; } = true;

        public DateTime? Created { get; set; } = DateTime.Now;

        public Customer? Customer { get; set; }
        public ICollection<Order> orders { get; } = new List<Order>();

    }
}
