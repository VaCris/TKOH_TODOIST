using System.Text.Json.Serialization;

namespace TKOH.DTOs
{
    public class AuthResponse
    {
        [JsonPropertyName("accessToken")]
        public string? AccessToken { get; set; }
        [JsonPropertyName("tokenType")]
        public string? TokenType { get; set; }
        [JsonPropertyName("expiresAt")]
        public DateTime ExpiresAt { get; set; }
    }
}
