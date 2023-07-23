using MovieTagApp.Application.Models.Tags;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Interfaces
{
    public interface IParserService
    {
        public Task<TagRequest> GetTagsByMovieNameAsync(string movieName);
        public Task<string> GetMovieUrlAsync(string name);

        public Task<string> GetTitleFromIMDB(string IMDBId);
    }
}
