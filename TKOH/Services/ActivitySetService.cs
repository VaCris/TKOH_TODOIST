using TKOH.DTOs;

namespace TKOH.Services
{
    public class ActivitySetService
    {
        private readonly ConnectorAPI _api;

        public ActivitySetService(ConnectorAPI api)
        {
            _api = api;
        }

        public async Task<List<ActivitySetDTO>> GetAllSetsAsync(string? token)
        {
            var response = await _api.GetAsync<ApiResponse<List<ActivitySetDTO>>>("/api/activity-sets",token);
            return response?.Data ?? new List<ActivitySetDTO>();
        }

        public async Task<ActivitySetDTO?> GetSetByIdAsync(long id, string? token)
        {
            var response = await _api.GetAsync<ApiResponse<ActivitySetDTO>>($"/api/activity-sets/{id}", token);
            return response?.Data;
        }


        public async Task<ActivitySetDetailDTO?> GetSetDetailByIdAsync(long id,string? token)
        {
            var response = await _api.GetAsync<ApiResponse<ActivitySetDetailDTO>>($"/api/activity-sets/{id}/detail",token);
            return response?.Data;
        }

        public async Task<List<ActivitySetSummaryDTO>> GetSetSummariesAsync(string? token)
        {
            var response = await _api.GetAsync<ApiResponse<List<ActivitySetSummaryDTO>>>("/api/activity-sets/summaries",token);
            return response?.Data ?? new List<ActivitySetSummaryDTO>();
        }

        public async Task<ActivitySetDTO?> CreateSetAsync(CreateActivitySetDTO setData,string? token)
        {
            var response = await _api.PostAsync<CreateActivitySetDTO, ApiResponse<ActivitySetDTO>>("/api/activity-sets", setData,token);
            return response?.Data;
        }

        public async Task<ActivitySetDTO?> UpdateSetAsync(long id, UpdateActivitySetDTO setData,string? token)
        {
            var response = await _api.PutAsync<UpdateActivitySetDTO, ApiResponse<ActivitySetDTO>>($"/api/activity-sets/{id}", setData,token);
            return response?.Data;
        }

        public async Task<bool> DeleteSetAsync(long id,string? token)
        {
            return await _api.DeleteAsync($"/api/activity-sets/{id}",token);
        }
    }
}
