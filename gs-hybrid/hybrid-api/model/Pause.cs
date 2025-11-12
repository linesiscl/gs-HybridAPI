namespace gs_hybrid.hybrid_api.model
{
    public class Pause
    {
        public Guid Id { get; set; }
        public Guid WorkSessionId { get; set; }
        public WorkSession? WorkSession { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime? EndUtc { get; set; }
        public string PauseType { get; set; } = "Break";
    }
}
