using AngleSharp;
using AngleSharp.Dom;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace MovieTagApp.Application.Services
{

    public class ParserService : IParserService
    {
        public async Task<List<string>> GetTagsByMovieNameAsync(string movieNameEng)
        {
            var moviewUrl = await GetMovieUrlAsync(movieNameEng);
            if (string.IsNullOrEmpty(moviewUrl))
            {
                 
            }
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);
            string urlAddress = $"https://bestsimilar.com{moviewUrl}";
            using var doc = await context.OpenAsync(urlAddress);
            var tagsRaw = doc.QuerySelector("div.attr-tag-group-1 span.value").Text();
            var tags = tagsRaw.Split(',').Select(x => x.Trim()).ToList();
            return tags;
        }

        private async Task<string> GetMovieUrlAsync(string name)
        {
            name = HttpUtility.UrlEncode(name).Replace("+", "%20");
            HttpClient client = new HttpClient();
            var response = await client.PostAsync($"https://bestsimilar.com/site/autocomplete?term={name}", null);
            var data = await response.Content.ReadAsStringAsync();
            var q = JsonSerializer.Deserialize<Autocomplete>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return q?.Movie?.FirstOrDefault()?.Url;
        }

        
    }

    public class Autocomplete
    {
        public AutocompleteMovie[] Movie { get; set; }
    }
    public class AutocompleteMovie
    {
        public string Label { get; set; }
        public string Url { get; set; }
    }
}
