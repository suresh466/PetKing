using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetKing.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace PetKing.Controllers
{
    public class CartController : Controller
    {
        private readonly PetKingContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(PetKingContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.UserID == userId && o.Status == "IN_CART");

            if (order == null)
            {
                order = new Order
                {
                    UserID = userId,
                    OrderDate = DateTime.Now,
                    Status = "IN_CART",
                    OrderItems = new List<OrderItem>() // Initialize OrderItems
                };
                _context.Orders.Add(order);
            }

            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductID == productId);
            if (orderItem == null)
            {
                orderItem = new OrderItem
                {
                    ProductID = productId,
                    Quantity = quantity,
                    Price = product.Price
                };
                order.OrderItems.Add(orderItem);
            }
            else
            {
                orderItem.Quantity += quantity;
            }

            order.TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.Price);

            _context.SaveChanges();

            return RedirectToAction("ViewCart");
        }

        [AllowAnonymous]
        public IActionResult AddToCartGuest(int productId, int quantity)
        {
            TempData["Message"] = "Please log in to add items to your cart.";
            return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Details", "Product", new { id = productId }) });
        }

        public IActionResult RemoveFromCart(int orderItemId)
        {
            var orderItem = _context.OrderItems.Find(orderItemId);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();

            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            var userId = _userManager.GetUserId(User);
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.UserID == userId && o.Status == "IN_CART");

            return View(order);
        }
    }
}