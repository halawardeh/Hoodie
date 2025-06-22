using Hoodie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Masterpiece.Controllers
{
    public class AdminController : Controller
    {
        private readonly HoodieContext _context;

        public AdminController(HoodieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
   
        #region Category

        public IActionResult viewCategory()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult addCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addCategory(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(viewCategory));
        }

        public IActionResult editCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult editCategory(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existingCategory == null)
                return NotFound();

            existingCategory.Name = category.Name;
            existingCategory.Image = category.Image;

            _context.SaveChanges();

            return RedirectToAction(nameof(viewCategory));
        }

        public IActionResult deleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(viewCategory));
        }

        #endregion

        #region Product

        public IActionResult viewProduct()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult addProduct()
        {
            var categories = _context.Categories
                               .Select(c => new { c.CategoryId })
                               .ToList();

            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryId");

            return View();
        }


        [HttpPost]
        //public IActionResult addProduct(Product product)
        //{
        //    product.Availability = true;

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryId");
        //        return View(product);
        //    }

        //    _context.Products.Add(product);
        //    _context.SaveChanges();

        //    return RedirectToAction(nameof(viewProduct));
        //}

        [HttpPost]
        public IActionResult addProduct(Product product)
        {
            if (product.ImageFile != null && product.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(product.ImageFile.FileName);

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/img1");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    product.ImageFile.CopyTo(stream);
                }

                // فقط اسم الملف يُخزن
                product.Image = fileName;
            }

            product.Availability = true;

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryId");
                return View(product);
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(viewProduct));
        }

        public IActionResult editProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult editProduct(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            var existingProduct = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Image = product.Image;

            _context.SaveChanges();

            return RedirectToAction(nameof(viewProduct));
        }

        public IActionResult deleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(viewProduct));
        }

        #endregion

        #region Users

        public IActionResult viewUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        #endregion
    }
}
