using System.Text.Json.Serialization;

namespace Domain.Entity.VehicleEntity
{
    public class VehicleBrand : BaseClass
    {
        public string? Location { get; set; }
        [JsonIgnore]
        public ICollection<Vehicle>? Vehicles { get; set; }
    }
}
