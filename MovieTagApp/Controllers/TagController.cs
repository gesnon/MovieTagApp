using Microsoft.AspNetCore.Mvc;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Models.Tags;

namespace MovieTagApp.Controllers
{
    public class TagController : BaseController
    {


        private readonly ITagService _tagService;

        public TagController(ITagService _tagService)
        {
            this._tagService = _tagService;
        }

        [HttpGet("Dev")]
        public async Task<ActionResult> GetTagsDTOByName([FromQuery] string? Name)
        {
            return Ok(await _tagService.GetTagsDTOByNameAsync(Name));
        }

        [HttpGet]
        public async Task<ActionResult> GetTagsByName([FromQuery] string? Name)
        {
            return Ok(await _tagService.GetTagsByNameAsync(Name));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDublicates()
        {
            await _tagService.DeleteDublicates();

            return Ok();
        }
    }
}
