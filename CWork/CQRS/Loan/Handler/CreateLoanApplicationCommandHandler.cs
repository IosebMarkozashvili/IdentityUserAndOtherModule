using CWork.CQRS.Loan.Command;
using CWork.Db;
using CWork.Models;
using MediatR;

namespace CWork.CQRS.Loan.Handler
{
    public class CreateLoanApplicationCommandHandler : IRequestHandler<CreateLoanApplicationCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateLoanApplicationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateLoanApplicationCommand request, CancellationToken cancellationToken)
        {
            var loanApplication = new LoanApplication
            {
                LoanAmount = request.LoanAmount,
                InterestRate = request.InterestRate,
                Type = request.Type,
                UserId = request.UserId
            };

            _context.Loans.Add(loanApplication);
            await _context.SaveChangesAsync(cancellationToken);

            return (int)loanApplication.Id;
        }
    }
}
