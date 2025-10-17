using TKOH.DTOs;
using Microsoft.AspNetCore.Http;

namespace TKOH.Services
{
    public class RoleService
    {
        private readonly ConnectorAPI _api;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string JwtCookieName = "JWT_TOKEN";

        public RoleService(ConnectorAPI api, IHttpContextAccessor httpContextAccessor)
        {
            _api = api;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetTokenFromCookie()
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies[JwtCookieName];
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            var token = GetTokenFromCookie();
            var res = await _api.GetAsync<ApiResponse<List<RoleDTO>>>("/api/roles", token);
            return res?.Data ?? new List<RoleDTO>();
        }

        public async Task<List<RoleSummaryDTO>> GetAllSummariesAsync()
        {
            var token = GetTokenFromCookie();
            var res = await _api.GetAsync<ApiResponse<List<RoleSummaryDTO>>>("/api/roles/summaries", token);
            return res?.Data ?? new List<RoleSummaryDTO>();
        }

        public async Task<RoleDTO?> GetRoleByIdAsync(long id)
        {
            var token = GetTokenFromCookie();
            var response = await _api.GetAsync<ApiResponse<RoleDTO>>($"/api/roles/{id}", token);
            return response?.Data;
        }

        public async Task<RoleDTO?> GetRoleByNameAsync(string name)
        {
            var token = GetTokenFromCookie();
            var response = await _api.GetAsync<ApiResponse<RoleDTO>>($"/api/roles/by-name/{Uri.EscapeDataString(name)}", token);
            return response?.Data;
        }

        public async Task<bool> CreateAsync(CreateRoleDTO request)
        {
            var token = GetTokenFromCookie();
            var res = await _api.PostAsync<CreateRoleDTO, ApiResponse<RoleDTO>>("/api/roles", request, token);
            return res?.Data != null;
        }

        public async Task<RoleDTO?> UpdateRoleAsync(long id, UpdateRoleDTO roleData)
        {
            var token = GetTokenFromCookie();
            var response = await _api.PutAsync<UpdateRoleDTO, ApiResponse<RoleDTO>>($"/api/roles/{id}", roleData, token);
            return response?.Data;
        }

        public async Task<bool> DeleteRoleAsync(long id)
        {
            var token = GetTokenFromCookie();
            return await _api.DeleteAsync($"/api/roles/{id}", token);
        }
    }
}