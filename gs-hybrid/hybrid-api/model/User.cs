namespace gs_hybrid.hybrid_api.model
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public ICollection<WorkSession>? WorkSessions { get; set; }
    }
}
