using AutoMapper;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Models.Tags
{
    public class TagGetDTO
    {
        public int Id { get; set; }
        public string NameRu { get; set; }
        public string NameEng { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tag, TagGetDTO>()
            .ForMember(_ => _.Id, opt => opt.MapFrom(i => i.Id))
            .ForMember(_ => _.NameRu, opt => opt.MapFrom(i => i.NameRu))
            .ForMember(_ => _.NameEng, opt => opt.MapFrom(i => i.NameEng));

        }
    }
}
