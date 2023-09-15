using AutoMapper;
using CWork.DTO;
using CWork.Models;

namespace CWork
{
    public class YourAutoMapperProfile : Profile
    {
        public YourAutoMapperProfile()
        {
            CreateMap<LoanApplication, LoanApplicationDto>();
            CreateMap<LoanApplicationDto, LoanApplication>();
        }
    }
}
