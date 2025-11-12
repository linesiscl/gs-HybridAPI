namespace gs_hybrid.hybrid_api.model
{
    public class WorkSession
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime? EndUtc { get; set; }
        public bool IsProductive { get; set; } = true;
        public ICollection<Pause>? Pauses { get; set; }
    }
}
