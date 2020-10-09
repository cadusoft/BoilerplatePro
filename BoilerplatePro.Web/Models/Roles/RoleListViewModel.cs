using System.Collections.Generic;
using BoilerplatePro.Roles.Dto;

namespace BoilerplatePro.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}