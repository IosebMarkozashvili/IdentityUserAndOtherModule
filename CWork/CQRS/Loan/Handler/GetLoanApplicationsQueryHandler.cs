using AutoMapper;
using CWork.CQRS.Loan.Query;
using CWork.Db;
using CWork.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CWork.CQRS.Loan.Handler
{
    public class GetLoanApplicationsQueryHandler : IRequestHandler<GetLoanApplicationsQuery, List<LoanApplicationDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLoanApplicationsQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LoanApplicationDto>> Handle(GetLoanApplicationsQuery request, CancellationToken cancellationToken)
        {
            var loanApplications = await _context.Loans
                .Where(application => application.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<LoanApplicationDto>>(loanApplications);
        }
    }

}
