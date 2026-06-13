using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;

namespace StudentPortal.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
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

            var students = _context.Students.ToList();

            var attendanceData =
                students.Select(student =>
                {
                    var latestAttendance =
                        _context.Attendances
                        .Where(a =>
                            a.StudentId == student.Id)
                        .OrderByDescending(a => a.Date)
                        .FirstOrDefault();

                    return new AttendanceViewModel
                    {
                        StudentId = student.Id,
                        Name = student.Name,
                        RollNo = student.RollNo,
                        PhotoPath = student.PhotoPath,
                        Status = latestAttendance != null
                            ? latestAttendance.Status
                            : "Present"
                    };
                }).ToList();

            return View(attendanceData);
        }

        public IActionResult History()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var attendanceList =
                _context.Attendances
                .OrderByDescending(a => a.Date)
                .ToList();

            return View(attendanceList);
        }

        public IActionResult ClearHistory()
        {
            _context.Attendances.RemoveRange(
                _context.Attendances);

            _context.SaveChanges();

            return RedirectToAction(
                "History");
        }

        [HttpPost]
        public IActionResult Save(List<string> status)
        {
            var students =
                _context.Students.ToList();

            for (int i = 0; i < students.Count; i++)
            {
                DateTime today =
                    DateTime.Today;

                var existingAttendance =
                    _context.Attendances
                    .FirstOrDefault(a =>
                        a.StudentId ==
                        students[i].Id &&
                        a.Date.Date == today);

                if (existingAttendance != null)
                {
                    existingAttendance.Status =
                        status[i];

                    existingAttendance.Date =
                        DateTime.Now;
                }
                else
                {
                    Attendance attendance =
                        new Attendance()
                        {
                            StudentId =
                                students[i].Id,

                            StudentName =
                                students[i].Name,

                            RollNo =
                                students[i].RollNo,

                            Status =
                                status[i],

                            Date =
                                DateTime.Now
                        };

                    _context.Attendances.Add(
                        attendance);
                }
            }

            _context.SaveChanges();

            TempData["Success"] =
                "Attendance Saved Successfully";

            return RedirectToAction(
                "History");
        }
    }
}