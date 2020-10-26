using BoilerplatePro.nuDirect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoilerplatePro.Web.Models.Home
{
    public class HomeViewModel
    {
        public List<EndpointEntities> Endpoints { get; set; }

        public int TwentyFourHourTransfersCount { get; set; }
        public int WaitingTransfersCount { get; set; }
        public int CostCenterCount { get; internal set; }
        public string TenHourTransfers { get; internal set; }
        public int[] ThreeDayTransferCounts { get; internal set; }
        public int[] TransfersByStatus { get; internal set; }
        public List<CostCenterCountModel> MostActiveCostCenters { get; internal set; }
    }

    public class CostCenterCountModel
    {
        public string CostCenterName { get; set; }
        public int CostCenterCount { get; set; }
    }
}