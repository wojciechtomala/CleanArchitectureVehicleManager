using Application.DTO.Request.Vehicles;
using Application.DTO.Response;
using Application.DTO.Response.Vehicles;

namespace Application.Contracts
{
    public interface IVehicle
    {
        // VEHICLE:
        Task<GeneralResponse> AddVehicle(CreateVehicleRequestDTO model);
        Task<IEnumerable<GetVehicleResponseDTO>> GetVehicles();
        Task<GetVehicleResponseDTO> GetVehicle(int id);
        Task<GeneralResponse> DeleteVehicle(int id);
        Task<GeneralResponse> UpdateVehicle(UpdateVehicleRequestDTO model);
        
        // VEHICLE BRAND:
        Task<GeneralResponse> AddVehicleBrand(CreateVehicleBrandRequestDTO model);
        Task<IEnumerable<GetVehicleBrandResponseDTO>> GetVehicleBrands();
        Task<GetVehicleBrandResponseDTO> GetVehicleBrand(int id);
        Task<GeneralResponse> DeleteVehicleBrand(int id);
        Task<GeneralResponse> UpdateVehicleBrand(UpdateVehicleBrandRequestDTO model);
       
        // OWNER:
        Task<GeneralResponse> AddVehicleOwner(CreateVehicleOwnerRequestDTO model);
        Task<IEnumerable<GetVehicleOwnerResponseDTO>> GetVehicleOwners();
        Task<GetVehicleOwnerResponseDTO> GetVehicleOwner(int id);
        Task<GeneralResponse> DeleteVehicleOwner(int id);
        Task<GeneralResponse> UpdateVehicleOwner(UpdateVehicleOwnerRequestDTO model);
    }
}
