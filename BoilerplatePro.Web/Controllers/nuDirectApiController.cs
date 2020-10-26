using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BoilerplatePro.nuDirect;
using log4net;
using Microsoft.Owin.Security;
using Newtonsoft.Json;

namespace BoilerplatePro.Web.Controllers
{
    public class nuDirectApiController : Controller
    {
        static string AuthToken = null;
        static string ApiBaseUrl = null;
        static DateTime AuthExpiry = DateTime.Now;

        ILog log = LogManager.GetLogger("nuDirectApiErrors");

        // GET: nuDirectApi
        private async Task<string> GetAuthenticationToken()
        {
            ApiBaseUrl = ConfigurationManager.AppSettings["nuDirectApiUrlBase"];

            if (string.IsNullOrEmpty(AuthToken) || AuthExpiry < DateTime.Now)
            {
                using (HttpClient client = new HttpClient())
                {

                    var formContent = new FormUrlEncodedContent(new[]
                     {
                        new KeyValuePair<string, string>("username", ConfigurationManager.AppSettings["nuDirectUsername"]),
                        new KeyValuePair<string, string>("password", ConfigurationManager.AppSettings["nuDirectPassword"]),
                        new KeyValuePair<string, string>("grant_type", "password"),
                    });

                    string response = await(await client.PostAsync(ApiBaseUrl + "Authenticate", formContent)).Content.ReadAsStringAsync();

                    AuthenticationResponse responseFields = JsonConvert.DeserializeObject<AuthenticationResponse>(response);

                    if (!string.IsNullOrEmpty(responseFields.error))
                    {
                        log.Fatal("Error authenticating with the API: " + responseFields.error);
                    }

                    AuthToken = responseFields.access_token;
                    AuthExpiry = DateTime.Now.AddSeconds(Convert.ToInt32(responseFields.expires_in));
                }

            }

            return AuthToken;
        }

        public async Task<List<EndpointEntities>> GetAllEndpoints()
        {
            List<EndpointEntities> endpoints = new List<EndpointEntities>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await GetAuthenticationToken());

                string response = await(await client.GetAsync(ApiBaseUrl + "Endpoints")).Content.ReadAsStringAsync();

                endpoints = JsonConvert.DeserializeObject<List<EndpointEntities>>(response);
            }

            return endpoints;
        }

        public async Task<List<TransferEntities>> GetAllTransfers(DateTime? startDate = null, DateTime? endDate = null)
        {
            List<TransferEntities> transfers = new List<TransferEntities>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await GetAuthenticationToken());

                StringBuilder requestUrlBuilder = new StringBuilder(ApiBaseUrl + "Transfers");

                if (startDate != null) {
                    requestUrlBuilder.Append("?StartDate=" + startDate.Value.ToString("yyyy-MM-dd"));
                    requestUrlBuilder.Append("&EndDate=" + endDate.Value.ToString("yyyy-MM-dd"));
                }

                string response = await (await client.GetAsync(requestUrlBuilder.ToString())).Content.ReadAsStringAsync();

                transfers = JsonConvert.DeserializeObject<List<TransferEntities>>(response);
            }

            return transfers;
        }

        public async Task<List<FolderEntities>> GetAllFolders()
        {
            List<FolderEntities> folders = new List<FolderEntities>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await GetAuthenticationToken());

                string response = await (await client.GetAsync(ApiBaseUrl + "Folders")).Content.ReadAsStringAsync();

                folders = JsonConvert.DeserializeObject<List<FolderEntities>>(response);
            }

            return folders;
        }

        private class AuthenticationResponse
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string error { get; set; }
        }
    }
}