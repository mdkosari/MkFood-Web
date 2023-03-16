using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFood.DB.Models
{
    public class Price
    {
        public int PriceId { get; set; }
        public double Cost { get; init; }
        public bool Expierd { get; set; } = false;

        public int FoodId { get; set; }
        [ForeignKey("FoodId")]
        public Food Food { get; set; }

    }
}
