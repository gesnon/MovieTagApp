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
using MovieTagApp.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using MovieTagApp.Application.Models.Tags;

namespace MovieTagApp.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieTagAppContext _context;
        private readonly IMapper _mapper;
        private readonly IKinopoiskService _kinopoiskService;
        private readonly IMovieTagService _movieTagService;
        public MovieService(
            IMovieTagAppContext context,
            IMapper mapper,
            IKinopoiskService kinopoiskService,
            IMovieTagService movieTagService) 
        {
            _context = context;
            _mapper = mapper;
            _kinopoiskService = kinopoiskService;
            _movieTagService = movieTagService;
        }

        public async Task<int> CreateMovieWithKinopoiskAPIAsync(int KinopoiskId)
        {
            MovieDTO dto = await _kinopoiskService.GetMovieFromKinopoisk(KinopoiskId);
            if (_context.Movies.Any(_=>_.KinopoiskLink == $"https://www.kinopoisk.ru/film/{KinopoiskId}/"))
            {
               // throw new AlreadyInDBException($"Такой фильм уже есть на сайте, его Id ");
            }           

            // Потом объеденю в один if
            if (string.IsNullOrEmpty(dto.NameEng))
            {

                if (_context.MovieWithNoTags.FirstOrDefault(_ => _.KpId == KinopoiskId) != null)
                {
                    throw new AlreadyInDBException("Такой фильм уже есть в БД и скоро будет обработан вручную");
                }

                _context.MovieWithNoTags.Add(new MovieWithNoTags { KpId = KinopoiskId });
                await _context.SaveChangesAsync(CancellationToken.None);
                throw new NotFoundException("Сайт с тегами не смог найти теги с этому фильму, они будут обработаны в ручную"); 
                
            }

            MovieTagRequest movieTagRequest = await _movieTagService.AddTagsToMovieAsync(dto.NameEng);

            if (movieTagRequest.Status == Status.MovieNotFound)
            {
                if (_context.MovieWithNoTags.FirstOrDefault(_ => _.KpId == KinopoiskId) != null)
                {
                    throw new AlreadyInDBException("Такой фильм уже есть в БД и скоро будет обработан вручную");
                }

                _context.MovieWithNoTags.Add(new MovieWithNoTags { KpId = KinopoiskId });
                await _context.SaveChangesAsync(CancellationToken.None);
                throw new NotFoundException("Сайт с тегами не смог найти теги с этому фильму, они будут обработаны в ручную");
            }

            Movie movie = new Movie
            {
                Description = dto.Description,
                KinopoiskLink = dto.KinopoiskLink,
                NameEng = dto.NameEng,
                NameRu = dto.NameRu,
                Poster = dto.Poster,
                Rating = dto.Rating,
                MovieTags = movieTagRequest.MovieTags
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
                throw new NotFoundException("Фильм не найден");
            }
            MovieGetDTO result = new MovieGetDTO
            {
                Description = movie.Description,
                KinopoiskLink = movie.KinopoiskLink,
                Id = movie.Id,                
                NameEng = movie.NameEng,
                NameRu = movie.NameRu,
                Poster = movie.Poster,
                Rating = movie.Rating
            };

            result.MovieTagsDTOs = _context.MovieTags
                .Include(_=>_.Tag)
                .Where(_=>_.MovieId == id)
                .Select(mt => new MovieTagPreviewDTO { Id= mt.Id, NameEng=mt.Tag.NameEng, NameRu=mt.Tag.NameRu })
                .ToList();
             

            return result;
        }

        public async Task<List<MovieGetDTO>> GetMovieListAsync(List<int> tags)
        {
            List<int> movies = _context.Movies.Select(_ => _.Id).ToList(); 
            if (tags.Count != 0)
            {
                movies = _context.MovieTags.Where(mt => tags.Contains(mt.TagId))
                .GroupBy(mt => mt.MovieId)
                .Where(g => g.Count() == tags.Count())
                .Select(g => g.Key).ToList();
            }            

            List<MovieGetDTO> result = new List<MovieGetDTO>();

            foreach (int i in movies)
            {
                MovieGetDTO dto = await GetMovieDTOAsync(i);
                result.Add(dto);
            }

            return result;
        }
    }
}
