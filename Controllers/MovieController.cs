using HFilms.Infrastructure;
using HFilms.Models;
using HFilms.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using static Azure.Core.HttpHeader;

namespace HFilms.Controllers
{
    public class MovieController : Controller
    {
        private readonly KinoboxApiClient _kinoboxApiClient;
        private readonly UserManager<AppUser> _signInManager;
        private readonly DataContext _dataContext;


        public MovieController(KinoboxApiClient kinoboxApiClient, UserManager<AppUser> signInManager, DataContext dataContext)
        {
            _kinoboxApiClient = kinoboxApiClient;
            _signInManager = signInManager;
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            var popularMovies = await _kinoboxApiClient.GetPopularFilmsAsync();
            return View(popularMovies);
        }



        public async Task<IActionResult> Search(string title)
        {
            var searchResults = await _kinoboxApiClient.SearchFilmsAsync(title);
            return RedirectToAction("SearchResults", new { title = title });
        }

        public async Task<IActionResult> SearchResults(string title)
        {
            var searchResults = await _kinoboxApiClient.SearchFilmsAsync(title);
            ViewBag.Query = title;
            return View(searchResults);
        }

        public async Task<IActionResult> Details(string kinopoiskId)
        {
            var filmDetails = await _kinoboxApiClient.GetFilmDetailsAsync(kinopoiskId);

           

            if (filmDetails != null)
            {
                return View("Details", filmDetails);
            }
            else
            {
                return View();
            }
        }

		[HttpPost]
		public async Task<IActionResult> SaveSeasonFilms(string KinopoiskId, string nameRu, string season, string seriya, SaveSeason saveSeason)
		{
			// Проверяем, был ли запрос отправлен методом POST
			if (HttpContext.Request.Method == "POST")
			{
				var currentUser = await _signInManager.GetUserAsync(User);

				if (currentUser != null)
				{
					saveSeason.PrimaryTitle = nameRu;
					saveSeason.Seriya = seriya;
					saveSeason.User = currentUser;
					saveSeason.Season = season;
					saveSeason.FilmsID = KinopoiskId;

					_dataContext.SaveSeasons.Add(saveSeason);
					await _dataContext.SaveChangesAsync();
				}
			}

			// Перенаправляем на другую страницу (GET), например, на страницу деталей фильма
			return RedirectToAction("Details", new { kinopoiskId = KinopoiskId });
		}


		public async Task<IActionResult> ViewSeasonFilms()
        {
            return View();
        }



	}
}
