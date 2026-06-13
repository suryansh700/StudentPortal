using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;

namespace StudentPortal.Controllers
{
    public class NoticeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoticeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var notices =
                _context.Notices
                .OrderByDescending(n => n.Date)
                .ToList();

            return View(notices);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Notice notice)
        {
            notice.Date =
                DateTime.Now;

            _context.Notices.Add(
                notice);

            _context.SaveChanges();

            TempData["Success"] =
                "Notice Added Successfully";

            return RedirectToAction(
                "Index");
        }

        public IActionResult Delete(int id)
        {
            var notice =
                _context.Notices
                .FirstOrDefault(n =>
                    n.Id == id);

            if (notice != null)
            {
                _context.Notices.Remove(
                    notice);

                _context.SaveChanges();
            }

            return RedirectToAction(
                "Index");
        }
    }
}