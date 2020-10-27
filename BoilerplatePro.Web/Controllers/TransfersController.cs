using BoilerplatePro.nuDirect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BoilerplatePro.Web.Controllers
{
    public class TransfersController : Controller
    {
        // GET: Transfers
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> TransfersByEntity(string StartDate, string EndDate, string EntityType)
        {
            if (string.IsNullOrEmpty(EntityType))
                return View(new List<EntityTransferCountModel>());

            List<EntityTransferCountModel> transferCounts = new List<EntityTransferCountModel>();

            List<TransferEntities> transfers = (await new nuDirectApiController().GetAllTransfers()).Where(x=>x.Started > DateTime.Parse(StartDate) && x.Started < DateTime.Parse(EndDate).AddDays(1)).ToList();

            if (EntityType == "Customer")
            {
                List<CustomerEntities> customers = await new nuDirectApiController().GetAllCustomers();
                List<EndpointEntities> endpoints = await new nuDirectApiController().GetAllEndpoints();

                int[] sourceEndpoints = transfers.Select(x => x.SourceEpId).Distinct().ToArray();

                foreach (int i in sourceEndpoints)
                {
                    EndpointEntities currentEndpoint = endpoints.FirstOrDefault(z => z.EndpointId == i);
                    int endpointCount = transfers.Where(x => x.SourceEpId == i).Count();

                    if (transferCounts.Where(x => x.EntityName == currentEndpoint.customer.Company).Count() == 0)
                    {
                        transferCounts.Add(new EntityTransferCountModel() { EntityName = currentEndpoint.customer.Company, EntityCount = endpointCount });
                    }
                    else
                    {
                        transferCounts.FirstOrDefault(x => x.EntityName == currentEndpoint.customer.Company).EntityCount += endpointCount;
                    }
                }
            }
            else if (EntityType == "CostCenter")
            {
                List<FolderEntities> folders = await new nuDirectApiController().GetAllFolders();

                int[] sourceFolders = transfers.Select(x => x.SourceFolderId).Distinct().ToArray();

                foreach (int i in sourceFolders)
                {
                    FolderEntities currentFolder = folders.FirstOrDefault(z => z.FolderId == i);
                    int FolderCount = transfers.Where(x => x.SourceFolderId == i).Count();

                    if (transferCounts.Where(x => x.EntityName == currentFolder.CostCentreCode).Count() == 0)
                    {
                        transferCounts.Add(new EntityTransferCountModel() { EntityName = currentFolder.CostCentreCode, EntityCount = FolderCount });
                    }
                    else
                    {
                        transferCounts.FirstOrDefault(x => x.EntityName == currentFolder.CostCentreCode).EntityCount += FolderCount;
                    }
                }
            }
            else
            {
                List<CustomerEntities> customers = await new nuDirectApiController().GetAllCustomers();
                List<EndpointEntities> endpoints = await new nuDirectApiController().GetAllEndpoints();

                int[] sourceEndpoints = transfers.Select(x => x.SourceEpId).Distinct().ToArray();

                foreach (int i in sourceEndpoints)
                {
                    EndpointEntities currentEndpoint = endpoints.FirstOrDefault(z => z.EndpointId == i);
                    int endpointCount = transfers.Where(x => x.SourceEpId == i).Count();

                    if (transferCounts.Where(x => x.EntityName == currentEndpoint.customer.BusinessUnit).Count() == 0)
                    {
                        transferCounts.Add(new EntityTransferCountModel() { EntityName = currentEndpoint.customer.BusinessUnit, EntityCount = endpointCount });
                    }
                    else
                    {
                        transferCounts.FirstOrDefault(x => x.EntityName == currentEndpoint.customer.BusinessUnit).EntityCount += endpointCount;
                    }
                }
            }

            return View(transferCounts);
        }
    }

    public class EntityTransferCountModel
    {
        public string EntityName { get; set; }
        public int EntityCount { get; set; }
    }
}