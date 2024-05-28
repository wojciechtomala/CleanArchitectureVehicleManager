namespace Application.Extensions
{
    public static class Constant
    {
        public const string RegisterRoute = "api/account/identity/create";
        public const string LoginRoute = "api/account/identity/login";
        public const string RefreshTokenRoute = "api/account/identity/refresh-token";
        public const string GetRolesRoute = "api/account/identity/role/list";
        public const string CreateRoleRoute = "api/account/identity/role/create";
        public const string CreateAdminRoute = "setting";
        public const string BrowserStorageKey = "x-key";
        public const string HttpClientName = "WebUIClient";
        public const string HttpClientHeaderScheme = "Bearer";
        public const string AuthenticationType = "JwtAuth";
        public const string GetUserWithRolesRoute = "api/account/identity/users-with-roles";
        public const string ChangUserRoleRoute = "api/account/identity/change-role";
        public static class Role { 
            public const string Admin = "Admin";
            public const string User = "User";
        }

        // VEHICLE:
        public const string AddVehicleRoute = "api/vehicle/add-vehicle";
        public const string AddVehicleBrandRoute = "api/vehicle/add-vehicle-brand";
        public const string AddVehicleOwnerRoute = "api/vehicle/add-vehicle-owner";

        public const string GetVehicleRoute = "api/vehicle/get-vehicle";
        public const string GetVehicleBrandRoute = "api/vehicle/get-vehicle-brand";
        public const string GetVehicleOwnerRoute = "api/vehicle/get-vehicle-owner";

        public const string GetVehiclesRoute = "api/vehicle/get-vehicles";
        public const string GetVehicleBrandsRoute = "api/vehicle/get-vehicle-brands";
        public const string GetVehicleOwnersRoute = "api/vehicle/get-vehicle-owners";

        public const string UpdateVehicleRoute = "api/vehicle/update-vehicle";
        public const string UpdateVehicleBrandRoute = "api/vehicle/update-vehicle-brand";
        public const string UpdateVehicleOwnerRoute = "api/vehicle/update-vehicle-owner";

        public const string DeleteVehicleRoute = "api/vehicle/delete-vehicle";
        public const string DeleteVehicleBrandRoute = "api/vehicle/delete-vehicle-brand";
        public const string DeleteVehicleOwnerRoute = "api/vehicle/delete-vehicle-owner";
    }
}
