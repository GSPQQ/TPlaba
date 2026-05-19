using Microsoft.AspNetCore.Mvc;

namespace laba3.Controllers
{
    public interface IController
    {
        ControllerContext ControllerContext { get; set; }
        IActionResult Execute(string actionName, int id);
    }

    public class CustomIController : IController
    {
        public ControllerContext ControllerContext { get; set; } = null!;

        public IActionResult Execute(string actionName, int id)
        {
            if (string.Equals(actionName, "start", StringComparison.OrdinalIgnoreCase) && id == 0)
            {
                return new RedirectToActionResult("Index", "Simple", null);
            }

            var request = ControllerContext.HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}";

            return new ContentResult
            {
                Content = $"Ошибка: условие не выполнено (действие должно быть «start», id = 0).\r\n{url}",
                ContentType = "text/plain; charset=utf-8"
            };
        }
    }

    public class SimpleController : Controller
    {
        [laba3.Filters.CustomAuthorize]
        [ServiceFilter(typeof(laba3.Filters.HandleErrorAttribute))]
        [ServiceFilter(typeof(laba3.Filters.FormatExceptionFilterAttribute))]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [laba3.Filters.CustomAuthorize]
        [ServiceFilter(typeof(laba3.Filters.HandleErrorAttribute))]
        [ServiceFilter(typeof(laba3.Filters.FormatExceptionFilterAttribute))]
        [ServiceFilter(typeof(laba3.Filters.PostIndexActionFilter))]
        public IActionResult Index(int? Id, string ExhibitName)
        {
            if (!Id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var originYear = Request.Form["OriginYear"].ToString();
            var isOnDisplayRaw = Request.Form["IsOnDisplay"].ToString();
            var acquisitionDate = Request.Form["AcquisitionDate"].ToString();
            var description = Request.Form["Description"].ToString();

            bool isOnDisplay = false;
            if (!string.IsNullOrEmpty(isOnDisplayRaw))
            {
                isOnDisplay = isOnDisplayRaw.StartsWith("true") || isOnDisplayRaw == "on" || isOnDisplayRaw == "true,false";
            }

            ViewBag.Id = Id;
            ViewBag.ExhibitName = ExhibitName;
            ViewBag.OriginYear = originYear;
            ViewBag.IsOnDisplay = isOnDisplay;
            ViewBag.AcquisitionDate = acquisitionDate;
            ViewBag.Description = description;

            return View("Display");
        }
    }
}
