using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetKing.Models;
using System;
using System.Linq;

namespace PetKing.Controllers
{
    public class ProductController : Controller
    {
        private readonly PetKingContext _context;
        private readonly int PageSize = 6; // Number of products per page

        public ProductController(PetKingContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? categoryId, int page = 1)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryID == categoryId.Value);
            }

            var totalProducts = products.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)PageSize);

            products = products.Skip((page - 1) * PageSize).Take(PageSize);

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(products.ToList());
        }

    public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}