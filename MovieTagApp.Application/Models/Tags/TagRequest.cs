using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Models.Tags
{
    public class TagRequest
    {
        public Status Status { get; set; }
        public List<string> Tags { get; set; }
    }
}
