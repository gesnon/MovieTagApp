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
            ResponceBody r = await GetMovieUrlAsync(id);

            MovieDTO result = new MovieDTO
            {
                Description = r.docs[0].Description,
                KinopoiskLink = $"https://www.kinopoisk.ru/film/{r.docs[0].Id}/",
                NameEng = r.docs[0].AlternativeName,
                NameRu = r.docs[0].Name,
                Poster = r.docs[0].Poster["url"],
                Rating = r.docs[0].Rating["kp"].Value
            };

            List<string> tags = await _parserService.GetTagsByMovieNameAsync(result.NameEng);

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
    }
}
