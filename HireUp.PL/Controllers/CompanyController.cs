using AutoMapper;
using HireUp.BLL.Repositories;
using HireUp.BLL.Services;
using HireUp.DAL.Models;
using HireUp.DAL.Models.Auth;
using HireUp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HireUp.PL.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
      
        #region CRUD
        public IActionResult Index(int? categoryId, string? SearchValue)
        {
            List<Company> comps = new List<Company>();
     
            if (!string.IsNullOrEmpty(SearchValue))
            {
                SearchValue = SearchValue.Trim();

                comps = _companyRepository.GetAll()
                    .Where(c =>
                        c.Name.Contains(SearchValue, StringComparison.OrdinalIgnoreCase) ||
                        c.Description.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                ViewBag.Flage = comps.Count();
            }

            if (categoryId.HasValue)
                comps = _companyRepository.GetByCategory(categoryId.Value);
            else
                comps = _companyRepository.GetAll();

            var mappedComps = _mapper.Map<List<Company>, List<CompanyViewModel>>(comps);
            ViewBag.CategoryId = categoryId;
            return View(mappedComps);
        }

        //[Authorize(Roles = "Company,Admin")]
        public IActionResult Profile(string id)
        {
            var company = _companyRepository.GetById(id);
            var mappedCompany = _mapper.Map<Company, CompanyViewModel>(company);
            return View(mappedCompany);
        }
        [Authorize(Roles = "Company")]
        public IActionResult UpdateProfile(string id)
        {
            var oldCompany = _companyRepository.GetById(id);
            if (oldCompany is null)
            {
                return BadRequest();
            }
            var mappedCompany = _mapper.Map<Company, CompanyViewModel>(oldCompany);
            return View(mappedCompany);
        }
        [Authorize(Roles = "Company")]
        [HttpPost]
        public IActionResult UpdateProfile(CompanyViewModel CompanyVM)
        {
            if (ModelState.IsValid)
            {
                var mappedCompany = _mapper.Map<CompanyViewModel, Company>(CompanyVM);
                _companyRepository.Update(mappedCompany);
                _companyRepository.Save();
                return RedirectToAction("Index", "Home");   //// Hint we need inhance that
            }
            return View(CompanyVM);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)   /// Admin Role
        {
            var Company = _companyRepository.GetById(id);
            if (Company is null)
            {
                return BadRequest();
            }
            var mappedJop = _mapper.Map<Company, CompanyViewModel>(Company);
            return View(mappedJop);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(CompanyViewModel CompanyVM)   /// Admin Role
        {
            if (ModelState.IsValid)
            {
                var mappedCompany = _mapper.Map<CompanyViewModel, Company>(CompanyVM);
                _companyRepository.Delete(mappedCompany);
                _companyRepository.Save();
                return RedirectToAction("Index", "Home");  //// Hint we need inhance that
            }
            return View(CompanyVM);
        }
        public IActionResult Details(string id)
        {
            var Company = _companyRepository.GetById(id);
            if (Company is null)
            {
                return BadRequest();
            }
            var mappedJop = _mapper.Map<Company, CompanyViewModel>(Company);
            return View(mappedJop);
        }
        #endregion

        #region Actions
        
        #endregion
    }
}
