using Application.DTO.Request.Account;
using Application.DTO.Response;
using Application.DTO.Response.Account;
using Application.Extensions;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class AccountService(HttpClientService httpClientService) : IAccountService
    {
        public async Task<LoginResponse> LoginAccountAsync(LoginDTO model) {
            try
            {
                var publicClient = httpClientService.GetPublicClient();
                var response = await publicClient.PostAsJsonAsync(Constant.LoginRoute, model);
                string error = CheckResponseStatus(response);
                if (!string.IsNullOrEmpty(error))
                    return new LoginResponse(Flag: false, Message: error);
                
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result!;
            }
            catch (Exception ex) { 
                return new LoginResponse(Flag: false, Message: ex.Message);
            }
        }

        public async Task<GeneralResponse> RegisterAccountAsync(CreateAccountDTO model) {
            try {
                var publicClient = httpClientService.GetPublicClient();
                var response = await publicClient.PostAsJsonAsync(Constant.RegisterRoute, model);
                string error = CheckResponseStatus(response);
                if(!string.IsNullOrEmpty(error)) return new GeneralResponse(Flag: false, Message: error);
                var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
                return result!;
            }
            catch (Exception ex)
            {
                return new GeneralResponse(Flag: false, Message: ex.Message);
            }
        }

        private static string CheckResponseStatus(HttpResponseMessage response) {
            if (!response.IsSuccessStatusCode)
                return $"Unknown error has occured. {Environment.NewLine} Error: {Environment.NewLine} Status: {response.StatusCode} {Environment.NewLine} Reasone Phrase: {response.ReasonPhrase}";
            else
                return null;
        }

        public async Task CreateAdminAtFirstStart() {
            try
            {
                var client = httpClientService.GetPublicClient();
                await client.PostAsync(Constant.CreateAdminRoute, null);
            }
            catch { 
            
            }
        }

        public async Task<IEnumerable<GetRoleDTO>> GetRolesAsync() {
            try {
                var privateClient = await httpClientService.GetPrivateClient();
                var response = await privateClient.GetAsync(Constant.GetRolesRoute);
                string error = CheckResponseStatus(response);
                if(!string.IsNullOrEmpty(error))
                    throw new Exception(error);
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<GetRoleDTO>>();
                return result!;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /*
            public IEnumerable<GetRoleDTO> GetDefaultRoles() { 
                var list = new List<GetRoleDTO>();
                list?.Clear();
                list.Add(new GetRoleDTO(1.ToString(), Constant.Role.Admin));
                list.Add(new GetRoleDTO(2.ToString(), Constant.Role.User));
                return list;
            }
        */

        public async Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync()
        {
            try {
                var privateClient = await httpClientService.GetPrivateClient();
                var response = await privateClient.GetAsync(Constant.GetUserWithRolesRoute);
                string error = CheckResponseStatus(response);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<GetUsersWithRolesResponseDTO>>();
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GeneralResponse> ChangeUserRole(ChangeUserRoleRequestDTO model) {
            try
            {
                var privateClient = await httpClientService.GetPrivateClient();
                var response = await privateClient.PostAsJsonAsync(Constant.ChangUserRoleRoute, model);
                string error = CheckResponseStatus(response);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);
                var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
                return result!;
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }
        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model)
        {
            try { 
                var publicClient = httpClientService.GetPublicClient();
                var response = await publicClient.PostAsJsonAsync(Constant.RefreshTokenRoute, model);
                string error = CheckResponseStatus(response);
                if (!string.IsNullOrEmpty(error)) return new LoginResponse(false, error);
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result!;
            }
            catch (Exception ex) {
                return new LoginResponse(false, ex.Message); 
            }
        }

        public Task CreateAdmin()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateAccountAsync(CreateAccountDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> CreateRoleAsync(CreateRoleDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleRequestDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
