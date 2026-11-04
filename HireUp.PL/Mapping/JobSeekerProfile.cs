using AutoMapper;
using HireUp.DAL.Models;
using HireUp.PL.ViewModels;


namespace HireUp.PL.Mapping
{
    public class JobSeekerProfile : Profile
    {
        public JobSeekerProfile()
        {
            CreateMap<JobSeeker, JobSeekerViewModel>().ReverseMap()
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CVPath, opt => opt.Ignore()); ;
        }
    }
}
