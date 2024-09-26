using System.ComponentModel.DataAnnotations;

namespace ECOMMERCEFAZZY.ViewModels
{
    public class CreateUserViewModel
    {


        [Required]
 
        [Display(Name = "UserName")]

        public string UserName { get; set; } = string.Empty;



        [Required]
        [EmailAddress]
        [Display(Name ="Email")]

        public string Email { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; } = string.Empty;


        public string ReturnUrl { get; set; }=string.Empty;
    }
}
