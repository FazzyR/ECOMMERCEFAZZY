using ECOMMERCEFAZZY.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECOMMERCEFAZZY.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> UserManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> SignInManager)
        {

            _userManager = UserManager;
            _roleManager = roleManager;
            _signInManager = SignInManager;
        }


        public IActionResult LoginUser(string? returnUrl = null)
        {

            LoginUserViewModel loginUserViewModel = new LoginUserViewModel();
            loginUserViewModel.ReturnUrl = returnUrl;

            return View(loginUserViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> LoginUser(LoginUserViewModel loginUserViewModel, string? returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("~/");


            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(loginUserViewModel.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginUserViewModel.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return View();
                    }
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Account is locked out.");
                        return View("Lockout");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }


            }



            return View(loginUserViewModel);
        }




        public IActionResult CreateUser(string? returnUrl = null)
        {

            CreateUserViewModel createUserViewModel = new CreateUserViewModel();
            createUserViewModel.ReturnUrl = returnUrl;

            return View(createUserViewModel);
        }



        [HttpPost]


        public async Task<IActionResult> CreateUser(CreateUserViewModel createUserViewModel, string? returnUrl = null)
        {

            createUserViewModel.ReturnUrl = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {


                var exist =_userManager.Users.FirstOrDefault(u=>u.Email==createUserViewModel.Email);
                if (exist == null)
                {
                    var user = new IdentityUser
                    {

                        Email = createUserViewModel.Email,
                        UserName = createUserViewModel.UserName,



                    };
                    var result = await _userManager.CreateAsync(user, createUserViewModel.Password);

                    if (result.Succeeded)
                    {

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return LocalRedirect(returnUrl);

                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(createUserViewModel);

                }
                else
                {
                    return NotFound();
                }

               

               

            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
