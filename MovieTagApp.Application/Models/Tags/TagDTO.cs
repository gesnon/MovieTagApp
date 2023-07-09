using AutoMapper;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Models.Tags
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string NameEng { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TagGetDTO,Tag>()
            .ForMember(_ => _.Id, opt => opt.MapFrom(i => i.Id))            
            .ForMember(_ => _.NameEng, opt => opt.MapFrom(i => i.NameEng))
            .ForMember(_ => _.NameRu, opt => opt.Ignore());

        }
    }
}
