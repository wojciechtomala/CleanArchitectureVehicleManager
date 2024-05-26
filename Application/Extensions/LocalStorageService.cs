using Application.DTO.Request.Account;
using NetcodeHub.Packages.Extensions.LocalStorage;
using System.Text.Json;

namespace Application.Extensions
{
    public class LocalStorageService(ILocalStorageService localStorageService) {
        private async Task<string> GetBrowserLocalStorage() {
            var tokenModel = await localStorageService.GetEncryptedItemAsStringAsync(Constant.BrowserStorageKey);
            return tokenModel;
        }

        public async Task<LocalStorageDTO> GetModelFromToken() {
            try
            {
                string token = await GetBrowserLocalStorage();
                if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                    return new LocalStorageDTO();
                return DeserializeJsonString<LocalStorageDTO>(token);
            }
            catch {
                return new LocalStorageDTO();
            }
        }

        public async Task SetBrowserLocalStorage(LocalStorageDTO localStorageDTO) {
            try
            {
                string token = SerializeObj(localStorageDTO);
                await localStorageService.SaveAsEncryptedStringAsync(Constant.BrowserStorageKey, token);
            }
            catch { }
        }

        public async Task RemoveTokenFromBrowserLocalStorage() => await localStorageService.DeleteItemAsync(Constant.BrowserStorageKey);

        public static string SerializeObj<T>(T modelObject) => JsonSerializer.Serialize(modelObject, JsonOptions());

        public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, JsonOptions());
        private static JsonSerializerOptions JsonOptions() {
            return new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip
            };
        }
    }
}
