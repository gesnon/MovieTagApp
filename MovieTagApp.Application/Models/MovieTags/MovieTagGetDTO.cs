using AutoMapper;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Models.MovieTags
{
    public class MovieTagGetDTO
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int MovieId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MovieTag, MovieTagGetDTO>()
            .ForMember(_ => _.Id, opt => opt.MapFrom(i => i.Id))
            .ForMember(_ => _.TagId, opt => opt.MapFrom(i => i.TagId))
            .ForMember(_ => _.MovieId, opt => opt.MapFrom(i => i.MovieId));
        }
    }
}
