using TKOH.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace  TKOH.Services
{
    public class UserService
    {
        private readonly ConnectorAPI _api;

        public UserService(ConnectorAPI api)
        {
            _api = api;
        }

        public async Task<UserDTO?> GetCurrentUserAsync(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var email = jwtToken.Claims.First(c => c.Type == "sub").Value;

                var searchResponse = await _api.GetAsync<ApiResponse<List<UserDTO>>>($"/api/users/search?q={Uri.EscapeDataString(email)}", token);
                if (searchResponse?.Data == null || searchResponse.Data.Count == 0)
                    return null;

                var user = searchResponse.Data.First();

                var response = await _api.GetAsync<ApiResponse<UserDTO>>($"/api/users/{user.Id}/detail", token);
                return response?.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetCurrentUserAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<List<UserDTO>> GetAllAsync(string token)
        {
            var response = await _api.GetAsync<ApiResponse<List<UserDTO>>>("/api/users", token);

            if (response?.Data != null)
            {
                foreach (var u in response.Data)
                {
                    var roles = u.Roles != null ? string.Join(", ", u.Roles.Select(r => r.Name)) : "N/A";
                }
            }
            else
            {
                Console.WriteLine("null");
            }

            return response?.Data ?? new List<UserDTO>();
        }

        public async Task<UserDTO?> GetUserByIdAsync(long id, string token)
        {
            var response = await _api.GetAsync<ApiResponse<UserDTO>>($"/api/users/{id}", token);
            return response?.Data;
        }

        public async Task<UserDTO?> GetUserDetailsByIdAsync(long id, string token)
        {
            var response = await _api.GetAsync<ApiResponse<UserDTO>>($"/api/users/{id}/detail", token);
            return response?.Data;
        }

        public async Task<UserDTO?> CreateUserAsync(CreateUserDTO user, string token)
        {
            var response = await _api.PostAsync<CreateUserDTO, ApiResponse<UserDTO>>("/api/users", user, token);
            return response?.Data;
        }

        public async Task<UserDTO?> UpdateUserAsync(long id, UpdateUserDTO user, string token)
        {
            var response = await _api.PutAsync<UpdateUserDTO, ApiResponse<UserDTO>>("/api/users", user, token);
            return response?.Data;
        }

        public async Task<bool> DeleteUserAsync(long id, string token)
        {
            return await _api.DeleteAsync($"/api/users/{id}", token);
        }

        public async Task<List<UserDTO>> GetUsersByRoleAsync(string role, string token)
        {
            var response = await _api.GetAsync<ApiResponse<List<UserDTO>>>($"/api/users/by-role/{role}", token);
            return response?.Data ?? new List<UserDTO>();
        }

        public async Task<List<UserDTO>> SearchUsersAsync(string query, string token)
        {
            var response = await _api.GetAsync<ApiResponse<List<UserDTO>>>($"/api/users/search?q={Uri.EscapeDataString(query)}", token);
            return response?.Data ?? new List<UserDTO>();
        }

        public async Task<bool> ChangePasswordAsync(long id, ChangePasswordDTO passwordData, string token)
        {
            var response = await _api.PatchAsync<ChangePasswordDTO, ApiResponse<object>>($"/api/users/{id}/password", passwordData, token);
            return response?.Success ?? false;
        }

        public async Task<bool> AdjustScoreAsync(long id, ScoreAdjustmentDTO adjustment, string token)
        {
            var response = await _api.PostAsync<ScoreAdjustmentDTO, ApiResponse<object>>($"/api/users/{id}/score-adjustments", adjustment, token);
            return response?.Success ?? false;
        }

        public async Task<bool> LockUserAsync(long id, string token)
        {
            var response = await _api.PostAsync<object, ApiResponse<object>>($"/api/users/{id}/lock", null, token);
            return response?.Success ?? false;
        }

        public async Task<bool> UnlockUserAsync(long id, string token)
        {
            var response = await _api.PostAsync<object, ApiResponse<object>>($"/api/users/{id}/unlock", null, token);
            return response?.Success ?? false;
        }
    }
}