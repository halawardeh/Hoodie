using System.Drawing;
using Hoodie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hoodie.Controllers
{
    public class CategoryProductsController : Controller
    {


        private readonly HoodieContext _context;
        public CategoryProductsController(HoodieContext context)
        {
            _context = context;
        }

        //present sub categories in this categories
        public IActionResult ByCategory(int categoryId)
        {
            var subcategories = _context.SubCategories
                .Where(c => c.CategoryId == categoryId)
                .ToList();

            return View(subcategories);
        }

      

        public IActionResult BySubCategory(int subcateoryID)
        {
            // Get the subcategory by its ID
            var subcategory = _context.SubCategories.FirstOrDefault(s => s.Id == subcateoryID);

            if (subcategory == null)
            {
                return NotFound();
            }

            // Get the category ID related to that subcategory
            var categoryId = subcategory.CategoryId;

            // Get all products where CategoryId matches
            var products = _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            return View(products);
        }


        public IActionResult ProductDetails(int id)
        {
            var product = _context.Products
            .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
            .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
            .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        public IActionResult GetProductByColor(int productID, int colorId)
        {
            var product = _context.Products
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .FirstOrDefault(p => p.ProductId == productID);

            if (product == null)
                return NotFound();

            ViewBag.SelectedColorId = colorId;

            return View("ProductDetails", product);
        }


        public IActionResult GetProductBySize(int productID, int sizeId)
        {

            var product = _context.Products
                .Include(p => p.ProductSizes)
                .ThenInclude(pc => pc.Size)
                .FirstOrDefault(p => p.ProductId == productID);

            if (product == null)
                return NotFound();
            ViewBag.SelectedSizeId = sizeId;
            return View("ProductDetails", product);
        }


    }
}
