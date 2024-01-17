using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.ViewModels.AuthVMs;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        UserManager<AppUser> _userManager { get; }
        SignInManager<AppUser> _signInManager { get; }
        RoleManager<IdentityRole> _roleManager { get; }
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            AppUser user = new AppUser
            {
                Fullname = vm.FullName,
                Email = vm.Email,
                UserName = vm.UserName
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded) 
            {
                return View(vm);
            }
            var roleResult =await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            if (!roleResult.Succeeded)
            {
                ModelState.AddModelError("", "something went wrong");
                return View(vm);

            }
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user;
            if (vm.UserNameOrEmail.Contains("@"))
            {
                user=await _userManager.FindByEmailAsync(vm.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(vm.UserNameOrEmail);
            }
            if (user == null)
            {
                return View(vm);
            }
            var result =await _signInManager.CheckPasswordSignInAsync(user, vm.Password,true);
            if (!result.Succeeded)
            {
                return View(vm);
            }

            await _signInManager.SignInAsync(user,true);
            return RedirectToAction(nameof(Index),"Home");
        }
        public async Task<bool> CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    var result=await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = item.ToString()
                    });
                    if (!result.Succeeded)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        public async  Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login), "Auth");
        }
    }
}
