using System.Text.Json.Serialization;

namespace TKOH.DTOs
{
    public class RegisterResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("roles")]
        public List<string>? Roles { get; set; }
    }
}
