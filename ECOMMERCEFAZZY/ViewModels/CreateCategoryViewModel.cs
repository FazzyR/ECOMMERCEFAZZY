using ECOMMERCEFAZZY.Models;
using System.ComponentModel.DataAnnotations;

namespace ECOMMERCEFAZZY.ViewModels
{
    public class CreateCategoryViewModel
    {

        [Required]
        public string Name { get; set; } = string.Empty;

       
    }
}
