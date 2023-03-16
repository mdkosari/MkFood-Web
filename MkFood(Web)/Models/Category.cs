using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKFood.DB.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(40)]
        public string Name { get; set; }

        public ICollection<Food> Foods { get; } = new List<Food>();

    }
}
