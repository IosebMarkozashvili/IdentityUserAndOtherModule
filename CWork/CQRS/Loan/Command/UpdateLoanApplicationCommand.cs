using MediatR;

namespace CWork.CQRS.Loan.Command
{
    public class UpdateLoanApplicationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public string UserId { get; set; }
    }

}
