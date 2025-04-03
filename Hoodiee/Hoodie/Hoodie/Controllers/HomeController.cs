using System.Diagnostics;
using Hoodie.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoodie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Stores()
        {
            return View();
        }

        public IActionResult ProductDetails()
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

        public IActionResult Error404(){
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
