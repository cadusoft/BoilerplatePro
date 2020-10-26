using BoilerplatePro.nuDirect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BoilerplatePro.Web.Controllers
{
    public class EndpointsController : Controller
    {

        // GET: Endpoints
        public async Task<ActionResult> Index()
        {
            List<EndpointEntities> endpoints = new List<EndpointEntities>();
            
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["nuDirectAuthToken"]);

                string response = await (await client.GetAsync("https://fnbnutest.southafricanorth.cloudapp.azure.com/api/Endpoints")).Content.ReadAsStringAsync();

                endpoints = JsonConvert.DeserializeObject<List<EndpointEntities>>(response);
            }

            return View(endpoints);
        }
    }
}