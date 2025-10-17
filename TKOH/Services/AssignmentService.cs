using TKOH.DTOs;

namespace TKOH.Services
{
    public class AssignmentService
    {
        private readonly ConnectorAPI _api;

        public AssignmentService(ConnectorAPI api)
        {
            _api = api;
        }

        public async Task<List<AssignmentDTO>> GetAllAssignmentsAsync(string? token)
        {
            var response = await _api.GetAsync<ApiResponse<List<AssignmentDTO>>>("/api/assignments",token);
            return response?.Data ?? new List<AssignmentDTO>();
        }

        public async Task<List<AssignmentDTO>> GetAssignmentsByUserIdAsync(long userId, string? token)
        {
            var response = await _api.GetAsync<ApiResponse<List<AssignmentDTO>>>($"/api/assignments/users/{userId}", token);
            return response?.Data ?? new List<AssignmentDTO>();
        }

        public async Task<List<AssignmentDTO>> GetAssignmentsByDateRangeAsync(DateTime start, DateTime end, string? token)
        {
            string startDate = start.ToString("yyyy-MM-dd");
            string endDate = end.ToString("yyyy-MM-dd");

            var response = await _api.GetAsync<ApiResponse<List<AssignmentDTO>>>($"/api/assignments/range?start={startDate}&end={endDate}", token);
            return response?.Data ?? new List<AssignmentDTO>();
        }

        public async Task<AssignmentDetailDTO?> GetAssignmentByIdAsync(long id,string? token)
        {
            var response = await _api.GetAsync<ApiResponse<AssignmentDetailDTO>>($"/api/assignments/{id}", token);
            return response?.Data;
        }

        public async Task<AssignmentDTO?> CreateAssignmentAsync(CreateAssignmentDTO assignmentData,string? token)
        {
            var response = await _api.PostAsync<CreateAssignmentDTO, ApiResponse<AssignmentDTO>>("/api/assignments", assignmentData,token);
            return response?.Data;
        }

        public async Task<AssignmentDTO?> UpdateAssignmentAsync(long id, UpdateAssignmentDTO assignmentData,string? token)
        {
            var response = await _api.PutAsync<UpdateAssignmentDTO, ApiResponse<AssignmentDTO>>($"/api/assignments/{id}", assignmentData,token);
            return response?.Data;
        }

        public async Task<bool> DeleteAssignmentAsync(long id,string? token)
        {
            return await _api.DeleteAsync($"/api/assignments/{id}", token);
        }
    }
}

