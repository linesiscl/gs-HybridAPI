namespace gs_hybrid.hybrid_api.dto
{
    public class CreateWorkSessionDto
    {
        public Guid UserId { get; set; }
        public DateTime StartUtc { get; set; } = DateTime.UtcNow;
        public bool IsProductive { get; set; } = true;
    }
}
