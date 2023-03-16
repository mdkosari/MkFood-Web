using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFood.DB.Models
{
    public class Order
    {

        public int OrderId { get; set; }

        public int OrderNumber { get; set; }

        public int FoodID { get; set; }
        public Food? Food { get; set; }
        public int Count { get; set; }
        public double Cost { get; set; }

        public int BillID { get; set; }
        public Bill? Bill { get; set; }
       
    }
}
