using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetKing.Models;
using System.Linq;

namespace PetKing.Controllers
{
    public class ProductController : Controller
    {
        private readonly PetKingContext _context;

        public ProductController(PetKingContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? categoryId)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryID == categoryId.Value);
            }

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.SelectedCategoryId = categoryId;
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