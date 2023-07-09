using MovieTagApp.Application.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Interfaces
{
    public interface IKinopoiskService
    {
        public Task<MovieDTO> GetMovieFromKinopoisk(int id);
    }
}
