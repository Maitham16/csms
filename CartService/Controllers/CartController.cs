// Cart Controller class
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartService.Data;
using CartService.Models;
using CartService.Repositories;
using CartService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CartService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartsAsync()
        {
            var carts = await _cartRepository.GetCartsAsync();
            return Ok(carts);
        }

        [HttpGet("{cartId}", Name = "GetCartById")]
        public async Task<ActionResult<Cart>> GetCartAsync(int cartId)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCartAsync(Cart cart)
        {
            var newCart = await _cartRepository.CreateCartAsync(cart);
            return CreatedAtRoute("GetCartById", new { cartId = newCart.Id }, newCart);
        }

        [HttpPut("{cartId}")]
        public async Task<ActionResult<bool>> UpdateCartAsync(int cartId, Cart cart)
        {
            if (cartId != cart.Id)
            {
                return BadRequest();
            }

            var updated = await _cartRepository.UpdateCartAsync(cart);
            return Ok(updated);
        }

        [HttpDelete("{cartId}")]
        public async Task<ActionResult<bool>> DeleteCartAsync(int cartId)
        {
            var deleted = await _cartRepository.DeleteCartAsync(cartId);
            return Ok(deleted);
        }

        [HttpGet("{cartId}/items")]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetItemsInCartAsync(int cartId)
        {
            var items = await _cartRepository.GetItemsInCartAsync(cartId);
            return Ok(items);
        }

        [HttpPost("{cartId}/items")]
        public async Task<ActionResult<bool>> AddItemToCartAsync(int cartId, CartItem item)
        {
            var added = await _cartRepository.AddItemToCartAsync(cartId, item);
            return Ok(added);
        }

        [HttpDelete("{cartId}/items/{itemId}")]
        public async Task<ActionResult<bool>> RemoveItemFromCartAsync(int cartId, int itemId)
        {
            var removed = await _cartRepository.RemoveItemFromCartAsync(cartId, itemId);
            return Ok(removed);
        }

        [HttpPut("{cartId}/items/{itemId}")]
        public async Task<ActionResult<bool>> UpdateItemInCartAsync(int cartId, CartItem item)
        {
            var updated = await _cartRepository.UpdateItemInCartAsync(cartId, item);
            return Ok(updated);
        }

        [HttpGet("{cartId}/items/{itemId}")]
        public async Task<ActionResult<CartItem>> GetCartItemByIdAsync(int itemId)
        {
            var item = await _cartRepository.GetCartItemByIdAsync(itemId);
            return Ok(item);
        }

        [HttpGet("{cartId}/total")]
        public async Task<ActionResult<decimal>> CalculateCartTotalAsync(int cartId)
        {
            var total = await _cartRepository.CalculateCartTotalAsync(cartId);
            return Ok(total);
        }

        [HttpGet("products/{productId}/availability")]
        public async Task<ActionResult<bool>> CheckProductAvailabilityInInventoryAsync(int productId)
        {
            var available = await _cartRepository.IsProductAvailableInInventoryAsync(productId);
            return Ok(available);
        }

        [HttpGet("{cartId}/expired")]
        public async Task<ActionResult<bool>> IsCartExpired(int cartId)
        {
            var cart = await _cartRepository.GetCartAsync(cartId);
            if (cart == null)
            {
                return NotFound();
            }
            var isExpired = _cartRepository.IsCartExpired(cart);
            return Ok(isExpired);
        }

        [HttpPost("{cartId}/applyDiscount")]
        public async Task<ActionResult> ApplyDiscountToCart(int cartId, [FromBody] string discountCode)
        {
            try
            {
                await _cartRepository.ApplyDiscountToCartAsync(cartId, discountCode);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{cartId}/checkout")]
        public async Task<ActionResult<bool>> Checkout(int cartId)
        {
            try
            {
                var result = await _cartRepository.CheckoutAsync(cartId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}