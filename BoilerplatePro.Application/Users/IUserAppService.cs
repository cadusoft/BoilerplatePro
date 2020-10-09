using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BoilerplatePro.Roles.Dto;
using BoilerplatePro.Users.Dto;

namespace BoilerplatePro.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}