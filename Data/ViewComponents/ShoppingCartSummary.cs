using Microsoft.AspNetCore.Mvc;
using NutryDairyASPApplication.Data.Cart;

namespace NutryDairyASPApplication.Data.ViewComponents
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart _shoppingcart;
        public ApplicationDbContext _context { get; set; }

        public ShoppingCartSummary(ApplicationDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingcart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingcart.GetShoppingCartItems();
            return View(items.Count);
        }
    }
}
