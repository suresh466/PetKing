﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PetKing.Models;
using PetKing.ViewModels;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace PetKing.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly PetKingContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(PetKingContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.UserID == userId && o.Status == "IN_CART");

            if (order == null || !order.OrderItems.Any())
            {
                return RedirectToAction("Index", "Product");
            }

            var checkoutViewModel = new CheckoutViewModel
            {
                Order = order
            };

            return View(checkoutViewModel);
        }

        [HttpPost]
        public IActionResult PlaceOrder(CheckoutViewModel checkoutViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var order = _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefault(o => o.UserID == userId && o.Status == "IN_CART");

                if (order == null || !order.OrderItems.Any())
                {
                    ModelState.AddModelError("", "Your cart is empty");
                    return View("Checkout", checkoutViewModel);
                }

                // Validate that all products in the order still exist and have sufficient stock
                foreach (var item in order.OrderItems)
                {
                    var product = _context.Products.Find(item.ProductID);
                    if (product == null)
                    {
                        ModelState.AddModelError("", $"Product {item.Product.Name} is no longer available");
                        return View("Checkout", checkoutViewModel);
                    }
                    // Add stock check here if you implement inventory management
                }

                order.Status = "PLACED";
                order.OrderDate = DateTime.Now;
                order.ShippingName = checkoutViewModel.ShippingName;
                order.ShippingAddress = checkoutViewModel.ShippingAddress;
                order.ShippingCity = checkoutViewModel.ShippingCity;
                order.ShippingPostalCode = checkoutViewModel.ShippingPostalCode;

                _context.SaveChanges();
                return RedirectToAction("OrderConfirmation", new { orderId = order.OrderID });
            }

            // If we got this far, something failed; redisplay form
            return View("Checkout", checkoutViewModel);
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderID == orderId);

            if (order == null || order.UserID != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            return View(order);
        }
    }
}