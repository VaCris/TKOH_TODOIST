using TKOH.DTOs;

namespace TKOH.Services
{
    public class ScoreHistoryService
    {
        private readonly ConnectorAPI _api;

        public ScoreHistoryService(ConnectorAPI api)
        {
            _api = api;
        }

        public async Task<List<ScoreHistoryDTO>> GetScoreHistoryAsync(string? token)
        {
            var response = await _api.GetAsync<ApiResponse<List<ScoreHistoryDTO>>>("/api/score-history",token);
            return response?.Data ?? new List<ScoreHistoryDTO>();
        }

        public async Task<ScoreHistoryDTO?> GetHistoryEntryByIdAsync(long id,string? token)
        {
            var response = await _api.GetAsync<ApiResponse<ScoreHistoryDTO>>($"/api/score-history/{id}",token);
            return response?.Data;
        }

        public async Task<ScoreHistoryDetailDTO?> GetHistoryEntryDetailByIdAsync(long id,string? token)
        {
            var response = await _api.GetAsync<ApiResponse<ScoreHistoryDetailDTO>>($"/api/score-history/{id}/detail",token);
            return response?.Data;
        }

        public async Task<List<ScoreHistoryDTO>> GetRecentHistoryForUserAsync(long userId,string? token)
        {
            var response = await _api.GetAsync<ApiResponse<List<ScoreHistoryDTO>>>($"/api/score-history/users/{userId}/recent",token);
            return response?.Data ?? new List<ScoreHistoryDTO>();
        }

        public async Task<ScoreHistoryDTO?> CreateHistoryEntryAsync(CreateScoreHistoryDTO historyData,string? token)
        {
            var response = await _api.PostAsync<CreateScoreHistoryDTO, ApiResponse<ScoreHistoryDTO>>("/api/score-history", historyData,token);
            return response?.Data;
        }

        public async Task<ScoreHistoryDTO?> UpdateHistoryEntryAsync(long id, UpdateScoreHistoryDTO historyData,string? token)
        {
            var response = await _api.PutAsync<UpdateScoreHistoryDTO, ApiResponse<ScoreHistoryDTO>>($"/api/score-history/{id}", historyData,token);
            return response?.Data;
        }

        public async Task<bool> DeleteHistoryEntryAsync(long id,string? token)
        {
            return await _api.DeleteAsync($"/api/score-history/{id}",token);
        }
    }
}
