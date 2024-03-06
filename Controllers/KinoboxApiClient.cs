using HFilms.Models;
using HFilms.Models.ViewModels;
using HtmlAgilityPack;
using Humanizer.Localisation;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace HFilms.Controllers
{
    public class KinoboxApiClient
    {
        private readonly HttpClient _httpClient;

        public KinoboxApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://kinobox.tv"); // Укажите реальный базовый адрес API
        }

        public async Task<List<Player>> GetMainPlayersAsync(string kinopoiskId)
        {
            var url = $"/api/players/main?kinopoisk={kinopoiskId}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Player>>(content);
            }

            // Handle error here if needed
            return null;
        }

        public async Task<FilmSearchResponse> SearchFilmsAsync(string title)
        {
            var url = $"https://kinopoiskapiunofficial.tech/api/v2.1/films/search-by-keyword?keyword={title}";

            Console.WriteLine(url);

            // Include the API key in the request headers
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", "e84e75bd-eb7c-4e84-84bc-2b321db049b7");

            var response = await _httpClient.GetAsync(url);

            // Remove the API key from the headers after the request is made
            _httpClient.DefaultRequestHeaders.Remove("X-API-KEY");
            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return JsonConvert.DeserializeObject<FilmSearchResponse>(content);
            }

            // Handle error here if needed
            return null;
        }


        public async Task<List<BasicFilm>> GetPopularSeriesAsync()
        {
            var url = "/api/popular/series";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BasicFilm>>(content);
            }

            // Handle error here if needed
            return null;
        }
        public async Task<List<BasicFilm>> GetPopularFilmsAsync()
        {
            var url = "/api/popular/films";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BasicFilm>>(content);
            }

            // Handle error here if needed
            return null;
        }


        public async Task<List<Player>> SearchMainPlayersAsync(string kinopoiskId)
        {
            var url = $"/api/players/main?kinopoisk={kinopoiskId}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Player>>(content);
            }

            // Handle error here if needed
            return null;
        }

        public async Task<List<Player>> SearchAllPlayersAsync(string title)
        {
            var url = $"/api/players/all?title={title}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Player>>(content);
            }

            // Handle error here if needed
            return null;
        }

        public async Task<List<Player>> GetAllPlayersAsync(string kinopoisk = null, string imdb = null, string title = null, string token = null)
        {
            var url = $"/api/players/all?kinopoisk={kinopoisk}&imdb={imdb}&title={title}&token={token}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Player>>(content);
            }

            // Handle error here if needed
            return null;
        }
        public async Task<List<Player>> GetMoviePlayersAsync(string kinopoisk)
        {
            var url = $"/api/players/main?kinopoisk={kinopoisk}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Player>>(content);
            }

            // Обработка ошибок здесь, если это необходимо
            return null;
        }

        public async Task<FilmDetails> GetFilmDetailsAsync(string kinopoiskId)
        {
            var url = $"https://kinopoiskapiunofficial.tech/api/v2.2/films/{kinopoiskId}";

            Console.WriteLine(url);

            // Include the API key in the request headers
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", "e84e75bd-eb7c-4e84-84bc-2b321db049b7");

            var response = await _httpClient.GetAsync(url);

            // Remove the API key from the headers after the request is made
            _httpClient.DefaultRequestHeaders.Remove("X-API-KEY");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<FilmDetails>(content);
            }

            // Handle error here if needed
            return null;
        }
    }

    public class FilmDetails
    {
        public string KinopoiskId { get; set; }
        public string nameRu { get; set; }
        public string posterUrl { get; set; }

        public string coverUrl { get; set; }

		public string ratingKinopoisk { get; set; }
        public string description { get; set; }

        public string year { get; set; }

        public string genre { get; set; }

        public string Season { get; set; }

        public string Seriya { get; set; }
    }


    public class FilmSearchResponse
    {
        public string keyword { get; set; }
        public int pagesCount { get; set; }
        public List<FilmSearch> films { get; set; }
    }

    public class FilmSearch
    {
        public string filmId { get; set; }
        public string nameRu { get; set; }
        public string rating { get; set; }
        public string posterUrl { get; set; }
        
        public string year { get; set; }
    }


    public class Player
    {
        public string KinopoiskId { get; set; } // Добавьте это свойство
        public PlayerSource Source { get; set; }
        public string Translation { get; set; }
        public string Quality { get; set; }
        public string IframeUrl { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string FilmTitle { get; set; }
        public string AlternativeTitle { get; set; }
        public int FilmYear { get; set; }
        public double? FilmRating { get; set; }
        public string PosterUrl { get; set; }
    }

    public enum PlayerSource
    {
        Alloha,
        Ashdi,
        Bazon,
        Cdnmovies,
        Collaps,
        Hdvb,
        Iframe,
        Kodik,
        Videocdn,
        Voidboost
    }
}
