namespace TKOH.DTOs
{
    public class ScoreHistoryDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public double Score { get; set; }
        public string? Reason { get; set; }
        public DateTime RecordedAt { get; set; }
    }

    public class ScoreHistoryDetailDTO : ScoreHistoryDTO
    {
        public UserDTO? User { get; set; }
    }

    public class CreateScoreHistoryDTO
    {
        public long UserId { get; set; }
        public double Score { get; set; }
        public string? Reason { get; set; }
        public DateTime RecordedAt { get; set; }
    }

    public class UpdateScoreHistoryDTO
    {
        public double Score { get; set; }
        public string? Reason { get; set; }
    }
}
