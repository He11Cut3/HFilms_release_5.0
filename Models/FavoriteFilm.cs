using Newtonsoft.Json;

namespace HFilms.Models
{
    public class FavoriteFilm
    {
        public int Id { get; set; }
        public string PrimaryTitle { get; set; }
        public string AlternativeTitle { get; set; }
        public string Year { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate, NullValueHandling = NullValueHandling.Ignore)]
        public string Rating { get; set; }

        public string PosterUrl { get; set; }

        public string FilmsID { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }

}
