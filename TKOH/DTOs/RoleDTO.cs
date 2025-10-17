using System.Text.Json.Serialization;

namespace TKOH.DTOs
{
    public class RoleDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class RoleSummaryDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class CreateRoleDTO
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class UpdateRoleDTO
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
