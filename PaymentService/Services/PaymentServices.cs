// Payment Services
// By Maitham Al-rubaye

using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentService.Models;
using PaymentService.Data;
using PaymentService.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace PaymentService.Services
{
    public class PaymentServices : IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentServices(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            var result = await _context.Payments.ToListAsync();
            if (result == null)
            {
                throw new NullReferenceException("No Payments Found");
            }
            
            return result;
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            var result = await _context.Payments.FindAsync(id);
            if (result == null)
            {
                throw new NullReferenceException("No Payment Found");
            }

            return result;
        }

        public async Task InsertPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await GetPaymentByIdAsync(id);
            if (payment == null)
            {
                throw new NullReferenceException("No Payment Found");
            }
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment.PaymentStatuses> GetPaymentStatusByIdAsync(int id)
        {
            var payment = await GetPaymentByIdAsync(id);
            if (payment == null)
            {
                throw new NullReferenceException("No Payment Found");
            }
            return payment.PaymentStatus;
        }

        public async Task UpdatePaymentStatusAsync(int id, Payment.PaymentStatuses paymentStatus)
        {
            var payment = await GetPaymentByIdAsync(id);
            if (payment == null)
            {
                throw new NullReferenceException("No Payment Found");
            }
            payment.PaymentStatus = paymentStatus;
            await _context.SaveChangesAsync();
        }
    }
}