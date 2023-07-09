using AutoMapper;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Models.MovieTags;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Services
{
    public class MovieTagService : BaseService
        <MovieTag, MovieTagDTO, MovieTagGetDTO>, IMovieTagService
    {
        private readonly IMovieTagAppContext _context;
        private readonly IMapper _mapper;
        private readonly IParserService _parserService;
        private readonly ITagService _tagService;

        public MovieTagService(
            IMovieTagAppContext context,
            IMapper mapper, 
            IParserService parserService,
            ITagService tagService) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _parserService = parserService;
            _tagService = tagService;
        }

        public async Task AddTagsToMovieAsync(int movieId)
        {
            
            Movie movie = _context.Movies.FirstOrDefault(x => x.Id == movieId);
            if (movie == null)
            {
                throw new Exception("Фильм не найден");
            }
            List<string> tags = await _parserService.GetTagsByMovieNameAsync(movie.NameEng);
            foreach (string tag in tags)
            {
                int tagId  = await _tagService.CreateAsync(tag);

                MovieTagDTO dto = new MovieTagDTO { MovieId = movieId, TagId = tagId };
                
                await CreateAsync(dto);                
            }

        }
        public async Task<int> CreateAsync(MovieTagDTO dto)
        {
                MovieTag movieTag = new MovieTag { MovieId=dto.MovieId, TagId = dto.TagId };

            _context.MovieTags.Add(movieTag);

            await _context.SaveChangesAsync(CancellationToken.None);

            return movieTag.Id;
        }
    }
}
