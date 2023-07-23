using MovieTagApp.Application.Models.MovieTags;
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
        public Task<MovieTagRequest> AddTagsToMovieAsync(string name);
    }
}
