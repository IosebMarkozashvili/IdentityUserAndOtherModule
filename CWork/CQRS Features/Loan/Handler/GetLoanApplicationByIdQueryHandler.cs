using AutoMapper;
using CWork.CQRS_Features.Loan.Query;
using CWork.Db;
using CWork.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CWork.CQRS_Features.Loan.Handler
{
    public class GetLoanApplicationByIdQueryHandler : IRequestHandler<GetLoanApplicationByIdQuery, LoanApplicationDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLoanApplicationByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LoanApplicationDto> Handle(GetLoanApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var loanApplication = await _context.Loans
                .SingleOrDefaultAsync(application => application.Id == request.Id && application.UserId == request.UserId, cancellationToken);

            if (loanApplication == null)
                return null;

            return _mapper.Map<LoanApplicationDto>(loanApplication);
        }
    }
}
