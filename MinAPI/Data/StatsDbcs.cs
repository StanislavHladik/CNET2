using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Collections.Generic;
using MinAPI.Model;

namespace MinAPI.Data
{
    public class StatsDb : DbContext
    {
        public StatsDb(DbContextOptions<StatsDb> options) : base(options) { }

        public DbSet<StatsResult> StatsResults => Set<StatsResult>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StatsResult>()
                .Property(b => b.Top10Words)
                .HasConversion(
                   v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                   v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, (JsonSerializerOptions)null));
        }
    }
}
