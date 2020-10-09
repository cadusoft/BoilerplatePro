using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace BoilerplatePro.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : BoilerplateProControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}