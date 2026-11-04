using AutoMapper;
using HireUp.BLL.Repositories;
using HireUp.BLL.Services;
using HireUp.DAL.Models;
using HireUp.DAL.Models.Auth;
using HireUp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace HireUp.PL.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly IJobSeekerRepository _jobSeeker;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobController(IJobRepository jobRepository,IJobSeekerRepository jobSeeker,ICompanyRepository companyRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _jobRepository = jobRepository;
            _jobSeeker = jobSeeker;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        #region CRUD
        public IActionResult Index(int? categoryId, string? SearchValue)
        {
            List<Job> jobs = new List<Job>();

            if (!string.IsNullOrEmpty(SearchValue))
            {
                SearchValue = SearchValue.Trim();
                jobs = _jobRepository.GetAll().Where(j =>
                    j.Title.Contains(SearchValue, StringComparison.OrdinalIgnoreCase) ||
                    j.Description.Contains(SearchValue, StringComparison.OrdinalIgnoreCase) ||
                    j.Requirements.Contains(SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                ViewBag.Flage = jobs.Count();
            }
            else if (categoryId.HasValue)
                jobs = _jobRepository.GetByCategory(categoryId.Value);
            else
                jobs = _jobRepository.GetAll();

            var mappedJobs = _mapper.Map<List<Job>, List<JobViewModel>>(jobs);
            ViewBag.CategoryId = categoryId;
            return View(mappedJobs);
        }

        [Authorize(Roles = "Company")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
       [Authorize(Roles = "Company")]
        public IActionResult Add(JobViewModel jobVM)
        {
            jobVM.Skills = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(Request.Form["Skills"]);
            if (ModelState.IsValid)
            {
                var companyId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                jobVM.CompanyId = companyId;
                var mappedJob = _mapper.Map<JobViewModel, Job>(jobVM);
                _jobRepository.Add(mappedJob);
                _jobRepository.Save();
                return RedirectToAction("Index"); 
            }
            return View(jobVM);
        }
        //[Authorize(Roles = "Company")]
        //public IActionResult Update(int id)
        //{
        //    var oldJob = _jobRepository.GetById(id);
        //    if (oldJob is null)
        //    {
        //        return BadRequest();
        //    }
        //    var mappedJob = _mapper.Map<Job, JobViewModel>(oldJob);
        //    return View(mappedJob);
        //}
        //[HttpPost]
        //[Authorize(Roles = "Company")]
        //public IActionResult Update(JobViewModel jobVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var mappedJob = _mapper.Map<JobViewModel, Job>(jobVM);
        //        _jobRepository.Update(mappedJob);
        //        _jobRepository.Save();
        //        return RedirectToAction("Index");
        //    }
        //    return View(jobVM);
        //}
        //[Authorize(Roles = "Company, Admin")]
        //public IActionResult Delete(int id)
        //{
        //    var Job = _jobRepository.GetById(id);
        //    if(Job is null)
        //    {
        //        return BadRequest();
        //    }
        //    var mappedJop = _mapper.Map<Job, JobViewModel>(Job);
        //    return View(mappedJop);
        //}
        [HttpPost]
        [Authorize(Roles = "Company, Admin")]       
        public IActionResult Delete(int id)
        {
            var job = _jobRepository.GetById(id);

            if (job == null)
                return NotFound();

            _jobRepository.Delete(job);
            _jobRepository.Save();

            return RedirectToAction("MyJobs");
        }

        //public IActionResult Delete(JobViewModel jobVM)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var mappedJob = _mapper.Map<JobViewModel,Job>(jobVM);
        //        _jobRepository.Delete(mappedJob);
        //        _jobRepository.Save();
        //        return RedirectToAction("Index");
        //    }
        //    return View(jobVM);
        //}
        public IActionResult Details(int id)
        {
            var Job = _jobRepository.GetById(id);
            if (Job is null)
            {
                return BadRequest();
            }
            var mappedJop = _mapper.Map<Job, JobViewModel>(Job);
            return View(mappedJop);
        }
        #endregion

        #region Actions
       
        #endregion


        //CompanyJobs
        public IActionResult MyJobs()
        {        
            var companyId = _userManager.GetUserId(User);
            var myjobs = _jobRepository.GetAll().Where(a => a.CompanyId == companyId).ToList();
            var mappedjobs = _mapper.Map<List<Job>,List<JobViewModel>>(myjobs);
            return View(mappedjobs);
        }
        //JobSeekerJobs
        //public IActionResult AppliedJobs()
        //{
        //    var JobSeekerId = _userManager.GetUserId(User);
        //    var myjobs =
        //    return View();
        //}
    }
}
