using MediatR;

namespace CWork.CQRS.Loan.Command
{
    public class DeleteLoanApplicationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
