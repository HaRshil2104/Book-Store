using Bookstore.Models.ViewModel;
using BookStore.Models.Models;
using BookStore.repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.repository
{
    public class CartRepository : BaseRepository
    {
        public List<Cart> GetCartItems(string keyword)
        {
            keyword = keyword?.ToLower().Trim();
            var query = _context.Carts.Include(c => c.Book).Where(c => keyword == null || c.Book.Name.ToLower().Contains(keyword)).AsQueryable();
            return  query.ToList();
        }
        public Cart GetCarts(int id)
        {
            return _context.Carts.FirstOrDefault(c => c.Id == id);
        }
        public Cart AddToCart(Cart cart)
        {
            var entry = _context.Carts.Add(cart);
            _context.SaveChanges();
            return entry.Entity;
        }
        public Cart UpdateCart(Cart cart)
        {
            var entry = _context.Carts.Update(cart);
            _context.SaveChanges();
            return entry.Entity;
        }
        public bool DeleteCart(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
                return false;

            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}
