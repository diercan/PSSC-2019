using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ShoppingCart
    {
        private readonly AppPhoneDBContext _appPhoneDBContext;
        private ShoppingCart(AppPhoneDBContext appPhoneDBContext)
        {
            _appPhoneDBContext = appPhoneDBContext;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppPhoneDBContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Phone phone,string amount)
        {
            var shoppingCartItem = _appPhoneDBContext.ShoppingCartItems.SingleOrDefault(s => s.Phone.Id == phone.Id && s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Phone = phone,
                    Amount = 1
                };
                _appPhoneDBContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _appPhoneDBContext.SaveChanges();
        }

        public int RemoveFromCart(Phone phone)
        {
            var shoppingCartItem = _appPhoneDBContext.ShoppingCartItems.SingleOrDefault(s => s.Phone.Id == phone.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;
            if(shoppingCartItem !=null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appPhoneDBContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _appPhoneDBContext.SaveChanges();
            return localAmount;
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _appPhoneDBContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Phone)
                .ToList());
        }
        public void ClearCart()
        {
            var cartItems = _appPhoneDBContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appPhoneDBContext.ShoppingCartItems.RemoveRange(cartItems);
            _appPhoneDBContext.SaveChanges();
        }
        
        public decimal GetShoppingCartTotal()
        {
           
            var total = _appPhoneDBContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
               .Select(c => Convert.ToInt32(c.Phone.Price) * c.Amount).Sum();
            return total;
        }
        
        
    }
}
