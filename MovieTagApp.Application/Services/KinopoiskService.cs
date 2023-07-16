using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieTagApp.Application.Services
{
    public class KinopoiskService : IKinopoiskService
    {
        private readonly IParserService _parserService;

        public KinopoiskService(IParserService _parserService)
        {
            this._parserService = _parserService;
        }

        // Надо перенести токен в appsettings
        string token = "Z4JX270-H6S448M-NEMBZTP-7N0HBD5";

        string [] ValidLanguages = { "us", "gb" };

        public async Task<MovieDTO> GetMovieFromKinopoisk(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-KEY", $"{token}");
            var responce = await client.GetAsync($"https://api.kinopoisk.dev/v1.3/movie/{id}");
            var data = await responce.Content.ReadAsStringAsync();

            if (!responce.IsSuccessStatusCode)
            {
                throw new Exception("Кинопоиск не вернул данные");
            }

            var movie = JsonSerializer.Deserialize<GetByIdResponceDTO>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        
            MovieDTO result = new MovieDTO
            {
                Description = movie.Description,
                KinopoiskLink = $"https://www.kinopoisk.ru/film/{movie.Id}/",
                NameEng = movie.AlternativeName,
                NameRu = movie.Name,
                Poster = movie.Poster["url"],
                Rating = movie.Rating["kp"].Value
            };

            List<MovieAlternativNameDTO> alternativNames = movie.Names
                .Where(_=>ValidLanguages.Contains(_.Language, StringComparer.OrdinalIgnoreCase)).ToList();     
 

            foreach (var alternativName in alternativNames)
            {
                string url = await _parserService.GetMovieUrlAsync(alternativName.Name);

                if (!string.IsNullOrEmpty(url))
                {
                    result.NameEng = alternativName.Name;
                    break;
                }
            }            

            return result;
        }        

    }
}
