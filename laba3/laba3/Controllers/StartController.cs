using Microsoft.AspNetCore.Mvc;

namespace laba3.Controllers
{
    
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
