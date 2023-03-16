using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MKFood.DB.Models
{
    public class Food
    {

        public int FoodId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Picture { get; set; }



        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
       
        public ICollection<Price> Prices { get; } = new List<Price>();
    }
}
