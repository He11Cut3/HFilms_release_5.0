using HFilms.Controllers;

namespace HFilms.Models.ViewModels
{
    public class MovieDetailsViewModel
    {
		public List<BasicFilm> Films { get; set; }
        public List<Player> Players { get; set; }
    }
}
