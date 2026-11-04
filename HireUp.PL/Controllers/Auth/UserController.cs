using AutoMapper;
using HireUp.BLL.Services;
using HireUp.DAL.Models;
using HireUp.DAL.Models.Auth;
using HireUp.PL.ViewModels;
using HireUp.PL.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Data;

namespace HireUp.PL.Controllers.Auth
{

    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var usersList = new List<UserViewModel>();

                foreach (var U in _userManager.Users)
                {
                    var roles = await _userManager.GetRolesAsync(U);

                    usersList.Add(new UserViewModel()
                    {
                        Name = U.Email.Split('@')[0],
                        Email = U.Email,
                        Role = roles.FirstOrDefault() ?? "No Role",
                        Joined = U.CreatedAt
                    });
                }

                return View(usersList);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(SearchValue);
                var roles = await _userManager.GetRolesAsync(user);
                var mappedUser = new UserViewModel()
                {
                    Name = user.Email.Split('@')[0],
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "No Role",
                    Joined = user.CreatedAt
                };
                return View(new List<UserViewModel>() { mappedUser });
            }
        }
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "No Role";
            if(role == "JobSeeker")
            {
                return RedirectToAction("Profile", "JobSeeker", new { id });
            }
            else
            {
                return RedirectToAction("Profile", "Company", new { id });
            }
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
