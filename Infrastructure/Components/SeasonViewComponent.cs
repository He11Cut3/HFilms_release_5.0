using HFilms.Controllers;
using HFilms.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace HFilms.Infrastructure.Components
{
	public class SeasonViewComponent : ViewComponent
	{
		private DataContext _context;
		private readonly UserManager<AppUser> _signInManager;
		private readonly DataContext _dataContext;

		public SeasonViewComponent(UserManager<AppUser> signInManager, DataContext dataContext)
        {
			_context = dataContext;
			_signInManager = signInManager;
			_dataContext = dataContext;
		}

		public async Task<IViewComponentResult> InvokeAsync(string kinopoisk)
		{
			var currentUser = await _signInManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);

			if (currentUser == null)
			{
				return View(new List<SaveSeason>());
			}
			var istruePerson = _dataContext.SaveSeasons
				.Where(o => o.UserId == currentUser.Id && o.FilmsID == kinopoisk)
				.ToList();

			return View(istruePerson);
		}
	}
}
