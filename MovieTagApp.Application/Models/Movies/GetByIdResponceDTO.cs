using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Models.Movies
{
    public class GetByIdResponceDTO
    {
        public Dictionary<string, decimal?> Rating { get; set; }
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }       
        
        public Dictionary<string, object> externalId { get; set; }
        public Dictionary<string, string> Poster { get; set; }   
        public string AlternativeName { get; set; }
        public List<MovieAlternativNameDTO> Names { get; set; }
    }
}
