using CWork.CQRS_Features.Loan.Command;
using CWork.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CWork.CQRS_Features.Loan.Handler
{
    public class UpdateLoanApplicationCommandHandler : IRequestHandler<UpdateLoanApplicationCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public UpdateLoanApplicationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateLoanApplicationCommand request, CancellationToken cancellationToken)
        {
            var existingApplication = await _context.Loans
                .SingleOrDefaultAsync(application => application.Id == request.Id && application.UserId == request.UserId, cancellationToken);

            if (existingApplication == null)
                return Unit.Value;

            existingApplication.LoanAmount = request.LoanAmount;
            existingApplication.InterestRate = request.InterestRate;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
