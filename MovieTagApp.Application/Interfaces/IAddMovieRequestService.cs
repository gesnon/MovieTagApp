﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Interfaces
{
    public interface IAddMovieRequestService
    {
        public Task CreateAsync(int Kpid);
    }
}
