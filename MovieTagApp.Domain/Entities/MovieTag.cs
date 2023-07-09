using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Domain.Entities
{
    public class MovieTag: Entity
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
