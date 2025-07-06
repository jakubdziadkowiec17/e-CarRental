using e_CarRentalAPI.Models.Entities;

namespace e_CarRentalAPI.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<int> GetPaymentCountAsync();
        Task<List<Payment>> GetPayments();
        Task<List<Payment>> GetPaymentsByRentalId(int rentalId);
        Task<Payment> GetPaymentByIdAsync(int id);
        Task AddPaymentAsync(Payment payment);
        Task DeletePaymentAsync(Payment payment);
    }
}
