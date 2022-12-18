using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }


        public DbSet<Movie> Movie { get; set; }
        public DbSet<PageData> PageData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasKey(c => c.Id);
            modelBuilder.Entity<PageData>().HasKey(c => c.Id);



            modelBuilder.Entity<Movie>()
    .HasOne<PageData>(s => s.PageData)
    .WithMany(g => g.Results)
    .HasForeignKey(s => s.PageId);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "ilhandemirtepe";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "ilhandemirtepe";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
