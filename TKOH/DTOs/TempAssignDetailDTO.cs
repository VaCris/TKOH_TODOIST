using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TKOH.DTOs
{
    public enum Status
    {
        Pending,
        Completed,
        Overdue
    }

    public enum GradeStatus
    {
        NotGraded,
        Graded,
        Reviewed
    }

    public class TempAssignDetailDTO
    {
        [JsonPropertyName("templateId")]
        public long TemplateId { get; set; }

        [JsonPropertyName("assignmentId")]
        public long AssignmentId { get; set; }

        [JsonPropertyName("name")]
        [Required]
        public string? Name { get; set; }

        [JsonPropertyName("weekday")]
        [Range(1, 7)]
        public int Weekday { get; set; }

        [JsonPropertyName("weight")]
        [Range(1, 10)]
        public int Weight { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("userId")]
        public long UserId { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("gradeStatus")]
        public GradeStatus GradeStatus { get; set; }

        [JsonPropertyName("evidenceUrl")]
        public string? EvidenceUrl { get; set; }
    }
}
