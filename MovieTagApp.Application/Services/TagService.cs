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
    public class TagService: ITagService
    {
        private readonly IMovieTagAppContext _context;
        private readonly IMapper _mapper;

        public TagService(
            IMovieTagAppContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(string Name)
        {            
            
            Tag tag = new Tag { NameEng = Name, NameRu="" };
            _context.Tags.Add(tag);

            await _context.SaveChangesAsync(CancellationToken.None);

            return tag.Id;
        }

        public async Task<List<TagGetDTO>> GetTagsByNameAsync(string? Name)
        {
            var query = _context.Tags.AsQueryable();

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(x => x.NameRu.ToLower().Contains(Name.ToLower()));
            }

            List<TagGetDTO> result = query.Select(_ => new TagGetDTO
            {
                Id = _.Id,
                NameEng = _.NameEng,
                NameRu = _.NameRu
            }).OrderBy(_=>_.Id).ToList();

            return result;
        }
    }
    
}
