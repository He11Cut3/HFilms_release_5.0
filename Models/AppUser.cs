using Microsoft.AspNetCore.Identity;

namespace HFilms.Models
{
	public class AppUser : IdentityUser
	{
        public AppUser()
        {
            FavoriteFilms = new List<FavoriteFilm>();
        }
        // Список любимых фильмов
        public ICollection<FavoriteFilm> FavoriteFilms { get; set; }
    }
}
