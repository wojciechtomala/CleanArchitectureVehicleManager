namespace Domain.Entity.VehicleEntity
{
    public class Vehicle : BaseClass
    {
        public string? Description { get; set; }

        //public decimal Price { get; set; }
        public VehicleOwner? VehicleOwner { get; set; }
        public int VehicleOwnerId { get; set; }
        public VehicleBrand? VehicleBrand { get; set; }
        public int VehicleBrandId { get; set; }
    }
}
