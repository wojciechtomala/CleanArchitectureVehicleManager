using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Request.Vehicles
{
    public class CreateVehicleBrandRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
