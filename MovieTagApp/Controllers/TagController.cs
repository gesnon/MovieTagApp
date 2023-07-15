using Microsoft.AspNetCore.Mvc;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Models.Tags;

namespace MovieTagApp.Controllers
{
    public class TagController: BaseController
    {
        //GetTagsByNameAsync

        private readonly ITagService _tagService;

        public TagController(ITagService _tagService)
        {
            this._tagService = _tagService;
        }

        [HttpGet("GetTagsByName/{Name}")]
        public async Task<ActionResult> GetTagsByName(string? Name)
        {
            return Ok(await _tagService.GetTagsByNameAsync(Name));
        }

    }
}
