using Application.DTO.Request.Account;
using Application.DTO.Response.Account;
using Application.DTO.Response;

namespace Application.Services
{
    public interface IAccountService
    {
        Task CreateAdmin();
        Task<GeneralResponse> CreateAccountAsync(CreateAccountDTO model);
        Task<LoginResponse> LoginAccountAsync(LoginDTO model);
        Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model);
        Task<GeneralResponse> CreateRoleAsync(CreateRoleDTO model);
        Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync();
        Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleRequestDTO model);
        Task<IEnumerable<GetRoleDTO>> GetRolesAsync();
    }
}
