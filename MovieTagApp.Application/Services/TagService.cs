using AutoMapper;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Models.Tags;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Services
{
    public class TagService: BaseService
        <Tag, TagDTO, TagGetDTO>, ITagService
    {
        private readonly IMovieTagAppContext _context;
        private readonly IMapper _mapper;

        public TagService(
            IMovieTagAppContext context,
            IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
    
}
