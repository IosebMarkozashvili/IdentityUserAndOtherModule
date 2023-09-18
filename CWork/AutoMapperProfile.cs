using AutoMapper;
using CWork.DTO;
using CWork.Models;

namespace CWork
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LoanApplication, LoanApplicationDto>();
            CreateMap<LoanApplicationDto, LoanApplication>();
        }
    }
}
