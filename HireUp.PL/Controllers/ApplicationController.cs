using AutoMapper;
using HireUp.BLL.Repositories;
using HireUp.BLL.Services;
using HireUp.DAL.Models;
using HireUp.DAL.Models.Auth;
using HireUp.PL.Helper;
using HireUp.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HireUp.PL.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobSeekerRepository _jobSeekerRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public ApplicationController(UserManager<ApplicationUser> userManager,IApplicationRepository applicationRepository,IJobSeekerRepository jobSeekerRepository,IJobRepository jobRepository,ICompanyRepository companyRepository, IMapper mapper)
        {
            _userManager = userManager;
            _applicationRepository = applicationRepository;
            _jobSeekerRepository = jobSeekerRepository;
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        #region JobSeeker
        [Authorize(Roles = "JobSeeker")]
        public IActionResult MyApplications()
        {
            var jobSeekerId = _userManager.GetUserId(User);

            var applications = _applicationRepository.GetAll() .Where(a => a.JobSeekerId == jobSeekerId).ToList();

            var mappedApp = _mapper.Map<List<ApplicationViewModel>>(applications);

            return View(mappedApp);
        }

        [Authorize(Roles = "JobSeeker")]
        public IActionResult Apply(int jobid)
        {
            var userId = _userManager.GetUserId(User);

            var job = _jobRepository.GetById(jobid);
            var jobSeeker = _jobSeekerRepository.GetById(userId);

            var vm = new ApplicationViewModel
            {
                JobId = jobid,
                Job = job,
                CompanyId = job.CompanyId,
                Company = job.Company,
                JobSeekerId = userId,
                JobSeeker = jobSeeker
            };

            return View(vm);
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpPost]
        public IActionResult Apply(ApplicationViewModel appVM)
        {
            //var userId = _userManager.GetUserId(User);
            //if (ModelState.IsValid)
            //{

            //}
            var oldJobSeeker = _jobSeekerRepository.GetById(appVM.JobSeekerId);
            if (oldJobSeeker == null)
                return BadRequest("User not found");

            if (appVM.JobSeekerViewModel.CVPath != null)
                oldJobSeeker.CVPath = Upload.UploadFile("CVs", appVM.JobSeekerViewModel.CVPath);

            _jobSeekerRepository.Update(oldJobSeeker);
            _jobSeekerRepository.Save();

            var mappedJobSeeker = _mapper.Map<JobSeeker, JobSeekerProfileViewModel>(oldJobSeeker);
            mappedJobSeeker.CVPathForDisplay = oldJobSeeker.CVPath;


            var app = new Application
            {
                JobId = appVM.JobId,
                JobSeekerId = appVM.JobSeekerId,
                Status = "Pending",
                AppliedAt = DateTime.Now
            };

            _applicationRepository.Add(app);
            _applicationRepository.Save();

            return RedirectToAction("MyApplications");
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var app = _applicationRepository.GetById(id);
            _applicationRepository.Delete(app);
            _applicationRepository.Save();
            return RedirectToAction("MyApplications");
        }

        [Authorize(Roles = "Company")]
        [HttpPost]
        public IActionResult Approve(int id)
        {

            var oldApp = _applicationRepository.GetById(id);

            oldApp.Status = "Approved";

            _applicationRepository.Update(oldApp);
            _applicationRepository.Save();

            return RedirectToAction("ApplicationsForMyJobs");
        }
        [Authorize(Roles = "Company")]
        [HttpPost]
        public IActionResult Reject(int id)
        {

            var oldApp = _applicationRepository.GetById(id);

            oldApp.Status = "Rejected";

            _applicationRepository.Update(oldApp);
            _applicationRepository.Save();

            return RedirectToAction("ApplicationsForMyJobs");
        }

        #endregion

        #region Company
        [Authorize(Roles = "Company")]
        public IActionResult ApplicationsForMyJobs()
        {
            var companyId = _userManager.GetUserId(User);

            var applications = _applicationRepository.GetAll().Where(a => a.Job?.Company?.Id == companyId).ToList();

            var mappedApp = _mapper.Map<List<Application>, List<ApplicationViewModel>>(applications);

            return View(mappedApp);
        }
        #endregion
    }
}
