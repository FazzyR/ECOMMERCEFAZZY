using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECOMMERCEFAZZY.ViewModels
{
    public class EditProductViewModel
    {


        public string Name { get; set; } = string.Empty;

        public int SelectedCategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; } = 0;

    }
}
