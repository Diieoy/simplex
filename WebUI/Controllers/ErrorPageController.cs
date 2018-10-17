using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ErrorPageController : Controller
    {
        public ActionResult ErrorMessage()
        {
            return View();
        }
    }
}