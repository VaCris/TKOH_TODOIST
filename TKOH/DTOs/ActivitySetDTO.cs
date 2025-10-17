using System.Text.Json.Serialization;

namespace TKOH.DTOs
{
    public class ActivitySetDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string? Title { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime CreatedAt { get; set; }
    }
    public class ActivitySetDetailDTO : ActivitySetDTO
    {
        public List<TemplateDTO> Templates { get; set; } = new List<TemplateDTO>();
    }
    public class ActivitySetSummaryDTO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string? Title { get; set; }
        [JsonPropertyName("assignmentCount")]
        public int AssignmentCount { get; set; }
    }
    public class CreateActivitySetDTO
    {
        [JsonPropertyName("name")]
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
    public class UpdateActivitySetDTO
    {
        [JsonPropertyName("name")]
        public string? Title { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
