namespace MkFood_Web_.Models
{
    public class FoodCreateView
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }



        public int CategoryId { get; set; }
    }
}
