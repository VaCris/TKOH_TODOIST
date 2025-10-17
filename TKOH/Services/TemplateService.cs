using TKOH.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace TKOH.Services
{
    public class TemplateService
    {
        private readonly ConnectorAPI _api;

        public TemplateService(ConnectorAPI api)
        {
            _api = api;
        }

        public async Task<List<TemplateDTO>> GetAllTemplatesAsync(string token)
        {
            var response = await _api.GetAsync<ApiResponse<List<TemplateDTO>>>("/api/templates", token);
            return response?.Data ?? new List<TemplateDTO>();
        }

        public async Task<List<TemplateDTO>> GetActiveTemplatesAsync(string token)
        {
            try
            {
                var response = await _api.GetAsync<ApiResponse<List<TemplateDTO>>>("/api/templates/active", token);
                return response?.Data ?? new List<TemplateDTO>();
            }
            catch (Exception ex)
            {
                return new List<TemplateDTO>();
            }
        }

        public async Task<List<TemplateDTO>> GetActiveTemplatesByWeekdayAsync(int weekday, string token)
        {
            try
            {
                var response = await _api.GetAsync<ApiResponse<List<TemplateDTO>>>($"/api/templates/active/by-weekday/{weekday}", token);
                return response?.Data ?? new List<TemplateDTO>();
            }
            catch (Exception ex)
            {
                return new List<TemplateDTO>();
            }
        }

        public async Task<TemplateDTO?> GetTemplateByIdAsync(long id, string token)
        {
            try
            {
                var response = await _api.GetAsync<ApiResponse<TemplateDTO>>($"/api/templates/{id}", token);
                return response?.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TemplateDTO?> GetTemplateDetailByIdAsync(long id, string token)
        {
            try
            {
                var response = await _api.GetAsync<ApiResponse<TemplateDTO>>($"/api/templates/{id}/detail", token);
                return response?.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TemplateDTO?> CreateTemplateAsync(CreateTemplateDTO templateData, string? token)
        {
            var response = await _api.PostAsync<CreateTemplateDTO, ApiResponse<TemplateDTO>>("/api/templates", templateData, token);
            return response?.Data;
        }

        public async Task<TemplateDTO?> UpdateTemplateAsync(long id, UpdateTemplateDTO templateData, string token)
        {
            try
            {
                var response = await _api.PutAsync<UpdateTemplateDTO, ApiResponse<TemplateDTO>>($"/api/templates/{id}", templateData, token);
                return response?.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteTemplateAsync(long id, string token)
        {
            try
            {
                return await _api.DeleteAsync($"/api/templates/{id}", token);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
