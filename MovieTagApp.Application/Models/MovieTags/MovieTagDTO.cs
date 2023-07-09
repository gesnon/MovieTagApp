using AutoMapper;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Models.MovieTags
{
    public class MovieTagDTO
    {
        public int MovieId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MovieTagDTO,MovieTag>()
            .ForMember(_ => _.MovieId, opt => opt.MapFrom(i => i.MovieId));
        }
    }
}
