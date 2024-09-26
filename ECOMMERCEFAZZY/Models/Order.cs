namespace ECOMMERCEFAZZY.Models
{
    public class Order
    {

        public int Id { get; set; }


        public string UserEmail { get; set; }=string.Empty;

        public DateTime OrderDate { get; set; } 

        public decimal TotalAmount { get; set; } = 0;

        public List<OrderItem> OrderItems { get; set; }=new List<OrderItem>();
    }
}
