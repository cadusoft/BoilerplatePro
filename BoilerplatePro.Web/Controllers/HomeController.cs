using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using BoilerplatePro.nuDirect;
using BoilerplatePro.Web.Models.Home;
using Newtonsoft.Json;

namespace BoilerplatePro.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : BoilerplateProControllerBase
    {
        public async Task<ActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel();

            List<EndpointEntities> endpoints = await (new nuDirectApiController().GetAllEndpoints());
            List<FolderEntities> folders = await (new nuDirectApiController().GetAllFolders());
            List<TransferEntities> transfers = await (new nuDirectApiController().GetAllTransfers(DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["DashboardDateRange"])*-1), DateTime.Now));

            model.Endpoints = endpoints;
            model.TwentyFourHourTransfersCount = transfers.Where(x => x.Started > DateTime.Now.AddHours(-24)).Count();
            model.WaitingTransfersCount = transfers.Where(x => x.CurrentStatus == 2).Count();
            model.CostCenterCount = folders.Select(x=>x.CostCentreCode).Distinct().Count();

            StringBuilder tenHourTransfersSb = new StringBuilder();

            for(int i = 10; i > 0; i--)
            {
                tenHourTransfersSb.Append(transfers.Where(x => x.Started > DateTime.Now.AddHours(i * -1) && x.Started < DateTime.Now.AddHours((i - 1)*-1)).Count());
                tenHourTransfersSb.Append(",");
            }

            model.TenHourTransfers = tenHourTransfersSb.ToString().TrimEnd(',');
            model.ThreeDayTransferCounts = new int[3];

            for(int i = 2; i > -1; i--)
            {
                model.ThreeDayTransferCounts[i] = transfers.Where(x => x.Started > DateTime.Now.AddDays((i + 1)*-1) && x.Started < DateTime.Now.AddDays(i*-1)).Count();
            }

            model.MostActiveCostCenters = new List<CostCenterCountModel>();
            int[] sourceFolders = transfers.Select(x => x.SourceFolderId).Distinct().ToArray();
            
            foreach(int i in sourceFolders)
            {
                FolderEntities currentFolder = folders.FirstOrDefault(z => z.FolderId == i);
                int costCenterCount = transfers.Where(x => x.SourceFolderId == i).Count();

                if (string.IsNullOrEmpty(currentFolder.CostCentreCode))
                    continue;

                if (model.MostActiveCostCenters.Where(x=>x.CostCenterName == currentFolder.CostCentreCode).Count() == 0)
                {
                    model.MostActiveCostCenters.Add(new CostCenterCountModel() { CostCenterName = currentFolder.CostCentreCode, CostCenterCount = costCenterCount });
                } else
                {
                    model.MostActiveCostCenters.FirstOrDefault(x => x.CostCenterName == currentFolder.CostCentreCode).CostCenterCount += costCenterCount;
                }
            }

            model.MostActiveCostCenters = model.MostActiveCostCenters.OrderByDescending(x => x.CostCenterCount).Take(3).ToList();

            model.TransfersByStatus = new int[3];
            model.TransfersByStatus[0] = transfers.Where(x => x.CurrentStatus == 9).Count();
            model.TransfersByStatus[1] = transfers.Where(x => x.CurrentStatus == 2).Count();
            model.TransfersByStatus[2] = transfers.Where(x => x.CurrentStatus == 99).Count();

            return View(model);
        }

        

	}
}