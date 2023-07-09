using Microsoft.AspNetCore.Mvc;
using MovieTagApp.Application.Interfaces;

namespace MovieTagApp.Controllers
{
    public class KinopoiskController : BaseController
    {
        private readonly IKinopoiskService _kinopoiskService;

        public KinopoiskController(IKinopoiskService _kinopoiskService)
        {
            this._kinopoiskService = _kinopoiskService;
        }

        [HttpGet("{id}")]
        public async Task GetAsync(int id)
        {
            await _kinopoiskService.GetMovieFromKinopoisk(id);
        }
    }
}
