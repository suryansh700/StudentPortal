using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;

namespace StudentPortal.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public StudentController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(
            Student student,
            IFormFile PhotoFile)
        {
            if (PhotoFile != null)
            {
                string folder =
                    Path.Combine(
                        _environment.WebRootPath,
                        "uploads");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(
                        PhotoFile.FileName);

                string filePath =
                    Path.Combine(
                        folder,
                        fileName);

                using (var stream =
                    new FileStream(
                        filePath,
                        FileMode.Create))
                {
                    PhotoFile.CopyTo(stream);
                }

                student.PhotoPath =
                    "/uploads/" + fileName;
            }

            _context.Students.Add(student);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var student =
                _context.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            _context.Students.Update(student);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var student =
                _context.Students.Find(id);

            if (student != null)
            {
                _context.Students.Remove(student);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}