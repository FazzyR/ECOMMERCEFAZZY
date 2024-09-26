namespace ECOMMERCEFAZZY.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<CartItem> CartItems { get; set; }=new List<CartItem>();

        public string UserEmail { get; set; }=string.Empty;
    }
}
