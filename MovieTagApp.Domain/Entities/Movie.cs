using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Domain.Entities
{
    public class Movie : Entity
    {
        public string NameRu { get; set; }
        public string NameEng { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public List<MovieTag> MovieTags { get; set; }
        public string KinopoiskLink { get; set; }
    }
}
