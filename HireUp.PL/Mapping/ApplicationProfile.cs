using AutoMapper;
using HireUp.DAL.Models;
using HireUp.PL.ViewModels;

namespace HireUp.PL.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Application, ApplicationViewModel>().ReverseMap();
        }
    }
}
