using ECOMMERCEFAZZY.Data;
using ECOMMERCEFAZZY.Models;
using ECOMMERCEFAZZY.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;

namespace ECOMMERCEFAZZY.Controllers
{
   
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(ApplicationDbContext Context, UserManager<IdentityUser> UserManager, RoleManager<IdentityRole> roleManager)
        {
            _context = Context;
            _userManager = UserManager;
            _roleManager = roleManager;
        }



        public IActionResult UserPanel(string searchTerm)
        {

            var products = _context.Products.Include(P => P.Category).AsQueryable();


            if (!string.IsNullOrEmpty(searchTerm))
            {

                products = products.Where(p => p.Name.Contains(searchTerm) ||
                p.Description.Contains(searchTerm) ||
                p.Category.Name.Contains(searchTerm)

                );
                ViewData["SearchTerm"] = searchTerm;
            }


            return View(products.ToList());
        }


        [HttpGet("User/UserProductDetails/{ProductId}")]
        public IActionResult UserProductDetails(int ProductId)
        {



            Product product = _context.Products.FirstOrDefault(x => x.Id == ProductId);
            return View(product);
        }


























    }
}
