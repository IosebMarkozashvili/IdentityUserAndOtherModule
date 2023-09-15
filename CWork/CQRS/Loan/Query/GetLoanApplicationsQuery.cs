using CWork.DTO;
using MediatR;

namespace CWork.CQRS.Loan.Query
{
    public class GetLoanApplicationsQuery : IRequest<List<LoanApplicationDto>>
    {
        public string UserId { get; set; }
    }
}
