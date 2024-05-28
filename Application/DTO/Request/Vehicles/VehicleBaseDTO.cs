using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Request.Vehicles
{
    public class VehicleBaseDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="Select Vehicle Owner")]
        public int VehicleOwnerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Select Vehicle Brand")]
        public int VehicleBrandId { get; set; }
    }
}
