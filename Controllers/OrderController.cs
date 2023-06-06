using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutryDairyASPApplication.Data;
using NutryDairyASPApplication.Data.Cart;
using NutryDairyASPApplication.Data.Static;
using NutryDairyASPApplication.Models;
using NutryDairyASPApplication.ViewModels;

namespace NutryDairyASPApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductController _productController;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(ApplicationDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _productController = new ProductController(context);
            _shoppingCart = shoppingCart;

        }

        [Authorize]
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = GetOrdersByUserId(userId, userRole);
            return View(orders);
        }
        public List<Order> GetOrdersByUserId(string userId, string userRole)
        {
            var orders = _context.Orders
                .Include(o => o.Items)
                .ThenInclude(p => p.Product)
                .Include(u => u.User)
                .ToList();
            if (userRole != UserRoles.Admin)
            {
                orders = orders.Where(o => o.UserId == userId).ToList();
            }
            return orders;
        }
        [Authorize]
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
            };
            return View(response);
        }

        [Authorize]
        public IActionResult AddItemToShoppingCart(int id)
        {
            var item = _productController.GetProductById(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        [Authorize]
        public IActionResult RemoveItemFromShoppingCart(int id)
        {
            var item = _productController.GetProductById(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        [Authorize]
        public IActionResult CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            string userEmail = User.FindFirstValue(ClaimTypes.Email);

            if(items.Count == 0)
            {
                return RedirectToAction(nameof(ShoppingCart));
            }
            StoreOrder(items, userId, userEmail);
            _shoppingCart.ClearShoppingCart();

            return View("OrderCompleted");
        }

        [Authorize]
        public void StoreOrder(List<ShoppingCartItem> items, string userId, string userEmail)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmail,
                CreatedDate = DateTime.Now,
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.Price,
                };
                _context.OrderItems.Add(orderItem);
            }
            _context.SaveChanges();
        }
    }
}
