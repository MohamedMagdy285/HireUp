using AutoMapper;
using HireUp.DAL.Models;
using HireUp.PL.ViewModels;

namespace HireUp.PL.Mapping
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyViewModel>().ReverseMap();
        }
    }
}
