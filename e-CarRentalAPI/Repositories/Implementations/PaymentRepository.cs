using e_CarRentalAPI.Database;
using e_CarRentalAPI.Models.Entities;
using e_CarRentalAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace e_CarRentalAPI.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetPaymentCountAsync()
        {
            return await _context.Payment.CountAsync();
        }

        public async Task<List<Payment>> GetPayments()
        {
            var payments = await _context.Payment.OrderByDescending(c => c.PaymentDate).ToListAsync();
            return payments;
        }

        public async Task<List<Payment>> GetPaymentsByRentalId(int rentalId)
        {
            var payments = await _context.Payment.Where(a => a.RentalId == rentalId).OrderByDescending(c => c.PaymentDate).ToListAsync();
            return payments;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _context.Payment.FindAsync(id);
        }

        public async Task DeletePaymentAsync(Payment payment)
        {
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
        }
    }
}
