using System.ComponentModel.DataAnnotations.Schema;

namespace ECOMMERCEFAZZY.Models
{
    public class Product
    {
        public int Id { get; set; }


        public string Name { get; set; } = string.Empty;

        
        public int CategoryId { get; set; }
      
        public Category Category { get; set; } = new Category();
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; } = 0;
    }
}
