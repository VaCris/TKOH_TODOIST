using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TKOH.DTOs
{
    public class RegisterRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [Required, Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string? ConfirmPassword { get; set; }

        [JsonPropertyName("roleIds")]
        public List<long> RoleIds { get; set; } = new() { 3 };
    }
}
