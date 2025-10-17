using System.Text.Json.Serialization;

namespace TKOH.DTOs
{
    public class UserDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("currentScore")]
        public int CurrentScore { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("locked")]
        public bool Locked { get; set; }

        [JsonPropertyName("roles")]
        public List<RoleSummaryDTO> Roles { get; set; } = new();
    }

    public class CreateUserDTO
    {
        [JsonPropertyName("name")]
        public string? Username { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
        [JsonPropertyName("roleIds")]
        public List<long> RoleIds { get; set; } = new();
    }

    public class UpdateUserDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string? Username { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("roleIds")]
        public List<long> RoleIds { get; set; } = new();
        [JsonPropertyName("currentScore")]
        public int CurrentScore { get; set; }
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("locked")]
        public bool Locked { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
    public class ChangePasswordDTO
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
    public class ScoreAdjustmentDTO
    {
        public double Adjustment { get; set; }
        public string? Reason { get; set; }
    }
}

