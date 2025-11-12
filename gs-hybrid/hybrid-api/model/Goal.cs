namespace gs_hybrid.hybrid_api.model
{
    public class Goal
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TargetDateUtc { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
