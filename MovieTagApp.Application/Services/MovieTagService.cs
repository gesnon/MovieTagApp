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

        public MovieTagService(
            IMovieTagAppContext context,
            IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
    }
}
