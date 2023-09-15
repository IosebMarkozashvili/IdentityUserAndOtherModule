using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWork.Db;
using CWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CWork.Repository.Loan
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanApplication>> GetLoanApplicationsAsync(string userId)
        {
            return await _context.Loans
                .Where(application => application.UserId == userId)
                .ToListAsync();
        }

        public async Task<LoanApplication> GetLoanApplicationByIdAsync(int id, string userId)
        {
            return await _context.Loans
                .FirstOrDefaultAsync(application => application.Id == id && application.UserId == userId);
        }

        public async Task<LoanApplication> CreateLoanApplicationAsync(LoanApplication loanApplication)
        {
            _context.Loans.Add(loanApplication);
            await _context.SaveChangesAsync();
            return loanApplication;
        }

        public async Task<bool> UpdateLoanApplicationAsync(int id, LoanApplication updatedLoanApplication)
        {
            var existingLoanApplication = await _context.Loans.FindAsync(id);

            if (existingLoanApplication == null)
            {
                return false; // Loan application not found
            }
            _context.Entry(existingLoanApplication).CurrentValues.SetValues(updatedLoanApplication);
            _context.Entry(existingLoanApplication).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true; // Update successful
            }
            catch (DbUpdateException ex)
            {
                return false; // Update failed
            }
        }
        public async Task<bool> DeleteLoanApplicationAsync(int id, string userId)
        {
            var loanApplication = await GetLoanApplicationByIdAsync(id, userId);
            if (loanApplication == null)
            {
                return false;
            }

            _context.Loans.Remove(loanApplication);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
