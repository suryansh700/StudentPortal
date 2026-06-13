using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;

namespace StudentPortal.Controllers
{
    public class FeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeesController(ApplicationDbContext context)
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

            var students =
                _context.Students.ToList();

            List<FeeViewModel> feeList =
                new List<FeeViewModel>();

            foreach (var student in students)
            {
                var latestFee =
                    _context.Fees
                    .Where(f =>
                        f.StudentId == student.Id)
                    .OrderByDescending(f => f.Id)
                    .FirstOrDefault();

                feeList.Add(
                    new FeeViewModel()
                    {
                        StudentId =
                            student.Id,

                        StudentName =
                            student.Name,

                        RollNo =
                            student.RollNo,

                        PhotoPath =
                            student.PhotoPath,

                        TotalFee =
                            latestFee?.TotalFee ?? 50000,

                        PaidFee =
                            latestFee?.PaidFee ?? 0,

                        DueFee =
                            latestFee?.DueFee ?? 50000,

                        Status =
                            latestFee?.Status ?? "Pending"
                    });
            }

            return View(feeList);
        }

        [HttpPost]
        public IActionResult Save(
            List<decimal> totalFee,
            List<decimal> paidFee)
        {
            var students =
                _context.Students.ToList();

            for (int i = 0; i < students.Count; i++)
            {
                decimal dueFee =
                    totalFee[i] - paidFee[i];

                string status;

                if (dueFee <= 0)
                {
                    status = "Paid";
                }
                else if (paidFee[i] > 0)
                {
                    status = "Partial";
                }
                else
                {
                    status = "Pending";
                }

                Fee fee =
                    new Fee()
                    {
                        StudentId =
                            students[i].Id,

                        StudentName =
                            students[i].Name,

                        RollNo =
                            students[i].RollNo,

                        TotalFee =
                            totalFee[i],

                        PaidFee =
                            paidFee[i],

                        DueFee =
                            dueFee,

                        Status =
                            status
                    };

                _context.Fees.Add(fee);
            }

            _context.SaveChanges();

            TempData["Success"] =
                "Fee Details Saved Successfully";

            return RedirectToAction(
                "History");
        }

        public IActionResult History()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var fees =
                _context.Fees
                .OrderByDescending(f => f.Id)
                .ToList();

            return View(fees);
        }

        public IActionResult ClearHistory()
        {
            _context.Fees.RemoveRange(
                _context.Fees);

            _context.SaveChanges();

            return RedirectToAction(
                "History");
        }
    }
}