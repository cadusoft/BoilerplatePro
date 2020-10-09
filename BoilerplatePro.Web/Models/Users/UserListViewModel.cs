using System.Collections.Generic;
using BoilerplatePro.Roles.Dto;
using BoilerplatePro.Users.Dto;

namespace BoilerplatePro.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}