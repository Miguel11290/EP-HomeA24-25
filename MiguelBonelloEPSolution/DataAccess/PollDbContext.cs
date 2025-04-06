using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class PollDbContext : DbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options) { }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
