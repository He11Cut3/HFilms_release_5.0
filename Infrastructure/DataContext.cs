using HFilms.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HFilms.Infrastructure
{
    public class DataContext: IdentityDbContext<AppUser>
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<FavoriteFilm> FavoriteFilms { get; set; }

        public DbSet<SaveSeason> SaveSeasons { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Определение отношения между пользователями и заказами
            builder.Entity<FavoriteFilm>()
                .HasOne(o => o.User)
                .WithMany(u => u.FavoriteFilms)
                .HasForeignKey(o => o.UserId);
        }

    }
}
