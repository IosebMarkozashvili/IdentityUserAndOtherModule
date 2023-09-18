using MediatR;

namespace CWork.CQRS_Features.Loan.Command
{
    public class DeleteLoanApplicationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
