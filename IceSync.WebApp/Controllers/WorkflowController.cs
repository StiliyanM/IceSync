using Microsoft.AspNetCore.Mvc;

namespace IceSync.WebApp.Controllers
{
    public class WorkflowController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
