using AutoMapper;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Models.Movies;
using MovieTagApp.Domain.Entities;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTagApp.Application.Models.MovieTags;

namespace MovieTagApp.Application.Services
{
    public class MovieService : BaseService
        <Movie, MovieDTO, MovieGetDTO>, IMovieService
    {
        private readonly IMovieTagAppContext _context;
        private readonly IMapper _mapper;
        private readonly IKinopoiskService _kinopoiskService;

        public MovieService(
            IMovieTagAppContext context,
            IMapper mapper,
            IKinopoiskService kinopoiskService) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _kinopoiskService = kinopoiskService;
        }

        public async Task<int> CreateMovieWithKinopoiskAPIAsync(int KinopoiskId)
        {
            MovieDTO dto = await _kinopoiskService.GetMovieFromKinopoisk(KinopoiskId);

            Movie movie = new Movie
            {
                Description = dto.Description,
                KinopoiskLink = dto.KinopoiskLink,
                NameEng = dto.NameEng,
                NameRu = dto.NameRu,
                Poster = dto.Poster,
                Rating = dto.Rating,
                MovieTags = new List<MovieTag>()
            };
            _context.Movies.Add(movie);

            await _context.SaveChangesAsync(CancellationToken.None);

            return movie.Id;
        }


        public async Task<MovieGetDTO> GetMovieDTOAsync(int id)
        {
            Movie movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null)
            {
                throw new Exception("Фильм не найден");
            }
            MovieGetDTO result = new MovieGetDTO
            {
                Description = movie.Description,
                KinopoiskLink = movie.KinopoiskLink,
                Id=movie.Id,
                MovieTagsDTOs = new List<MovieTagPreviewDTO>(),
                NameEng=movie.NameEng,
                NameRu=movie.NameRu,
                Poster=movie.Poster,
                Rating=movie.Rating
            };
            List<MovieTag> movieTags = _context.MovieTags.Where(_=>_.MovieId==id).ToList();

            List<Tag> tags = movieTags.Select(_ => _context.Tags.FirstOrDefault(i => i.Id == _.TagId)).ToList();

            List<MovieTagPreviewDTO> resultTags = tags
                .Select(_ => new MovieTagPreviewDTO { Id = _.Id, NameEng = _.NameEng, NameRu = _.NameRu }).ToList();

            result.MovieTagsDTOs=resultTags;

            return result;
        }
    }
}
