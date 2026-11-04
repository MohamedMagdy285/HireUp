using AutoMapper;
using HireUp.DAL.Models;
using HireUp.PL.ViewModels;

namespace HireUp.PL.Mapping
{
    public class JobSeekerpfpProfile: Profile
    {
        public JobSeekerpfpProfile() 
        {
            CreateMap<JobSeeker, JobSeekerProfileViewModel>()
            .ForMember(dest => dest.ProfileImageUrl, opt => opt.Ignore())
            .ForMember(dest => dest.CVPath, opt => opt.Ignore());

            CreateMap<JobSeekerProfileViewModel, JobSeeker>()
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CVPath, opt => opt.Ignore());
        }
    }
}
