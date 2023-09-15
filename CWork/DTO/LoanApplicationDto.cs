using CWork.Models;

namespace CWork.DTO
{
    public class LoanApplicationDto
    {
        public int Id { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public LoanStatus Status { get; set; }
        public LoanType Type { get; set; }
    }

}
