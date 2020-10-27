using BoilerplatePro.nuDirect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
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
            List<EndpointEntities> endpoints = await new nuDirectApiController().GetAllEndpoints();

            return View(endpoints.OrderBy(x=>x.LastSeenDate));
        }

        public async Task<ActionResult> ShowEndpointData(int id)
        {
            List<EndpointEntities> endpointEntities = await new nuDirectApiController().GetAllEndpoints();
            EndpointEntities endpoint = endpointEntities.FirstOrDefault(x => x.EndpointId == id);

            ViewBag.PingResponseTime = "-";
            ViewBag.PingStatus = "-";
            ViewBag.PingReplyAddress = "-";

            ViewBag.SimilarEndpoints = endpointEntities.Where(x => x.KnownIP == endpoint.KnownIP && x.EndpointId != endpoint.EndpointId);

            if (!string.IsNullOrEmpty(endpoint.KnownIP))
            {
                try
                {
                    Ping myPing = new Ping();
                    PingReply reply = myPing.Send(endpoint.KnownIP, 1000);
                    if (reply != null)
                    {
                        Console.WriteLine("Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address);
                        //Console.WriteLine(reply.ToString());

                        ViewBag.PingResponseTime = reply.RoundtripTime.ToString();
                        ViewBag.PingStatus = reply.Status;
                        ViewBag.PingReplyAddress = reply.Address;
                    }
                }
                catch
                {
                    Console.WriteLine("ERROR: You have Some TIMEOUT issue");
                }
            }

            return View(endpoint);
        }
    }
}