using CWork.CQRS.Loan.Command;
using CWork.Db;
using CWork.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CWork.CQRS.Loan.Handler
{
    public class DeleteLoanApplicationCommandHandler : IRequestHandler<DeleteLoanApplicationCommand, Unit>
    {
        private readonly ApplicationDbContext _context;

        public DeleteLoanApplicationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteLoanApplicationCommand request, CancellationToken cancellationToken)
        {
            var applicationToDelete = await _context.Loans
                .SingleOrDefaultAsync(application => application.Id == request.Id && application.UserId == request.UserId, cancellationToken);

            if (applicationToDelete == null)
                return Unit.Value;

            if (applicationToDelete.Status != LoanStatus.Pending)
                return Unit.Value;

            _context.Loans.Remove(applicationToDelete);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
