using Bookstore.Models.Models;
using Bookstore.Models.ViewModel;
using Bookstore.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace myWebapi2.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartRepository _cartRepository = new CartRepository();
        
        [Route("list")]
        [HttpGet]
        public IActionResult GetCartItems(string keyword)
        {
            List<Cart> carts = _cartRepository.GetCartItems(keyword);
            IEnumerable<CartModel> cartModels = carts.Select(c => new CartModel(c));
            return Ok(cartModels);
        }
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetCarts(int id)
        {
            var cart = _cartRepository.GetCarts(id);
            CartModel cartModel = new CartModel();
            return Ok(cartModel);
        }
        
        [Route("add")]
        [HttpPost]
        public IActionResult AddToCart(CartModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }
                Cart cart = new Cart()
                {
                    //Id = model.Id,
                    Userid = model.Userid,
                    Bookid = model.Bookid,
                    Quantity = model.Quantity

                };
                var response = _cartRepository.AddToCart(cart);
                return Ok(new CartModel(response));
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        
        [Route("update")]
        [HttpPut]
        public IActionResult UpdateCart(CartModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            Cart cart = new Cart()
            {
                //Id = model.Id,
                Userid = model.Userid,
                Bookid = model.Bookid,
                Quantity = model.Quantity
        };
            var response = _cartRepository.UpdateCart(cart); 
            return Ok(new CartModel(response));
        }

        [HttpDelete]
        [Route("del/{id}")]
        public IActionResult DeleteCart(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id is null");
            }
            bool response = _cartRepository.DeleteCart(id);
            return Ok(response);
        }
    }
}
