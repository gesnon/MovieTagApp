using MovieTagApp.Application.Models.MovieTags;
using MovieTagApp.Application.Models.Tags;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Interfaces
{
    public interface IMovieTagService
    {
        public Task AddRuTagsToMovieAsync(int movieId, List<string> ruNames);
        public Task<MovieTagRequest> AddTagsToMovieAsync(string name);
    }
}
