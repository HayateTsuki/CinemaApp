using Cinema.Domain.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Domain.Data.Context
{
    public class CinemaContext : IdentityDbContext<CinemaUser, IdentityRole<int>, int>
    {
        public CinemaContext()
        {
        }

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }

        public DbSet<HallEntity> Halls { get; set; }

        public DbSet<MovieEntity> Movies { get; set; }

        public DbSet<ScreeningEntity> Screenings { get; set; }

        public DbSet<BookingEntity> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("cinema");
            modelBuilder.Entity<IdentityRole<int>>(b =>
            {
                b.HasIndex(x => x.Name).IsUnique();
            });
            modelBuilder.Entity<CinemaUser>(e =>
            {
                e.Property(e => e.UserName).IsRequired();
            });
            modelBuilder.Entity<HallEntity>(e =>
            {
                e.Property(e => e.Name).IsRequired();
                e.HasIndex(e => e.Name).IsUnique();
            });
            modelBuilder.Entity<MovieEntity>(e =>
            {
                e.Property(e => e.Title).IsRequired();
                e.Property(e => e.Description).IsRequired();
            });
            modelBuilder.Entity<ScreeningEntity>(e =>
            {
                e.HasOne(f => f.Movie)
                .WithMany(g => g.Screenings)
                .HasForeignKey(h => h.MovieId);

                e.HasOne(f => f.Hall)
                .WithMany(g => g.Screenings)
                .HasForeignKey(h => h.HallId);
            });
            modelBuilder.Entity<BookingEntity>(e =>
            {
                e.HasOne(f => f.Screening)
                .WithMany(g => g.Bookings)
                .HasForeignKey(h => h.ScreeningId);
                e.HasOne(f => f.User)
                .WithMany(g => g.Bookings)
                .HasForeignKey(h => h.UserId);
            });
        }
    }
}
