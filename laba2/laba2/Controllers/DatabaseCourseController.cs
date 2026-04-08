using laba2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace laba2.Controllers
{
    public class DatabaseCourseController : Controller
    {
        private static readonly List<DatabaseCourseModel> Courses = new()
        {
            new DatabaseCourseModel
            {
                Id = 1,
                Name = "Краеведческий музей им. Петрова",
                Curator = "Иванов И.И.",
                Established = new DateTime(1950, 6, 1),
                Location = "г. Москва, ул. Ленина, 10",
                TicketPrice = 200m,
                IsGuidedToursAvailable = true,
                OpeningHours = "10:00 - 18:00",
                CollectionSize = 1200,
                Description = "Исторические экспонаты региона, археология и этнография.",
                ContactPhone = "+7 (495) 123-45-67",
                Website = "https://museum.example.com"
            },
            new DatabaseCourseModel
            {
                Id = 2,
                Name = "Музей краеведения Центрального района",
                Curator = "Петрова А.С.",
                Established = new DateTime(1978, 9, 15),
                Location = "г. Москва, пр. Мира, 5",
                TicketPrice = 150m,
                IsGuidedToursAvailable = true,
                OpeningHours = "09:00 - 17:00",
                CollectionSize = 800,
                Description = "Экспозиции, посвященные природе и культуре региона.",
                ContactPhone = "+7 (495) 987-65-43",
                Website = "https://central-museum.example.com"
            },
            new DatabaseCourseModel
            {
                Id = 3,
                Name = "Дом-музей народного творчества",
                Curator = "Сидоров Н.П.",
                Established = new DateTime(2001, 4, 20),
                Location = "г. Москва, наб. реки, 2",
                TicketPrice = 100m,
                IsGuidedToursAvailable = false,
                OpeningHours = "11:00 - 16:00",
                CollectionSize = 450,
                Description = "Коллекция народного творчества и ремесел.",
                ContactPhone = "+7 (495) 555-12-34",
                Website = "https://folk-museum.example.com"
            }
        };

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
                Established = DateTime.Today
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

            course.Name = model.Name;
            course.Curator = model.Curator;
            course.Established = model.Established;
            course.Location = model.Location;
            course.TicketPrice = model.TicketPrice;
            course.IsGuidedToursAvailable = model.IsGuidedToursAvailable;
            course.OpeningHours = model.OpeningHours;
            course.CollectionSize = model.CollectionSize;
            course.Description = model.Description;
            course.ContactPhone = model.ContactPhone;
            course.Website = model.Website;

            return RedirectToAction(nameof(Index));
        }
    }
}
