using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Domain.Entities
{
    public class AddMovieRequest: Entity
    {        
        public int KpId { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
