using ECOMMERCEFAZZY.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ECOMMERCEFAZZY.ViewModels
{
    public class CreateProductModelView
    {

        [Required]
        public string Name { get; set; } = string.Empty;


    
        public int SelectedCategoryId { get; set; } 
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }



        public int StockQuantity { get; set; } 


    }
}
