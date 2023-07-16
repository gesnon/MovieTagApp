using MovieTagApp.Application.Common.Mappings;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Models.Movies
{
    public class ResponceDTO
    {     
        public Dictionary<string, decimal?> Rating { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Poster { get; set; }
        public string AlternativeName { get; set; }
        public int Id { get; set; }
    }
}
