using Microsoft.AspNetCore.Mvc;

namespace laba3.Controllers
{
    // Entry point for CustomIController.Execute (action=start, id=0 → Simple/Index).
    public class StartController : Controller
    {
        public IActionResult Start(int id = 0)
        {
            var custom = new CustomIController
            {
                ControllerContext = ControllerContext
            };

            return custom.Execute("start", id);
        }
    }
}
