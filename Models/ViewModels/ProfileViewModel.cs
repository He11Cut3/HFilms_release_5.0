namespace HFilms.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }

        public List<FavoriteFilm> FavoriteFilms { get; set; }

        public string UserName { get; set; }
    }
}
