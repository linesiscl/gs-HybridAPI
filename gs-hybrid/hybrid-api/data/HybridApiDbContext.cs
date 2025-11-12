using gs_hybrid.hybrid_api.model;
using Microsoft.EntityFrameworkCore;

namespace gs_hybrid.hybrid_api.data
{
    public class HybridApiDbContext : DbContext
    {
        public HybridApiDbContext(DbContextOptions<HybridApiDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<WorkSession> WorkSessions => Set<WorkSession>();
        public DbSet<Pause> Pauses => Set<Pause>();
        public DbSet<Goal> Goals => Set<Goal>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.WorkSessions)
                .WithOne(ws => ws.User)
                .HasForeignKey(ws => ws.UserId);

            modelBuilder.Entity<WorkSession>()
                .HasMany(ws => ws.Pauses)
                .WithOne(p => p.WorkSession)
                .HasForeignKey(p => p.WorkSessionId);
        }
    }
}
