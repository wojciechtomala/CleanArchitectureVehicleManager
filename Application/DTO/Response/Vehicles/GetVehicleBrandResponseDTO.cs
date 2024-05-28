using Application.DTO.Request.Vehicles;

namespace Application.DTO.Response.Vehicles
{
    public class GetVehicleBrandResponseDTO : CreateVehicleBrandRequestDTO
    {
        public int Id { get; set; }
        public virtual ICollection<GetVehicleResponseDTO> Vehicles { get; set; } = null;
    }
}
