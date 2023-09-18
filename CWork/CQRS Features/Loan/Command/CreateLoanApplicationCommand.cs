using CWork.Models;
using MediatR;

namespace CWork.CQRS_Features.Loan.Command
{
    public class CreateLoanApplicationCommand : IRequest<int>
    {
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public LoanType Type { get; set; }
        public string UserId { get; set; }
    }
}
