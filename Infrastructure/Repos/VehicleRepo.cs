using Application.Contracts;
using Application.DTO.Request.Vehicles;
using Application.DTO.Response;
using Application.DTO.Response.Vehicles;
using Domain.Entity.VehicleEntity;
using Infrastructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace Infrastructure.Repos
{
    internal class VehicleRepo(AppDbContext context) : IVehicle
    {
        // FILTERING:
        private async Task<Vehicle> FindVehicleByName(string name) => await context.Vehicles.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        private async Task<VehicleBrand> FindVehicleBrandByName(string name) => await context.VehicleBrands.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        private async Task<VehicleOwner> FindVehicleOwnerByName(string name) => await context.VehicleOwners.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        private async Task<Vehicle> FindVehicleById(int id) => await context.Vehicles.Include(b => b.VehicleBrand).Include(o => o.VehicleOwner).FirstOrDefaultAsync(x => x.Id == id);
        private async Task<VehicleBrand> FindVehicleBrandById(int id) => await context.VehicleBrands.FirstOrDefaultAsync(x => x.Id == id);
        private async Task<VehicleOwner> FindVehicleOwnerById(int id) => await context.VehicleOwners.FirstOrDefaultAsync(x => x.Id == id);

        // SAVE CHANGES:
        private async Task SaveChangesAsync() => await context.SaveChangesAsync();

        // EXT:
        private static GeneralResponse NullResponse(string message) => new(false, message);
        private static GeneralResponse AlreadyExistResponse(string message) => new(false, message);
        private static GeneralResponse OperationSuccessResponse(string message) => new(false, message);

        // ADD:
        public async Task<GeneralResponse> AddVehicle(CreateVehicleRequestDTO model) {
            if (await FindVehicleByName(model.Name) is not null) return AlreadyExistResponse("Vehicle already exist");
            context.Vehicles.Add(model.Adapt(new Vehicle()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle saved successfully");
        }

        public async Task<GeneralResponse> AddVehicleBrand(CreateVehicleBrandRequestDTO model) {
            if (await FindVehicleBrandByName(model.Name) is not null) return AlreadyExistResponse("Vehicle Brand already exist");
            context.VehicleBrands.Add(model.Adapt(new VehicleBrand()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Brand saved successfully");
        }

        public async Task<GeneralResponse> AddVehicleOwner(CreateVehicleOwnerRequestDTO model) {
            if (await FindVehicleOwnerByName(model.Name) is not null) return AlreadyExistResponse("Vehicle Owner already exist");
            context.VehicleOwners.Add(model.Adapt(new VehicleOwner()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Owner saved successfully");
        }

        // DELETE:
        public async Task<GeneralResponse> DeleteVehicle(int id) {
            if (await FindVehicleById(id) is null) return NullResponse("Vehicle not found");
            context.Vehicles.Remove(await FindVehicleById(id));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle deleted successfully");
        }

        public async Task<GeneralResponse> DeleteVehicleBrand(int id) {
            if (await FindVehicleBrandById(id) is null) return NullResponse("Vehicle Brand not found");
            context.VehicleBrands.Remove(await FindVehicleBrandById(id));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Brand deleted successfully");
        }

        public async Task<GeneralResponse> DeleteVehicleOwner(int id) {
            if (await FindVehicleOwnerById(id) is null) return NullResponse("Vehicle Owner not found");
            context.VehicleOwners.Remove(await FindVehicleOwnerById(id));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Owner deleted successfully");
        }

        // GET BY ID:

        public async Task<GetVehicleResponseDTO> GetVehicle(int id) => (await FindVehicleById(id)).Adapt(new GetVehicleResponseDTO());
        public async Task<GetVehicleBrandResponseDTO> GetVehicleBrand(int id) => (await FindVehicleBrandById(id)).Adapt(new GetVehicleBrandResponseDTO());
        public async Task<GetVehicleOwnerResponseDTO> GetVehicleOwner(int id) => (await FindVehicleOwnerById(id)).Adapt(new GetVehicleOwnerResponseDTO());

        // GET ALL:
        public async Task<IEnumerable<GetVehicleResponseDTO>> GetVehicles() {
            var data = (await context.Vehicles.Include(b => b.VehicleBrand).Include(o => o.VehicleOwner).ToListAsync());
            return data.Select(vehicle => new GetVehicleResponseDTO
            {
                Id = vehicle.Id,
                Name = vehicle.Name,
                Description = vehicle.Description,
                VehicleOwnerId = vehicle.VehicleOwnerId,
                VehicleBrandId = vehicle.VehicleBrandId,
                VehicleBrand = new GetVehicleBrandResponseDTO() {
                    Id = vehicle.VehicleBrand.Id,
                    Name = vehicle.VehicleBrand.Name,
                    Location = vehicle.VehicleBrand.Location
                },
                VehicleOwner = new GetVehicleOwnerResponseDTO()
                {
                    Id = vehicle.VehicleOwner.Id,
                    Name = vehicle.VehicleOwner.Name,
                    Address = vehicle.VehicleOwner.Address
                }
            }).ToList();
        }

        public async Task<IEnumerable<GetVehicleBrandResponseDTO>> GetVehicleBrands() => (await context.VehicleBrands.ToListAsync()).Adapt<List<GetVehicleBrandResponseDTO>>();
        public async Task<IEnumerable<GetVehicleOwnerResponseDTO>> GetVehicleOwners() => (await context.VehicleOwners.ToListAsync()).Adapt<List<GetVehicleOwnerResponseDTO>>();

        // UPDATE:

        public async Task<GeneralResponse> UpdateVehicle(UpdateVehicleRequestDTO model){
            if (await FindVehicleById(model.Id) is null) return NullResponse("Vehicle not found");
            context.Entry(await FindVehicleById(model.Id)).State = EntityState.Detached;
            context.Vehicles.Update(model.Adapt(new Vehicle()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle data updated successfully");
        }

        public async Task<GeneralResponse> UpdateVehicleBrand(UpdateVehicleBrandRequestDTO model) { 
            if(await FindVehicleBrandById(model.Id) is null) return NullResponse("Vehicle Brand not found");
            context.Entry(await FindVehicleBrandById(model.Id)).State = EntityState.Detached;
            context.VehicleBrands.Update(model.Adapt(new VehicleBrand()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Brand updated successfully");
        }

        public async Task<GeneralResponse> UpdateVehicleOwner(UpdateVehicleOwnerRequestDTO model)
        {
            if (await FindVehicleOwnerById(model.Id) is null) return NullResponse("Vehicle Owner not found");
            context.Entry(await FindVehicleOwnerById(model.Id)).State = EntityState.Detached;
            context.VehicleOwners.Update(model.Adapt(new VehicleOwner()));
            await SaveChangesAsync();
            return OperationSuccessResponse("Vehicle Owner updated successfully");
        }
    }
}
