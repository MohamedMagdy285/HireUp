using AutoMapper;
using HireUp.DAL.Models;
using HireUp.PL.ViewModels;
using System.Text.Json;

namespace HireUp.PL.Mapping
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            // ========== Job → ViewModel ==========
            CreateMap<Job, JobViewModel>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Skills = string.IsNullOrWhiteSpace(src.SkillsJson)
                        ? new List<string>()
                        : JsonSerializer.Deserialize<List<string>>(src.SkillsJson) ?? new List<string>();
                });

            // ========== ViewModel → Job ==========
            CreateMap<JobViewModel, Job>()
                .ForMember(dest => dest.SkillsJson, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.SkillsJson = src.Skills == null
                        ? null
                        : JsonSerializer.Serialize(src.Skills);
                });
        }
    }
}
