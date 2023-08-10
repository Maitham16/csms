// Shipping Controller class
// By Maitham Al-rubaye

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShippingService.Models;
using ShippingService.Repositories;

namespace ShippingService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingRepository _shippingRepository;

        public ShippingController(IShippingRepository shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipping>>> GetAllShippingsAsync()
        {
            try
            {
                var shipping = await _shippingRepository.GetAllShippingsAsync();
                if (shipping == null)
                {
                    return NotFound();
                }
                return Ok(shipping);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Shipping>>> GetShippingByIdAsync(int id)
        {
            try
            {
                var shipping = await _shippingRepository.GetShippingByIdAsync(id);
                if (shipping == null)
                {
                    return NotFound();
                }
                return Ok(shipping);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Shipping>> InsertShippingAsync([FromBody] Shipping shipping)
        {
            try
            {
                await _shippingRepository.InsertShippingAsync(shipping);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShippingAsync(int id)
        {
            try
            {
                await _shippingRepository.DeleteShippingAsync(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateShippingAsync(int id, [FromBody] Shipping shipping)
        {
            try
            {
                shipping.Id = id;
                await _shippingRepository.UpdateShippingAsync(shipping);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/status")]
        public async Task<ActionResult<Shipping.ShippingStatus>> GetShippingStatusAsync(int id)
        {
            try
            {
                var shippingStatus = await _shippingRepository.GetShippingStatusAsync(id);
                return Ok(shippingStatus);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateShippingStatusAsync(int id, [FromBody] Shipping.ShippingStatus shippingStatus)
        {
            try
            {
                await _shippingRepository.UpdateShippingStatusAsync(id, shippingStatus);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}