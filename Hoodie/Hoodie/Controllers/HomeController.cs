using System.Diagnostics;
using Hoodie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hoodie.Controllers
{
    public class HomeController : Controller
    {

        private readonly HoodieContext _context;
        public HomeController(HoodieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = this._context.Categories.Where(cat => cat.StoreId == null).ToList();
            return View(categories);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Message msg)
        {
            var newMessage = new Message
            {
                UserName = msg.UserName,
                Email = msg.Email,
                PhoneNumber = msg.PhoneNumber,
                Messagee = msg.Messagee,
                CreatedAt = DateTime.Now
            };

            _context.Add(newMessage);
            _context.SaveChanges();

            TempData["Message"] = "Message sent successfully!";
            return RedirectToAction("Contact");

        }


        public IActionResult Stores()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }

        public IActionResult Hoodie()
        {
            return View();
        }

        public IActionResult Store()
        {
            return View();
        }

        public IActionResult Error404()
        {

            return View();
        }


    }
}
