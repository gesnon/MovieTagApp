using AutoMapper;
using MovieTagApp.Application.Common.Exceptions;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Domain.Entities;

namespace MovieTagApp.Application.Services
{
    public class AddMovieRequestService : IAddMovieRequestService
    {
        private readonly IMovieTagAppContext _context;
        

        public AddMovieRequestService(
            IMovieTagAppContext context) 
        {
            this._context = context;            
        }

        public async Task CreateAsync(int Kpid)
        {
            if (_context.AddMovieRequests.Any(_ => _.KpId == Kpid))
            {
                throw new AlreadyInDBException("Такой фильм уже принят к рассмотрению");
            }
            if (_context.Movies.Any(_ => _.KinopoiskLink == $"https://www.kinopoisk.ru/film/{Kpid}/"))
            {
                throw new AlreadyInDBException("Такой фильм уже есть на сайте");
            }

            AddMovieRequest request = new AddMovieRequest { KpId = Kpid, DateOfCreation=DateTime.Now };

            _context.AddMovieRequests.Add(request);

            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
