using Application.DTO.Request.Vehicles;
using Application.DTO.Response;
using Application.DTO.Response.Vehicles;
using Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VehicleService(HttpClientService httpClientService) : IVehicleService
    {
        private async Task<HttpClient> PrivateClient() => (await httpClientService.GetPrivateClient());

        private static string CheckResponseStatus(HttpResponseMessage response) {
            if (!response.IsSuccessStatusCode)
                return $"Sorry unknown error occured";
            else
                return null;
        }

        private static GeneralResponse ErrorOperation(string message) => new(false, message);

        // ADD:
        public async Task<GeneralResponse> AddVehicle(CreateVehicleRequestDTO model) {
            var result = await (await PrivateClient()).PostAsJsonAsync(Constant.AddVehicleRoute, model);
            if(!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }

        public async Task<GeneralResponse> AddVehicleBrand(CreateVehicleBrandRequestDTO model)
        {
            var result = await (await PrivateClient()).PostAsJsonAsync(Constant.AddVehicleBrandRoute, model);
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }

        public async Task<GeneralResponse> AddVehicleOwner(CreateVehicleOwnerRequestDTO model)
        {
            var result = await (await PrivateClient()).PostAsJsonAsync(Constant.AddVehicleOwnerRoute, model);
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }

        // DELETE:

        public async Task<GeneralResponse> DeleteVehicle(int id)
        {
            var result = await (await PrivateClient()).DeleteAsync($"{Constant.DeleteVehicleRoute}/{id}");
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }
        public async Task<GeneralResponse> DeleteVehicleBrand(int id)
        {
            var result = await (await PrivateClient()).DeleteAsync($"{Constant.DeleteVehicleBrandRoute}/{id}");
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }
        public async Task<GeneralResponse> DeleteVehicleOwner(int id)
        {
            var result = await (await PrivateClient()).DeleteAsync($"{Constant.DeleteVehicleOwnerRoute}/{id}");
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }

        // GET BY ID:

        public async Task<GetVehicleResponseDTO> GetVehicle(int id) => await (await PrivateClient()).GetFromJsonAsync<GetVehicleResponseDTO>($"{Constant.GetVehicleRoute}/{id}");
        public async Task<GetVehicleBrandResponseDTO> GetVehicleBrand(int id) => await (await PrivateClient()).GetFromJsonAsync<GetVehicleBrandResponseDTO>($"{Constant.GetVehicleBrandRoute}/{id}");
        public async Task<GetVehicleOwnerResponseDTO> GetVehicleOwner(int id) => await (await PrivateClient()).GetFromJsonAsync<GetVehicleOwnerResponseDTO>($"{Constant.GetVehicleOwnerRoute}/{id}");

        // GET ALL:

        public async Task<IEnumerable<GetVehicleResponseDTO>> GetVehicles() => await (await PrivateClient()).GetFromJsonAsync<IEnumerable<GetVehicleResponseDTO>>(Constant.GetVehiclesRoute);
        public async Task<IEnumerable<GetVehicleBrandResponseDTO>> GetVehicleBrands() => await (await PrivateClient()).GetFromJsonAsync<IEnumerable<GetVehicleBrandResponseDTO>>(Constant.GetVehicleBrandsRoute);
        public async Task<IEnumerable<GetVehicleOwnerResponseDTO>> GetVehicleOwners() => await (await PrivateClient()).GetFromJsonAsync<IEnumerable<GetVehicleOwnerResponseDTO>>(Constant.GetVehicleOwnersRoute);

        // UPDATE:
        public async Task<GeneralResponse> UpdateVehicle(UpdateVehicleRequestDTO model) {
            var result = await (await PrivateClient()).PutAsJsonAsync(Constant.UpdateVehicleRoute, model);
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }
        public async Task<GeneralResponse> UpdateVehicleBrand(UpdateVehicleBrandRequestDTO model)
        {
            var result = await (await PrivateClient()).PutAsJsonAsync(Constant.UpdateVehicleBrandRoute, model);
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }
        public async Task<GeneralResponse> UpdateVehicleOwner(UpdateVehicleOwnerRequestDTO model)
        {
            var result = await (await PrivateClient()).PutAsJsonAsync(Constant.UpdateVehicleOwnerRoute, model);
            if (!string.IsNullOrEmpty(CheckResponseStatus(result))) return ErrorOperation(CheckResponseStatus(result));
            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }
    }
}
