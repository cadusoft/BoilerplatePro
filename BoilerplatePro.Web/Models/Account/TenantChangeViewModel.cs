using Abp.AutoMapper;
using BoilerplatePro.Sessions.Dto;

namespace BoilerplatePro.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}