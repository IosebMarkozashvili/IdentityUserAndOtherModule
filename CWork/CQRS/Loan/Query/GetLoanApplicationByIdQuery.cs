using CWork.DTO;
using MediatR;

namespace CWork.CQRS.Loan.Query
{
    public class GetLoanApplicationByIdQuery : IRequest<LoanApplicationDto>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
