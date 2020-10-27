using BoilerplatePro.nuDirect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BoilerplatePro.Web.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public async Task<ActionResult> Index()
        {
            List<CustomerEntities> customers = await new nuDirectApiController().GetAllCustomers();

            return View(customers);
        }

        public async Task<ActionResult> ViewCustomer(int Id)
        {
            CustomerEntities customers = (await new nuDirectApiController().GetAllCustomers()).FirstOrDefault(x=>x.CustomerId == Id);
            ViewBag.Endpoints = (await new nuDirectApiController().GetAllEndpoints()).Where(x => x.CustomerId == customers.CustomerId);

            return View(customers);
        }
    }
}