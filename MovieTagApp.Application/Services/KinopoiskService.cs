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

        string token = "Z4JX270-H6S448M-NEMBZTP-7N0HBD5";
        public async Task<MovieDTO> GetMovieFromKinopoisk(int id)
        {

            GetByIdResponceDTO r = await GetMovieUrlByIdAsync(id);
            MovieDTO result = new MovieDTO
            {
                Description = r.Description,
                KinopoiskLink = $"https://www.kinopoisk.ru/film/{r.Id}/",
                NameEng = r.AlternativeName,
                NameRu = r.Name,
                Poster = r.Poster["url"],
                Rating = r.Rating["kp"].Value
            };

           // List<string> tags = await _parserService.GetTagsByMovieNameAsync(result.NameEng);

            return result;
        }

        private async Task<ResponceBody> GetMovieUrlAsync(int id)
        {
            var selectFields = new string[] { "rating", "id", "name", "description", "year", "poster", "alternativeName" };
            string selectField = string.Join(" ", selectFields);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-KEY", $"{token}");
           // var response = await client.GetAsync($"https://api.kinopoisk.dev/v1.3/movie?selectFields={selectField}&page=1&limit=5&name={name}");
            var responce2 = await client.GetAsync($"https://api.kinopoisk.dev/v1.3/movie/{id}");
            var data = await responce2.Content.ReadAsStringAsync();
            var q = JsonSerializer.Deserialize<ResponceBody>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return q;
        }

        private async Task<GetByIdResponceDTO> GetMovieUrlByIdAsync(int id)
        {
            var selectFields = new string[] { "rating", "id", "name", "description", "year", "poster", "alternativeName" };
            string selectField = string.Join(" ", selectFields);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-API-KEY", $"{token}");
            var responce = await client.GetAsync($"https://api.kinopoisk.dev/v1.3/movie/{id}");
            var data = await responce.Content.ReadAsStringAsync();
            var q = JsonSerializer.Deserialize<GetByIdResponceDTO>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return q;
        }
    }
}
