using HFilms.Models.ViewModels;
using HFilms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HFilms.Infrastructure;

namespace HFilms.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
		private readonly DataContext _context;

		public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = dataContext;
        }

		[Authorize]
		public async Task<IActionResult> Profile()
        {
            // Получите текущего пользователя
            var user = await _userManager.GetUserAsync(User);

            // Проверьте, что пользователь не равен null
            if (user != null)
            {
				var films = _context.FavoriteFilms.Where(o => o.UserId == user.Id).ToList();
				// Передайте информацию о пользователе и избранных фильмах в модель представления
				var profileViewModel = new ProfileViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FavoriteFilms = films.ToList() // Преобразуйте в список, если это необходимо
				};

                return View(profileViewModel);
            }

            // Верните ошибку, если пользователь не найден
            return NotFound("Пользователь не найден.");
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(string Id, string title, string year, string rating, string posterUrl)
        {
            // Получите текущего пользователя с подгрузкой свойства FavoriteFilms
            var user = await _userManager.Users
                .Include(u => u.FavoriteFilms)
                .SingleOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

            // Проверьте, что пользователь не равен null и что свойство FavoriteFilms пользователя инициализировано
            if (user != null && user.FavoriteFilms != null)
            {
                // Проверьте, не добавлен ли уже такой фильм в список для данного пользователя
                var isDuplicate = user.FavoriteFilms.Any(f =>
                    string.Equals(f.FilmsID, Id, StringComparison.OrdinalIgnoreCase) && f.UserId == user.Id);

                if (!isDuplicate)
                {
                    // Создайте новый объект FavoriteFilm для каждого фильма
                    var favoriteFilm = new FavoriteFilm
                    {
                        UserId = user.Id,
                        FilmsID = Id,
                        PrimaryTitle = title,
                        Year = year,
                        Rating = rating,
                        PosterUrl = posterUrl,
                    };

                    // Добавьте фильм в список любимых фильмов пользователя
                    user.FavoriteFilms.Add(favoriteFilm);

                    // Сохраните изменения в базе данных
                    await _userManager.UpdateAsync(user);

                    // Верните успешный статус или другой ответ
                    return Ok("Фильм успешно добавлен в избранное.");
                }
                else
                {
                    // Фильм уже добавлен в список для данного пользователя
                    // Верните ошибку или другой соответствующий статус
                    return BadRequest("Фильм уже в избранном у этого пользователя.");
                }
            }

            // Верните ошибку, если пользователь не найден или свойство FavoriteFilms не инициализировано
            return NotFound("User not found or FavoriteFilms not initialized.");
        }





        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser { UserName = user.UserName, Email = user.Email };
                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);

                if (result.Succeeded)
                {
                    // Назначение роли "Admin" только конкретному пользователю с именем "He11Cut3"
                    if (user.UserName == "He11Cut3")
                    {
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, "User");
                    }
                    return Redirect("/account/login");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(user);
        }
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);

                if (result.Succeeded)
                {
                    return Redirect(loginVM.ReturnUrl ?? "/");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(loginVM);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }

	}
}
