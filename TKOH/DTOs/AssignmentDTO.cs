using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TKOH.DTOs
{
    public class AssignmentDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("templateId")]
        public long TemplateId { get; set; }
        public string? TemplateTitle { get; set; }
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
        public string? UserName { get; set; }
        [JsonPropertyName("date")]
        public DateTime DueDate { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
    public class AssignmentDetailDTO : AssignmentDTO
    {
        [JsonPropertyName("evidenceUrl")]
        public string? Evidence { get; set; }
        public string? TemplateContent { get; set; }
    }

    public class CreateAssignmentDTO
    {
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "El nombre de la plantilla es obligatorio.")]
        public string? Name { get; set; }
        [JsonPropertyName("weekday")]
        [Required(ErrorMessage = "El día de la semana es obligatorio.")]
        public int Weekday { get; set; }
        [JsonPropertyName("weight")]
        [Required(ErrorMessage = "El peso es obligatorio.")]
        public int Weight { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("date")]
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Date { get; set; }
        [JsonPropertyName("user_id")]
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public long UserId { get; set; }
    }

    public class UpdateAssignmentDTO
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("evidenceUrl")]
        public string? Evidence { get; set; }
    }
}

