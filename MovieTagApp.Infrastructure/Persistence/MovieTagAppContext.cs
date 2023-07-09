using Microsoft.EntityFrameworkCore;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTagApp.Infrastructure.Persistence
{
    public class MovieTagAppContext : DbContext, IMovieTagAppContext
    {

        public MovieTagAppContext(DbContextOptions<MovieTagAppContext> options) : base(options)
        {
        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieTag> MovieTags { get; set; }
        public DbSet<AddMovieRequest> AddMovieRequests { get; set; }       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieTagAppContext).Assembly);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            //modelBuilder.Entity<Tournament>()
            //    .HasOne(b => b.Bracket)
            //    .WithOne(i => i.Tournament).
            //    HasForeignKey<Bracket>(b => b.TournamentId);
        }
    }
}
