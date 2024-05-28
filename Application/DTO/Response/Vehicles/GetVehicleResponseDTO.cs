
namespace Application.DTO.Response.Vehicles
{
    public class GetVehicleResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int VehicleOwnerId { get; set; }
        public int VehicleBrandId { get; set; }
        public GetVehicleBrandResponseDTO VehicleBrand { get; set; }
        public GetVehicleOwnerResponseDTO VehicleOwner { get; set; }
    }
}
