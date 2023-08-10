// IPaymentRepository Interface
// By Maitham Al-rubaye

using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentService.Models;

namespace PaymentService.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task InsertPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(int id);
        Task<Payment.PaymentStatuses> GetPaymentStatusByIdAsync(int id);
        Task UpdatePaymentStatusAsync(int id, Payment.PaymentStatuses paymentStatus);
    }
}
