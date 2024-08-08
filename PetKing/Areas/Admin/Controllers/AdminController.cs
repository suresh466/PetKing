using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PetKing.Models;
using System.Linq;
using System;

namespace PetKing.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly PetKingContext _context;
        private readonly int PageSize = 6; // Number of items per page

        public AdminController(PetKingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalProducts = _context.Products.Count();
            ViewBag.NewOrders = _context.Orders.Count();

            return View();
        }

        public IActionResult Products(int page = 1)
        {
            var products = _context.Products.Include(p => p.Category);
            var totalProducts = products.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)PageSize);

            var pagedProducts = products
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedProducts);
        }

        public IActionResult Orders(int page = 1)
        {
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.Status != "IN_CART")
                .OrderByDescending(o => o.OrderDate);

            var totalOrders = orders.Count();
            var totalPages = (int)Math.Ceiling(totalOrders / (double)PageSize);

            var pagedOrders = orders
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedOrders);
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(int orderId, string status)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = status;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Orders));
        }

        public IActionResult AddProduct()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                // Check if a product with the same name already exists
                if (_context.Products.Any(p => p.Name == product.Name))
                {
                    ModelState.AddModelError("Name", "A product with this name already exists");
                    ViewBag.Categories = _context.Categories.ToList();
                    return View(product);
                }

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Products));
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        public IActionResult EditProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Update(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Products));
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Products));
        }
    }
}