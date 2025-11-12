namespace gs_hybrid.hybrid_api.dto
{
    public class PauseDto
    {
        public Guid WorkSessionId { get; set; }
        public DateTime StartUtc { get; set; } = DateTime.UtcNow;
        public DateTime? EndUtc { get; set; }
        public string PauseType { get; set; } = "Break";
    }
}
