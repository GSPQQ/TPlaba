using laba2.Models;
using Microsoft.AspNetCore.Mvc;

namespace laba2.Controllers
{
    public class DatabaseCourseController : Controller
    {
        private static readonly List<DatabaseCourseModel> Courses =
        [
            new DatabaseCourseModel
            {
                Id = 1,
                Title = "Проектирование баз данных",
                Instructor = "Иванов И.И.",
                Credits = 5,
                StartDate = DateTime.Today.AddDays(7),
                TuitionFee = 15000m,
                IsExamRequired = true,
                Semester = "Весенний"
            },
            new DatabaseCourseModel
            {
                Id = 2,
                Title = "Администрирование СУБД",
                Instructor = "Петрова А.С.",
                Credits = 4,
                StartDate = DateTime.Today.AddDays(14),
                TuitionFee = 13500m,
                IsExamRequired = true,
                Semester = "Осенний"
            },
            new DatabaseCourseModel
            {
                Id = 3,
                Title = "SQL и оптимизация запросов",
                Instructor = "Сидоров Н.П.",
                Credits = 3,
                StartDate = DateTime.Today.AddDays(21),
                TuitionFee = 12000m,
                IsExamRequired = false,
                Semester = "Летний"
            }
        ];

        public IActionResult Index()
        {
            return View(Courses);
        }

        public IActionResult AllData()
        {
            return View(Courses);
        }

        public IActionResult Details(int id)
        {
            var course = Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new DatabaseCourseModel
            {
                StartDate = DateTime.Today
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DatabaseCourseModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Id = Courses.Count == 0 ? 1 : Courses.Max(c => c.Id) + 1;
            Courses.Add(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DatabaseCourseModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var course = Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            course.Title = model.Title;
            course.Instructor = model.Instructor;
            course.Credits = model.Credits;
            course.StartDate = model.StartDate;
            course.TuitionFee = model.TuitionFee;
            course.IsExamRequired = model.IsExamRequired;
            course.Semester = model.Semester;

            return RedirectToAction(nameof(Index));
        }
    }
}
