using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieTagApp.Application.Common.Exceptions;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Models.MovieTags;
using MovieTagApp.Application.Models.Tags;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Services
{
    public class MovieTagService : IMovieTagService
    {
        private readonly IMovieTagAppContext _context;
        private readonly IMapper _mapper;
        private readonly IParserService _parserService;
        private readonly ITagService _tagService;

        public MovieTagService(
            IMovieTagAppContext context,
            IMapper mapper, 
            IParserService parserService,
            ITagService tagService)
        {
            _context = context;
            _mapper = mapper;
            _parserService = parserService;
            _tagService = tagService;
        }


        public async Task AddRuTagsToMovieAsync(int movieId, List<string> ruTags)
        {
            Movie movie = _context.Movies.FirstOrDefault(_=>_.Id==movieId);

            List<Tag> tagsFromBase = await _context.Tags.Where(t=>ruTags.Contains(t.NameRu))
                .GroupBy(_=>_.NameRu).Select(_=>_.First()).ToListAsync();

            foreach(string tag in ruTags)
            {
                Tag tagFromDB = tagsFromBase.FirstOrDefault(_ => _.NameRu.ToLower() == tag.ToLower());
                if (tagFromDB != null)
                {
                    _context.MovieTags.Add(new MovieTag { MovieId = movieId, TagId = tagFromDB.Id });
                   
                }

                if(tagFromDB == null)
                {
                    Tag newTag = new Tag { AreUsefull = true, NameRu=tag, NameEng=""};

                    _context.MovieTags.Add(new MovieTag { MovieId = movieId, Tag=newTag });
                }


                await _context.SaveChangesAsync(CancellationToken.None);
            }
        }




        public async Task<MovieTagRequest> AddTagsToMovieAsync(string movieName)
        {              
            MovieTagRequest result = new MovieTagRequest { MovieTags=new List<MovieTag>()};

            List<Tag> tagsFromBase = _context.Tags.ToList();            

            List<string> tagsToString = _context.Tags.Select(_=>_.NameEng).ToList();

            TagRequest tagRequest = await _parserService.GetTagsByMovieNameAsync(movieName);

            List<string> tags = tagRequest.Tags;

            result.Status = tagRequest.Status;

            if (tagRequest.Status == Status.MovieNotFound)
            {
                return result;
            }
            
            // Здесь происходит проверка на наличие тега в базе, для того чтобы избежать дублирования,
            // возможно ещё нужно приводить к нижнему регистру
            
            List<MovieTag> tagsFromMovie = new List<MovieTag>();

            foreach (string tag in tags)
            {
                int tagId=0;

                if (tagsToString.Contains(tag))
                {
                   tagId= tagsFromBase.FirstOrDefault(_=>_.NameEng.ToLower() == tag.ToLower()).Id;
                }

                if (!tagsToString.Contains(tag))
                {
                    tagId = await _tagService.CreateAsync(tag);
                }                

                tagsFromMovie.Add(new MovieTag { TagId=tagId });                
                             
            }
            result.MovieTags = tagsFromMovie;

            return result;
        }
        
    }
}
