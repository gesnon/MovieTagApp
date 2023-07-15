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
        public Task<List<string>> GetTagsByMovieNameAsync(string movieName);
        public Task<string> GetMovieUrlAsync(string name);
    }
}
