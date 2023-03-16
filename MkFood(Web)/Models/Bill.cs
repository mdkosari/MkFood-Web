namespace MKFood.DB.Models
{
    public class Bill
    {
        public int BillId { get; set; }


        public double Fee { get; set; }
        public double Tax { get; set; }

        public Double SumCost { get; set; }

        public bool Success { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<Order> orders { get; } = new List<Order>();

    }
}
