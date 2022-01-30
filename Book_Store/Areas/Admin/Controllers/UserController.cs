using Book_Store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        UserManager<ApplicationUser> UserManage;
        SignInManager<ApplicationUser> Sign;
        public UserController(UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> SignManager)
        {
            UserManage = UserManager;
            Sign = SignManager;
        }
        public IActionResult Login(string ReturnUrl)
        {
            return View(new UserModel()
            {
                ReturnUrl = ReturnUrl
            }); ;
        }
        public async Task<IActionResult> LogOut()
        {
            await Sign.SignOutAsync();
            return Redirect("/Admin/Home/Dashbord");
        }
        [HttpPost]
        public async Task<IActionResult> Loging(UserModel model)
        {
            var result = await Sign.PasswordSignInAsync(model.Email, model.Pass, true, false);
            if (string.IsNullOrEmpty(model.ReturnUrl))
                model.ReturnUrl = "/Admin/Home/Dashbord";
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }
            else
            {
                return View("Login", model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserModel Model)
        {
            var user = new ApplicationUser()
            {
                UserName = Model.Email,
                Email = Model.Email,
                Pass=Model.Pass
            };
            if (string.IsNullOrEmpty(Model.ReturnUrl))
                Model.ReturnUrl = "/Admin/User/Login";
            var result = await UserManage.CreateAsync(user,Model.Pass);
            if (result.Succeeded)
            {
                return Redirect(Model.ReturnUrl);
            }
            else
            {
                return View("Login", Model);
            }

        }
    }
}
