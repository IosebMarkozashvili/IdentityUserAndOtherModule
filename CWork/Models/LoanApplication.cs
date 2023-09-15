using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CWork.Models
{
    public class LoanApplication
    {
        public int? Id { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public LoanStatus Status { get; set; }
        public LoanType Type { get; set; } // Change the data type to LoanType

        // Foreign key to link the loan application to the user who submitted it
        public string UserId { get; set; }
        [ForeignKey(name: "UserId")]
        public ApplicationUser User { get; set; }
    }

    public enum LoanStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public enum LoanType // Define the LoanType enumeration
    {
        QuickLoan,
        AutoLoan,
        Installment
    }

}
