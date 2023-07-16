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

        [HttpPost("{movieName}")]
        public async Task AddTagsToMovieAsync(string movieName)
        {
            await _movieTagService.AddTagsToMovieAsync(movieName);
        }
    }
}
