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
        // qwe
        public async Task<int> CreateAsync(string Name)
        {            
            
            Tag tag = new Tag { NameEng = Name, NameRu="" };
            _context.Tags.Add(tag);

            await _context.SaveChangesAsync(CancellationToken.None);

            return tag.Id;
        }

        public async Task<List<TagGetDTO>> GetTagsDTOByNameAsync(string? Name)
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

        public async Task<List<string>> GetTagsByNameAsync(string? Name)
        {
            var query = _context.Tags.AsQueryable();

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(x => x.NameRu.ToLower().Contains(Name.ToLower()));
            }

            List<string> result = query.Select(_ => _.NameRu).OrderBy(_ => _).Distinct().ToList();

            return result;
        }

        public async Task DeleteDublicates()
        {
            var query = _context.Tags.AsQueryable().GroupBy(_=>_.NameEng).ToList();

            foreach (var group in query)
            {   
                List<Tag> tags = group.ToList();
                List<int> tagsIds = tags.Select(_ => _.Id).ToList();

                List<MovieTag> movieTags = _context.MovieTags.Where(_ => tagsIds.Contains(_.TagId)).ToList();

                foreach(MovieTag mt in movieTags)
                {
                    mt.TagId = tagsIds[0];
                }

                _context.Tags.RemoveRange(tags.Skip(1));

                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
    
}
