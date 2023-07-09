using Microsoft.EntityFrameworkCore;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Interfaces
{
    public interface IMovieTagAppContext
    {

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieTag> MovieTags { get; set; }
        public DbSet<AddMovieRequest> AddMovieRequests { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
