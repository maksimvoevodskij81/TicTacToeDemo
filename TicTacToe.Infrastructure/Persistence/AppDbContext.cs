using Microsoft.EntityFrameworkCore;
using TicTacToe.Infrastructure.Persistence.Entities;

namespace TicTacToe.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions): base(dbContextOptions) 
        {
        }

        public DbSet<GameEntity> Games { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<GameEntity>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(x => x.StateJson)
                   .IsRequired();

                b.Property(x => x.RowVersion)
                .IsConcurrencyToken();
            });
            
        }


    }
}
