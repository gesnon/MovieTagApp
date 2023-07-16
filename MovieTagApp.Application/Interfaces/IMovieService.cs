using MovieTagApp.Application.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Interfaces
{
    public interface IMovieService 
    {
        public Task<int> CreateMovieWithKinopoiskAPIAsync(int kinopoiskId);
        public Task<MovieGetDTO> GetMovieDTOAsync(int id);
        public Task<List<MovieGetDTO>> GetMovieListAsync(List<int> tagsIds);
    }
}
