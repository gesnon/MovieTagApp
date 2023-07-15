using AutoMapper;
using MovieTagApp.Application.Common.Exceptions;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Domain.Entities;

namespace MovieTagApp.Application.Services
{
    public class AddMovieRequestService : IAddMovieRequestService
    {
        private readonly IMovieTagAppContext _context;
        private readonly IMapper _mapper;

        public AddMovieRequestService(
            IMovieTagAppContext context,
            IMapper mapper) 
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task CreateAsync(int Kpid)
        {
            if (_context.AddMovieRequests.FirstOrDefault(_ => _.KpId == Kpid) != null)
            {
                throw new AlreadyInDBException("Такой фильм уже принят к рассмотрению");
            }
            if (_context.Movies.FirstOrDefault(_ => _.KinopoiskLink == $"https://www.kinopoisk.ru/film/{Kpid}/") !=null)
            {
                throw new AlreadyInDBException("Такой фильм уже есть на сайте");
            }

            AddMovieRequest request = new AddMovieRequest { KpId = Kpid, DateOfCreation=DateTime.Now };

            _context.AddMovieRequests.Add(request);

            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
