using AutoMapper;
using MovieTagApp.Application.Common.Mappings;
using MovieTagApp.Application.Models.MovieTags;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Models.Movies
{
    public class MovieGetDTO:IMapFrom<Movie>
    {
        public int Id { get; set; }
        public string NameRu { get; set; }
        public string NameEng { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }        
        public decimal Rating { get; set; }
        public string KinopoiskLink { get; set; }

        public List<MovieTagPreviewDTO> MovieTagsDTOs { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Movie, MovieGetDTO>()
            .ForMember(_ => _.Id, opt => opt.MapFrom(i => i.Id))
            .ForMember(_ => _.NameRu, opt => opt.MapFrom(i => i.NameRu))
            .ForMember(_ => _.NameEng, opt => opt.MapFrom(i => i.NameEng))
            .ForMember(_ => _.Poster, opt => opt.MapFrom(i => i.Poster))
            .ForMember(_ => _.Description, opt => opt.MapFrom(i => i.Description))
            .ForMember(_ => _.MovieTagsDTOs, opt => opt.MapFrom(i => i.MovieTags))
            .ForMember(_ => _.Rating, opt => opt.MapFrom(i => i.Rating))
            .ForMember(_ => _.KinopoiskLink, opt => opt.MapFrom(i => i.KinopoiskLink));

        }
    }
}
