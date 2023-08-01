using Microsoft.AspNetCore.Mvc;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Models.Movies;

namespace MovieTagApp.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService _movieService)
        {
            this._movieService = _movieService;
        }

        [HttpPost("{kinopoiskId}")]
        public async Task CreateMovieWithKinopoiskAPIAsync(int kinopoiskId)
        {
            await _movieService.CreateMovieWithKinopoiskAPIAsync(kinopoiskId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDTOAsync(int id)
        {
            return Ok(await _movieService.GetMovieDTOAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> GetMovieListAsync([FromQuery] List<int> tags)
        {
            return Ok(await _movieService.GetMovieListAsync(tags));
        }

        [HttpGet]
        public async Task<ActionResult> GetMovieListByTagNameAsync([FromQuery] List<string> tags)
        {
            return Ok(await _movieService.GetMovieListAsync(tags));
        }
    }
}
