using AutoMapper;
using MovieTagApp.Application.Models.MovieTags;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Models.Movies
{
    public class MovieDTO
    {       
        public string NameRu { get; set; }
        public string NameEng { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
        public List<MovieTagGetDTO> MovieTagsDTOs { get; set; }
        public decimal Rating { get; set; }
        public string KinopoiskLink { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MovieDTO,Movie>()
            .ForMember(_ => _.NameRu, opt => opt.MapFrom(i => i.NameRu))
            .ForMember(_ => _.NameEng, opt => opt.MapFrom(i => i.NameEng))
            .ForMember(_ => _.Poster, opt => opt.MapFrom(i => i.Poster))
            .ForMember(_ => _.Description, opt => opt.MapFrom(i => i.Description))
            .ForMember(_ => _.MovieTags, opt => opt.Ignore())
            .ForMember(_ => _.Rating, opt => opt.MapFrom(i => i.Rating))
            .ForMember(_ => _.KinopoiskLink, opt => opt.MapFrom(i => i.KinopoiskLink));

        }
    }
}
