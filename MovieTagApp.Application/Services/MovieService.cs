using AutoMapper;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Models.Movies;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Services
{
    public class MovieService: BaseService
        <Movie, MovieDTO, MovieGetDTO>, IMovieService
    {
        private readonly IMovieTagAppContext _context;
        private readonly IMapper _mapper;

        public MovieService(
            IMovieTagAppContext context,
            IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
