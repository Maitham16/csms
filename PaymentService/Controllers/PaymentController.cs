// Payment Controller
// by Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Models;
using PaymentService.Services;
using PaymentService.Repositories;

namespace PaymentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        // GET: Payment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAllPaymentsAsync()
        {
            try
            {
                var result = await _paymentRepository.GetAllPaymentsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Payment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPaymentByIdAsync(int id)
        {
            try
            {
                var result = await _paymentRepository.GetPaymentByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Payment
        [HttpPost]
        public async Task<ActionResult> InsertPaymentAsync([FromBody] Payment payment)
        {
            try
            {
                await _paymentRepository.InsertPaymentAsync(payment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: Payment/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePaymentAsync(int id, [FromBody] Payment payment)
        {
            try
            {
                payment.Id = id;
                await _paymentRepository.UpdatePaymentAsync(payment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: Payment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaymentAsync(int id)
        {
            try
            {
                await _paymentRepository.DeletePaymentAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Payment/5/Status
        [HttpGet("{id}/Status")]
        public async Task<ActionResult<Payment.PaymentStatuses>> GetPaymentStatusByIdAsync(int id)
        {
            try
            {
                var result = await _paymentRepository.GetPaymentStatusByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: Payment/5/Status
        [HttpPut("{id}/Status")]
        public async Task<ActionResult> UpdatePaymentStatusAsync(int id, [FromBody] Payment.PaymentStatuses paymentStatus)
        {
            try
            {
                await _paymentRepository.UpdatePaymentStatusAsync(id, paymentStatus);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

      