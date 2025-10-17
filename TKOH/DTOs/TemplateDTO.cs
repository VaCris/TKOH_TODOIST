using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TKOH.DTOs
{
    public class TemplateDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string? Title { get; set; }
        [JsonPropertyName("description")]
        public string? Content { get; set; }
        [JsonPropertyName("active")]
        public bool IsActive { get; set; }
        [JsonPropertyName("weekday")]
        public int Weekday { get; set; } // 1 - 7
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    public class CreateTemplateDTO
    {
        [JsonPropertyName("name")]
        public string? Title { get; set; }

        [JsonPropertyName("weekday")]
        public int Weekday { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("description")]
        public string? Content { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("active")]
        public bool IsActive { get; set; } = true;
    }

    public class UpdateTemplateDTO
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
        public string? Title { get; set; }
        [JsonPropertyName("description")]
        [Required(ErrorMessage = "El contenido es obligatorio.")]
        public string? Content { get; set; }
        [JsonPropertyName("active")]
        public bool IsActive { get; set; }
        [JsonPropertyName("weekday")]
        [Range(1, 7, ErrorMessage = "El día de la semana debe ser un número entre 1 y 7.")]
        public int Weekday { get; set; }
    }
}
