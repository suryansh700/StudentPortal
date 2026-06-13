using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;

namespace StudentPortal.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
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

            return View(students);
        }

        [HttpPost]
        public IActionResult Save(
            List<int> java,
            List<int> python,
            List<int> dbms)
        {
            var students =
                _context.Students.ToList();

            for (int i = 0; i < students.Count; i++)
            {
                double percentage =
                    (java[i] + python[i] + dbms[i]) / 3.0;

                string grade = "F";

                if (percentage >= 90)
                    grade = "A+";
                else if (percentage >= 80)
                    grade = "A";
                else if (percentage >= 70)
                    grade = "B";
                else if (percentage >= 60)
                    grade = "C";
                else
                    grade = "F";


                Result result =
                    new Result()
                    {
                        StudentId =
                            students[i].Id,

                        StudentName =
                            students[i].Name,

                        RollNo =
                            students[i].RollNo,

                        Java =
                            java[i],

                        Python =
                            python[i],

                        DBMS =
                            dbms[i],

                        Percentage =
                            percentage,

                        Grade =
                            grade
                    };

                _context.Results.Add(result);
            }

            _context.SaveChanges();

            TempData["Success"] =
                "Result Saved Successfully";

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

            var results =
                _context.Results
                .OrderByDescending(r => r.Id)
                .ToList();

            return View(results);
        }

        public IActionResult ClearHistory()
        {
            _context.Results.RemoveRange(
                _context.Results);

            _context.SaveChanges();

            return RedirectToAction(
                "History");
        }
    }
}