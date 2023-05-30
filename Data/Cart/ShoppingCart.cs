using Microsoft.EntityFrameworkCore;
using NutryDairyASPApplication.Models;

namespace NutryDairyASPApplication.Data.Cart
{
    public class ShoppingCart
    {
        public ApplicationDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<ApplicationDbContext>();

            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId",cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId};
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (
                ShoppingCartItems = _context.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Include(p => p.Product)
                .ToList()
            );
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Select(s => s.Product.Price * s.Amount)
                .Sum();
            return total;
        }

        public void AddItemToCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(
                s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId
            );
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemToCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(
                s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId
            );
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
                _context.SaveChanges();
            }
        }

        public void ClearShoppingCart()
        {
            var items = _context.ShoppingCartItems
                .Where( s => s.ShoppingCartId == ShoppingCartId )
                .ToList();
            _context.ShoppingCartItems.RemoveRange(items);
            _context.SaveChanges();
        }
    }
}
