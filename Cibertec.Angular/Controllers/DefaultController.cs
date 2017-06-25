using System.Web.Mvc;

namespace Cibertec.Angular.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}