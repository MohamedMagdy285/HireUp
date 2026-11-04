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
    public class JobSeekerController : Controller
    {
        private readonly IJobSeekerRepository _jobSeekerRepository;
        private readonly IMapper _mapper;

        public JobSeekerController(IJobSeekerRepository jobSeekerRepository, IMapper mapper)
        {
            _jobSeekerRepository = jobSeekerRepository;
            _mapper = mapper;
        }

        #region CRUD
        public IActionResult Index()
        {
            var jobSeekers = _jobSeekerRepository.GetAll();

            var mappedjobSeeker = _mapper.Map<List<JobSeeker>, List<JobSeekerViewModel>>(jobSeekers);

            return View(mappedjobSeeker);
        }

        [Authorize(Roles = "JobSeeker,Company,Admin")]
        public IActionResult Profile(string id)
        {
            var jobSeeker = _jobSeekerRepository.GetById(id);
            var mappedJobSeeker = _mapper.Map<JobSeeker, JobSeekerProfileViewModel>(jobSeeker);
            mappedJobSeeker.ProfileImageUrlForDisplay = jobSeeker.ProfileImageUrl;
            mappedJobSeeker.CVPathForDisplay = jobSeeker.CVPath;
            return View(mappedJobSeeker);
        }

        [Authorize(Roles = "JobSeeker")]
        public IActionResult UpdateProfile(string jobseekerid)
        {
            var oldProfile = _jobSeekerRepository.GetById(jobseekerid);
            if (oldProfile is null)
                return BadRequest();

            var mappedJobSeeker = _mapper.Map<JobSeeker, JobSeekerProfileViewModel>(oldProfile);
            mappedJobSeeker.ProfileImageUrlForDisplay = oldProfile.ProfileImageUrl;
            mappedJobSeeker.CVPathForDisplay = oldProfile.CVPath;

             

            return View(mappedJobSeeker);
        }
        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        public IActionResult UpdateProfile(JobSeekerProfileViewModel JSVM)
        {
            if (!ModelState.IsValid)
                return View(JSVM);

            var oldJobSeeker = _jobSeekerRepository.GetById(JSVM.Id);
            if (oldJobSeeker == null)
                return BadRequest("User not found");

            _mapper.Map(JSVM, oldJobSeeker);

            if (JSVM.ProfileImageUrl != null)
                oldJobSeeker.ProfileImageUrl = Upload.UploadFile("imageProfiles", JSVM.ProfileImageUrl);

            if (JSVM.CVPath != null)
                oldJobSeeker.CVPath = Upload.UploadFile("CVs", JSVM.CVPath);

            _jobSeekerRepository.Update(oldJobSeeker);
            _jobSeekerRepository.Save();

            return RedirectToAction("Profile", new { id = JSVM.Id });

        }
        [Authorize(Roles = "JobSeeker")]
        public IActionResult Delete(string id)
        {
            var jobSeeker = _jobSeekerRepository.GetById(id);
            if (jobSeeker is null)
            {
                return BadRequest();
            }
            var mappedJop = _mapper.Map<JobSeeker, JobSeekerViewModel>(jobSeeker);
            return View(mappedJop);
        }
        [Authorize(Roles = "JobSeeker")]
        [HttpPost]
        public IActionResult Delete(JobSeekerViewModel jobSeekerVM)  // Admin Role
        {
            if (ModelState.IsValid)
            {
                var mappedjobSeeker = _mapper.Map<JobSeekerViewModel, JobSeeker>(jobSeekerVM);
                _jobSeekerRepository.Delete(mappedjobSeeker);
                _jobSeekerRepository.Save();
                return View("Index", "Home");  //// Hint we need inhance that
            }
            return View(jobSeekerVM);
        }
        public IActionResult Details(string id)
        {
            var jobSeeker = _jobSeekerRepository.GetById(id);
            if (jobSeeker is null)
            {
                return BadRequest();
            }
            var mappedJop = _mapper.Map<JobSeeker, JobSeekerViewModel>(jobSeeker);
            return View(mappedJop);
        }
        #endregion

        #region Actions
        
       
        #endregion
    }
}
