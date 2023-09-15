using AutoMapper;
using CWork.CQRS.Loan.Query;
using CWork.Db;
using CWork.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CWork.CQRS.Loan.Handler
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
