using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;
using System.Linq;

namespace StudentPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.Admins
                .FirstOrDefault(a =>
                    a.Username == username &&
                    a.Password == password);

            if (admin != null)
            {
                HttpContext.Session.SetString(
                    "Admin",
                    admin.Username
                );

                return RedirectToAction(
                    "Index",
                    "Dashboard"
                );
            }

            ViewBag.Error =
                "Invalid Username or Password";

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Login",
                "Account"
            );
        }
    }
}