using Application.DTO.Request.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Response.Vehicles
{
    public class GetVehicleOwnerResponseDTO : CreateVehicleOwnerRequestDTO
    {
        public int Id { get; set; }
        public virtual ICollection<GetVehicleResponseDTO> Vehicles { get; set; }
    }
}
