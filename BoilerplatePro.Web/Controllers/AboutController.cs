using System.Web.Mvc;

namespace BoilerplatePro.Web.Controllers
{
    public class AboutController : BoilerplateProControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}