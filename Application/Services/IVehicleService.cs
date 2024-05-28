using Application.DTO.Request.Vehicles;
using Application.DTO.Response;
using Application.DTO.Response.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IVehicleService
    {
        Task<GeneralResponse> AddVehicle(CreateVehicleRequestDTO model);
        Task<IEnumerable<GetVehicleResponseDTO>> GetVehicles();
        Task<GetVehicleResponseDTO> GetVehicle(int id);
        Task<GeneralResponse> DeleteVehicle(int id);
        Task<GeneralResponse> UpdateVehicle(UpdateVehicleRequestDTO model);

        Task<GeneralResponse> AddVehicleBrand(CreateVehicleBrandRequestDTO model);
        Task<IEnumerable<GetVehicleBrandResponseDTO>> GetVehicleBrands();
        Task<GetVehicleBrandResponseDTO> GetVehicleBrand(int id);
        Task<GeneralResponse> DeleteVehicleBrand(int id);
        Task<GeneralResponse> UpdateVehicleBrand(UpdateVehicleBrandRequestDTO model);

        Task<GeneralResponse> AddVehicleOwner(CreateVehicleOwnerRequestDTO model);
        Task<IEnumerable<GetVehicleOwnerResponseDTO>> GetVehicleOwners();
        Task<GetVehicleOwnerResponseDTO> GetVehicleOwner(int id);
        Task<GeneralResponse> DeleteVehicleOwner(int id);
        Task<GeneralResponse> UpdateVehicleOwner(UpdateVehicleOwnerRequestDTO model);
    }
}
