using Hoodie.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoodie.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDbContext _context;

        public UserController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HandleSignUp(string fname, string lname, string email, string phoneNumber, string password, string repeatpassword)
        {
            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(fname)|| string.IsNullOrEmpty(phoneNumber) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatpassword))
            {
                TempData["Message"] = "All fields are required!";
                return RedirectToAction("SignUp");
            }
            else if (password != repeatpassword)
            {
                TempData["Message"] = "The two passwords do not match!";
                return RedirectToAction("SignUp");
            }
            else
            {
                var NewUser = new User
                {
                    Fname = fname,
                    Lname = lname,
                    Email = email,
                    Phonenumber = phoneNumber,
                    Password = password,
                };

                _context.Users.Add(NewUser);
                _context.SaveChanges();

                HttpContext.Session.SetString("email", email);
                HttpContext.Session.SetString("password", password);

                TempData["Message"] = "Registered successfully!";
                return RedirectToAction("Login");
            }
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult handleLogin(string email, string password)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Message"] = "All fields are required!";
                return RedirectToAction("Login");
            }


            var emailTrimmed = email.Trim().ToLower();  // تحويل الإيميل إلى حروف صغيرة
            var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == emailTrimmed);


            if (user == null)
            {
                TempData["Message"] = "User not found!";
                return RedirectToAction("SignUp");
            }
            
            if (password != user.Password)
            {
               
                TempData["Message"] = "The Email or the Password are wrong!";
                return RedirectToAction("Login");
            }


            HttpContext.Session.SetString("email", email);
            HttpContext.Session.SetString("password", password);
            TempData["user"] = new string[] { email };

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Profile()
        {
            var sessionEmail = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == sessionEmail);

            if (string.IsNullOrEmpty(sessionEmail))
                return RedirectToAction("Login");


            if (user == null)
            {
                TempData["Message"] = "User Not Found!";
                return RedirectToAction("Login");

            }
            return View(user);
        }


        //[HttpPost]
        public IActionResult EditProfile(string fname, string lname, string email, string phoneNumber)
        {
            try
            {
                var emailTrimmed = email.Trim().ToLower();  // تحويل الإيميل إلى حروف صغيرة
                var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == emailTrimmed);


                if (user == null)
                {
                    TempData["Message"] = "User not found!";
                    return RedirectToAction("Login");
                }

                user.Fname = fname;
                user.Lname = lname;
                user.Email = email;
                user.Phonenumber = phoneNumber;

                _context.Users.Update(user);
                _context.SaveChanges();

                HttpContext.Session.SetString("email", email);
                TempData["Message"] = "Profile updated successfully!";
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in a way that helps debug the issue
                TempData["Message"] = "An error occurred: " + ex.Message;
                return RedirectToAction("Profile");
            }
        }


        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Wishlist()
        {
            return View();
        }

  
        public IActionResult AllOrders()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }

    }
}
