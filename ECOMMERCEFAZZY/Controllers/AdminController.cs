using ECOMMERCEFAZZY.Data;
using ECOMMERCEFAZZY.Models;
using ECOMMERCEFAZZY.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;

namespace ECOMMERCEFAZZY.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(ApplicationDbContext Context, UserManager<IdentityUser> UserManager, RoleManager<IdentityRole> roleManager)
        {
            _context = Context;
            _userManager = UserManager;
            _roleManager = roleManager;
        }



        public IActionResult UsersDetails(string searchTerm)
        {

            var Users = _userManager.Users.ToList();


           


            return View(Users);
        }




        public IActionResult AdminPanel(string searchTerm)
        {

            var products = _context.Products.Include(P=>P.Category).AsQueryable();


            if (!string.IsNullOrEmpty(searchTerm)){

                products = products.Where(p => p.Name.Contains(searchTerm) ||
                p.Description.Contains(searchTerm) ||
                p.Category.Name.Contains(searchTerm)

                );
                ViewData["SearchTerm"] = searchTerm;
            }


            return View(products.ToList());
        }



        [HttpGet("Admin/ProductDetails/{ProductId}")]
        public IActionResult ProductDetails(int ProductId)
        {

           

           Product product = _context.Products.FirstOrDefault(x=>x.Id==ProductId);
            return View(product);
        }




        public IActionResult CreateProduct()
        {



            var categories = _context.Categories.ToList();
            CreateProductModelView createProductModelView = new CreateProductModelView
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList(),

            };
            return View(createProductModelView);
        }


        [HttpPost]

        public async Task<IActionResult> CreateProduct(CreateProductModelView createProductModelView)
        {

            if (ModelState.IsValid)
            {


                var category = _context.Categories.FirstOrDefault(x => x.CategoryId == createProductModelView.SelectedCategoryId);

                if (category != null)
                {
                    var product = new Product
                    {
                        Name = createProductModelView.Name,
                        Category = category,
                        Description = createProductModelView.Description,
                        Price = createProductModelView.Price,
                        StockQuantity = createProductModelView.StockQuantity,
                    };
                    category.Products.Add(product);
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();

                }










            }


            return RedirectToAction("AdminPanel");
        }


        public IActionResult CreateCategory()
        {

            CreateCategoryViewModel model = new CreateCategoryViewModel();


            return View(model);
        }


        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryViewModel model)
        {

            if (ModelState.IsValid)
            {

                var cate = _context.Categories.FirstOrDefault(x => x.Name == model.Name);

                if (cate == null)
                {
                    var category = new Category
                    {
                        Name = model.Name,
                    };
                    _context.Categories.Add(category);
                    _context.SaveChanges();

                    return RedirectToAction("AdminPanel");
                }

                return NotFound(cate?.Name);

            }


            return NotFound();
        }




        public async Task<IActionResult> EditProduct()
        {
            var categories = _context.Categories.ToList();
            EditProductViewModel editProductViewModel = new EditProductViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList(),

            };

            return View(editProductViewModel);
        }

        [HttpPost("Admin/EditProduct/{ProductId}")]
        public IActionResult EditProduct(EditProductViewModel editProductViewModel, [FromRoute] int ProductId)
        {

            if (ModelState.IsValid)
            {


                var product = _context.Products.FirstOrDefault(x => x.Id == ProductId);
                var category = _context.Categories.FirstOrDefault(x => x.CategoryId == editProductViewModel.SelectedCategoryId);
                if (product == null || category == null)
                {

                    return NotFound();

                }

                else
                {

                    product.Name = editProductViewModel.Name;
                    product.Category = category;
                    product.Description = editProductViewModel.Description;
                    product.Price = editProductViewModel.Price;
                    product.StockQuantity = editProductViewModel.StockQuantity;

                    _context.SaveChangesAsync();

                }



            }
            return RedirectToAction("AdminPanel");

        }


        [HttpGet("Admin/DeleteProduct/{ProductId}")]

        public IActionResult DeleteProduct(int ProductId)
        {
            

                var product = _context.Products.FirstOrDefault(x=>x.Id==ProductId);

                if (product == null) { 
              
                    return NotFound();
                }

                else
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();

                }

            
            

            return RedirectToAction("AdminPanel");


        }

    }
}
