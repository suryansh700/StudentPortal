using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;

namespace StudentPortal.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TotalStudents = _context.Students.Count();

            ViewBag.TotalAttendance = _context.Attendances.Count();

            ViewBag.TotalResults = _context.Results.Count();

            ViewBag.TotalFees = _context.Fees.Count();

            ViewBag.TotalNotices = _context.Notices.Count();

            return View();
        }
        public IActionResult ResetPortal()
        {
            _context.Attendances.RemoveRange(
                _context.Attendances);

            _context.Results.RemoveRange(
                _context.Results);

            _context.Fees.RemoveRange(
                _context.Fees);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}