using Hoodie.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Hoodie.Controllers
{
    public class UserController : Controller
    {
        private readonly HoodieContext _context;

        public UserController(HoodieContext context)
        {
            _context = context;
        }


        //------------ Sign Up + Login + Profile&Edit + Logout -----------
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public IActionResult HandleSignUp(string fname, string lname, string email, string phoneNumber, string password, string repeatpassword)
        {
            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(phoneNumber) ||
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
            else if (_context.Users.Any(u => u.Email == email))
            {
                TempData["Message"] = "This email is already registered!";
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
                    Password = password
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
            var user = _context.Users.FirstOrDefault(s => s.Email == email);

            if (user != null)
            {
                if (user.Password == password) // تحقق بسيط، بدون تشفير
                {
                    HttpContext.Session.SetString("email", user.Email);
                    return RedirectToAction("Profile");
                }
                else
                {
                    TempData["Message"] = "Incorrect Email or password!";
                    return RedirectToAction("Login");
                }
            }

            TempData["Message"] = "User not found!";
            return RedirectToAction("Login");
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


        [HttpGet]
        public IActionResult EditProfile()
        {
            var email = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                TempData["Message"] = "User not found!";
                return RedirectToAction("Login");
            }

            return View(user);
        }


        [HttpPost]
        public IActionResult EditProfile(User user1)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == user1.Email);
                if (user == null)
                {
                    TempData["Message"] = "User not found!";
                    return RedirectToAction("Login");
                }

                user.Fname = user1.Fname;
                user.Lname = user1.Lname;
                user.Email = user1.Email;
                user.Phonenumber = user1.Phonenumber;

                //_context.Users.Update(user);
                _context.SaveChanges();

                HttpContext.Session.SetString("email", user1.Email);
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

        //--------- End Of Sign Up + Login + Profile&Edit + Logout -----------



        //--------Reset password ---------
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string password, string newPassword, string repeatPassword)
        {
            string email = HttpContext.Session.GetString("email");

            if (!string.IsNullOrEmpty(email))
            {
                var user = _context.Users.FirstOrDefault(h => h.Email == email);

                if (user == null)
                {
                    TempData["Message"] = "User not found!";
                    return RedirectToAction(nameof(ResetPassword));
                }

                if (newPassword != repeatPassword)
                {
                    TempData["Message"] = "The two passwords do not match!";
                    return RedirectToAction(nameof(ResetPassword));
                }

                if (password != user.Password)
                {
                    TempData["Message"] = "The current password is incorrect!";
                    return RedirectToAction(nameof(ResetPassword));
                }

                user.Password = newPassword;
                _context.SaveChanges();

                TempData["Message"] = "Your password has been reset successfully!";
                return RedirectToAction(nameof(Profile));
            }

            TempData["Message"] = "Session expired. Please log in again.";
            return RedirectToAction(nameof(ResetPassword));
        }

        //-------- End of Reset password ---------


        //--------Add To Cart + Cart Details ---------
        public IActionResult Cart()
        { return View(); }

        [HttpPost]
        public IActionResult Cart(int productID)
        {
            var uEmail = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == uEmail);
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productID);

            if (user == null)
                return RedirectToAction("Login", "User");

            if (product == null)
                return NotFound();

            var existingCartItem = _context.Carts.FirstOrDefault(c => c.ProductId == productID && c.UserId == user.UserId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;



            }
            else
            {
                var newCartItem = new Cart
                {
                    UserId = user.UserId,
                    ProductId = product.ProductId,
                    Quantity = 1,
                };

                _context.Carts.Add(newCartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("CartDetails", "User"); // العودة للصفحة الرئيسية
        }

        public IActionResult CartDetails()
        {
            var sessionEmail = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == sessionEmail);

            if (user == null)
                return RedirectToAction("Login", "User");
            var CartItems = _context.Carts
                .Where(u => u.UserId == user.UserId)
                .Include(u => u.Product)
                .ToList();


            return View(CartItems);
        }


        [HttpPost]
        public IActionResult UpdateQuantity(int userId, int productId, int quantity, string actionType)
        {
            var cartItem = _context.Carts.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
            if (cartItem != null)
            {
                if (actionType == "increase")
                    cartItem.Quantity += 1;
                else if (actionType == "decrease" && cartItem.Quantity > 1)
                    cartItem.Quantity -= 1;
                else
                    cartItem.Quantity = quantity;

                _context.SaveChanges();
            }
            return RedirectToAction("CartDetails");
        }



        //--------End of Add To Cart + Cart Details ---------



        //--------Add To WishList ---------
        public IActionResult Wishlist()
        {

            return View();
        }

        public IActionResult AddToWishlist(int productID)
        {

            var uEmail = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == uEmail);
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productID);

            if (user == null || uEmail==null)
                return RedirectToAction("Login", "User");


            if (product == null)
                return NotFound();

            var existingWishListItem = _context.Wishlists.FirstOrDefault(c => c.ProductId == productID && c.UserId == user.UserId);


            var newWishItem = new Wishlist
            {
                UserId = user.UserId,
                ProductId = product.ProductId,
            };

            _context.Wishlists.Add(newWishItem);
            _context.SaveChanges();

            return RedirectToAction("WishListDetails");
        }
        public IActionResult WishListDetails()
        {
            var sessionEmail = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == sessionEmail);

            if (user == null)
                return RedirectToAction("Login", "User");

            var WishListItem = _context.Wishlists
                .Where(u => u.UserId == user.UserId)
                .Include(u => u.Product).ToList();

            return View(WishListItem);
        }
        //-------- End Of Add To WishList ---------



        //-------- User Checkout ---------
        public IActionResult Checkout()
        {
            var sessionEmail = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == sessionEmail);

            if(sessionEmail==null)
                return RedirectToAction("Login", "User");

            if (user == null)
                return RedirectToAction("Login", "User");

            var cartItems = _context.Carts.Where(u => u.UserId == user.UserId)
                                          .Include(u => u.Product)
                                          .ToList();

            if (cartItems.Count == 0)
                return RedirectToAction("Index", "Home");

            var newOrder = new Order
            {
                UserId = user.UserId,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                TotalAmount = cartItems.Sum(item => item.Product.Price * item.Quantity),
                Status = "Pending"
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges(); 

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = newOrder.OrderId,  
                    ProductId = item.ProductId,  
                    Quantity = item.Quantity.HasValue ? item.Quantity.Value : 0 , 
                    Price = item.Product.Price   
                };

                _context.OrderItems.Add(orderItem);
            }

            _context.SaveChanges();  

            return RedirectToAction("OrderDetails", new { orderId = newOrder.OrderId });
        }


        public IActionResult OrderDetails(int orderId)
        {
            var sessionEmail = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == sessionEmail);

            if (user == null)
                return RedirectToAction("Login", "User");

            var order = _context.Orders
                .Where(o => o.OrderId == orderId && o.UserId == user.UserId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefault();

            if (order == null)
                return RedirectToAction("Index", "Home");

            order.TotalAmount = order.OrderItems.Sum(oi => oi.Price * oi.Quantity);

            return View(order);
        }

        [HttpPost]
        public IActionResult CheckoutConfirmation(int orderId)
        {
            var order = _context.Orders
                                .Include(o => o.OrderItems)
                                .ThenInclude(oi => oi.Product)
                                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
                return RedirectToAction("OrderDetails");

            order.Status = "Confirmed";
            _context.SaveChanges();

            return RedirectToAction("PaymentPage", new { orderId = orderId }); 
        }

        [HttpGet]
        public IActionResult PaymentPage(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                return RedirectToAction("Index", "Home");

            return View(order); 
        }

        [HttpPost]
        public IActionResult PaymentPagePost(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                return RedirectToAction("Index", "Home");

            order.Status = "Paid";
            _context.SaveChanges();

            return RedirectToAction("PaymentSuccess");
        }

        public IActionResult PaymentSuccess()
        {
            var sessionEmail = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == sessionEmail);

            if (user == null)
                return RedirectToAction("Login", "User");

            var cartItems = _context.Carts.Where(c => c.UserId == user.UserId).ToList();
            if (cartItems.Any())
            {
                _context.Carts.RemoveRange(cartItems);
                _context.SaveChanges();
            }

            return View(); // يعرض View الـ PaymentSuccess.cshtml
        }


        //-------- End if User Checkout ---------



        //--------get All Orders & order details -----------------





        public IActionResult AllOrders()
        {
            var sessionEmail = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == sessionEmail);

            if (user == null)
                return RedirectToAction("Login", "User");

            var ordersItem = _context.Orders
                .Include(o => o.OrderItems)  // تضمين OrderItems
                .ThenInclude(oi => oi.Product)  // تضمين المنتجات داخل OrderItems
                .Where(u => u.UserId == user.UserId)
                .ToList();

            return View(ordersItem);
        }

        public IActionResult AllOrdersDetails(int orderId)
        {
            var sessionEmail = HttpContext.Session.GetString("email");
            var user = _context.Users.FirstOrDefault(u => u.Email == sessionEmail);

            if (user == null)
                return RedirectToAction("Login", "User");

            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderId == orderId && o.UserId == user.UserId);

            if (order == null)
                return RedirectToAction("AllOrders");

            return View(order); 
        }

        //--------End of getting All Orders ---------



    }
}
