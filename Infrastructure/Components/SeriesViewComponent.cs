using HFilms.Controllers;
using HFilms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HFilms.Infrastructure.Components
{
	public class SeriesViewComponent: ViewComponent
	{
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Используйте ваш KinoboxApiClient, чтобы получить популярные сериалы
            var kinoboxApiClient = new KinoboxApiClient(new HttpClient());
            var popularSeries = await kinoboxApiClient.GetPopularSeriesAsync();

            // Возвращаем результат
            return View(popularSeries);
        }
    }
}
