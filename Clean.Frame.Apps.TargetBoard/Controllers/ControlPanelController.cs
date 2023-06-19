using Microsoft.AspNetCore.Mvc;

namespace Clean.Frame.Apps.TargetBoard.Controllers
{
    public class ControlPanelController : Controller
    {
        public ControlPanelController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
