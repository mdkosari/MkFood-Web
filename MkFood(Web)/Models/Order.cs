using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFood.DB.Models
{
    public class Order
    {

        public int OrderId { get; set; }

        public int OrderNumber { get; set; }

        public int FoodId { get; set; }
        [ForeignKey("FoodId")]
        public Food? Food { get; set; }
        public int Count { get; set; }
        public double Cost { get; set; }

        public int BillId { get; set; }
        [ForeignKey("BillId")]
        public Bill Bill { get; set; }
       
    }
}
