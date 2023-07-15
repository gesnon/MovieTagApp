using Microsoft.AspNetCore.Mvc;
using MovieTagApp.Application.Interfaces;

namespace MovieTagApp.Controllers
{
    public class AddMovieRequestController: BaseController
    {
        private readonly IAddMovieRequestService _addMovieRequestService;

        public AddMovieRequestController(IAddMovieRequestService _addMovieRequestService)
        {
            this._addMovieRequestService = _addMovieRequestService;
        }

        [HttpPost("{kinopoiskId}")]
        public async Task CreateAsync(int kinopoiskId)
        {
            await _addMovieRequestService.CreateAsync(kinopoiskId);
        }
    }
}
