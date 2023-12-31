﻿using MovieTagApp.Application.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Interfaces
{
    public interface ITagService
    {
        public Task<int> CreateAsync(string Name);

        public Task <List<TagGetDTO>> GetTagsDTOByNameAsync(string? Name);

        public Task <List<string>> GetTagsByNameAsync(string? Name);

        public Task DeleteDublicates();
    }
}
