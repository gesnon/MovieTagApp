using Microsoft.AspNetCore.Mvc;
using MovieTagApp.Application.Interfaces;

namespace MovieTagApp.Controllers
{
    public class MovieTagController:BaseController
    {
        private readonly IMovieTagService _movieTagService;
        public MovieTagController(IMovieTagService _movieTagService)
        {
            this._movieTagService = _movieTagService;
        }

        [HttpPost("{movieId}")]
        public async Task AddTagsToMovieAsync(int movieId)
        {
            await _movieTagService.AddTagsToMovieAsync(movieId);
        }
    }
}
