﻿using HFilms.Infrastructure;
using HFilms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HFilms.Controllers
{
    public class HomeController : Controller
    {
        private readonly KinoboxApiClient _kinoboxApiClient;


        public HomeController(KinoboxApiClient kinoboxApiClient)
        {
            _kinoboxApiClient = kinoboxApiClient;

        }

        public async Task<IActionResult> Search(string title)
        {
            var films = await _kinoboxApiClient.SearchFilmsAsync(title);

            return RedirectToAction("SearchResults","MovieList" , new { films = films });
        }

        public async Task<IActionResult> Index()
		{
            var popularMovies = await _kinoboxApiClient.GetPopularFilmsAsync();
            return View(popularMovies);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
