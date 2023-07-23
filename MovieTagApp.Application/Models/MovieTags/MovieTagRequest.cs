using MovieTagApp.Application.Models.Tags;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Models.MovieTags
{
    public class MovieTagRequest
    {
        public List<MovieTag> MovieTags { get; set; }
        public Status Status { get; set; } 
    }
}
