using CWork.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWork.Repository.Loan
{
    public interface ILoanRepository
    {
        Task<IEnumerable<LoanApplication>> GetLoanApplicationsAsync(string userId);
        Task<LoanApplication> GetLoanApplicationByIdAsync(int id, string userId);
        Task<LoanApplication> CreateLoanApplicationAsync(LoanApplication loanApplication);
        Task<bool> UpdateLoanApplicationAsync(int id, LoanApplication updatedLoanApplication);
        Task<bool> DeleteLoanApplicationAsync(int id, string userId);
    }
}
