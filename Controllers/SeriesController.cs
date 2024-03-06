using HFilms.Controllers;
using HFilms.Infrastructure;
using HFilms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HellsFilms.Controllers
{
	public class SeriesController : Controller
	{

        private readonly KinoboxApiClient _kinoboxApiClient;

        public SeriesController(KinoboxApiClient kinoboxApiClient)
        {
            _kinoboxApiClient = kinoboxApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var popularMovies = await _kinoboxApiClient.GetPopularSeriesAsync();
            return View(popularMovies);
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
	}
}
