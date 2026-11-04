using HireUp.BLL.Services;
using HireUp.DAL.Models;
using HireUp.DAL.Models.Auth;
using HireUp.PL.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HireUp.PL.Controllers.Auth
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICategoryRepository _categoryRepository;

        public AccountController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager, ICategoryRepository categoryRepository)
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _categoryRepository = categoryRepository;
        }

        #region Selection Role
        public IActionResult SelectionRole()
        {
            return View("SelectionRole");
        }
        [HttpPost]
        public IActionResult SelectionRole(string role)
        {
            if(role == "JobSeeker")
            {
                return RedirectToAction(nameof(RegisterJobSeeker));
            }
            else
            {
                return RedirectToAction(nameof(RegisterCompany));
            }
        }
        #endregion

        #region Register
        public async Task<IActionResult> RegisterJobSeeker()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterJobSeeker(RegisterJobSeekerViewModel JobSeekerVM)
        {
            if (ModelState.IsValid)
            {
                var jobSeeker = new JobSeeker()
                {
                    UserName = JobSeekerVM.Email.Split('@')[0],
                    NationalNumber = JobSeekerVM.NationalNumber,
                    FirstName = JobSeekerVM.FirstName,
                    LastName = JobSeekerVM.LastName,
                    Email = JobSeekerVM.Email,
                    EmailConfirmed = true,               // Just at this time until Impelemnt the ConfirmedAccount 
                    Country = JobSeekerVM.Country,
                    Location = JobSeekerVM.Location,
                };
                var result = await _userManger.CreateAsync(jobSeeker, JobSeekerVM.Password);
                if (result.Succeeded)
                {
                    await _userManger.AddToRoleAsync(jobSeeker, "JobSeeker");
                    return RedirectToAction(nameof(Login));
                }
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(JobSeekerVM);
        }

        public async Task<IActionResult> RegisterCompany()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterCompany(RegisterCompanyViewModel CompanyVM)
        {
            if (ModelState.IsValid)
            {
                if (CompanyVM.CategoryId != null)
                {
                    var category = _categoryRepository.GetById(CompanyVM.CategoryId.Value);
                    CompanyVM.Industry = category.Name;
                }
                var company = new Company()
                {
                    UserName = CompanyVM.Email.Split('@')[0],
                    Name = CompanyVM.Name,
                    Industry = CompanyVM.Industry,
                    Email = CompanyVM.Email,
                    EmailConfirmed = true,               // Just at this time until Impelemnt the ConfirmedAccount 
                    Country = CompanyVM.Country,
                    Location = CompanyVM.Location,
                };
                var result = await _userManger.CreateAsync(company, CompanyVM.Password);
                if (result.Succeeded)
                {
                    await _userManger.AddToRoleAsync(company, "Company");
                    return RedirectToAction(nameof(Login));
                }
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(CompanyVM);
        }
        #endregion

        #region Login
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManger.FindByEmailAsync(userVM.Email);
                if(user is not null)
                {
                    var flag = await _userManger.CheckPasswordAsync(user, userVM.Password);
                    if (flag)
                    {
                        await _signInManager.PasswordSignInAsync(user, userVM.Password, userVM.RememberMe, false);
                        return RedirectToAction(nameof(Index), "Home");
                    }
                    ModelState.AddModelError(nameof(userVM.Password), "Invalid Password");
                    userVM.Password = string.Empty;
                    return View(userVM);
                }
                return RedirectToAction(nameof(SelectionRole));
            }
            return View(userVM);
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()   
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}
