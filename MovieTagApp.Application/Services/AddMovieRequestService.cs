using AutoMapper;
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
                throw new Exception("Такой фильм уже принят к рассмотрению");
            }

            AddMovieRequest request = new AddMovieRequest { KpId = Kpid, DateOfCreation=DateTime.Now };

            _context.AddMovieRequests.Add(request);

            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
