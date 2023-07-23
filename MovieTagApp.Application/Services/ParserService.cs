using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;
using MovieTagApp.Application.Common.Exceptions;
using MovieTagApp.Application.Interfaces;
using MovieTagApp.Application.Models.Tags;
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
        private readonly IMovieTagAppContext _context;
        public ParserService(
            IMovieTagAppContext context)
        {
            _context = context;
        }

        public async Task<TagRequest> GetTagsByMovieNameAsync(string movieNameEng)
        {
            var moviewUrl = await GetMovieUrlAsync(movieNameEng);

            TagRequest result = new TagRequest {Tags=new List<string>()};

            if (string.IsNullOrEmpty(moviewUrl))
            {
                result.Status = Status.MovieNotFound;

                return result;
            }
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);
            string urlAddress = $"https://bestsimilar.com{moviewUrl}";
            using var doc = await context.OpenAsync(urlAddress);
            var tagsRaw = doc.QuerySelector("div.attr-tag-group-1 span.value").Text();
            var tags = tagsRaw.Split(',').Select(x => x.Trim()).ToList();
            
            result.Tags = tags;
            
            return result;
        }

        public async Task<string> GetMovieUrlAsync(string name)
        {
            string saveName = name;

            name = HttpUtility.UrlEncode(name).Replace("+", "%20");
            HttpClient client = new HttpClient();
            var response = await client.PostAsync($"https://bestsimilar.com/site/autocomplete?term={name}", null);
            var data = await response.Content.ReadAsStringAsync();
            var q = JsonSerializer.Deserialize<Autocomplete>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if(q.Movie.Length == 0)
            {
                return null;
            }

            if (q.Movie.Length == 1)
            {
                return q.Movie.FirstOrDefault()?.Url;

            }

            for (int i = 0; i < q.Movie.Length; i++)
            {
                string nameFromParser = q.Movie[i].Label.Substring(0, q.Movie[i].Label.LastIndexOf(' '));

                if (nameFromParser.Equals(saveName, StringComparison.OrdinalIgnoreCase))
                {
                    return q.Movie[i].Url;
                }

            }          

            return null;
        }

        public async Task<string> GetTitleFromIMDB(string IMDBId)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var config = Configuration.Default.WithDefaultLoader();
            DefaultHttpRequester httpRequester = (DefaultHttpRequester)config.Services.First(_=> _.GetType() == typeof(DefaultHttpRequester));
            httpRequester.Headers.Add("Accept-Language", "en-US;q=0.8,en;q=0.7");
            using var context = BrowsingContext.New(config);
            string urlAddress = $"https://www.imdb.com/title/{IMDBId}";
            using var doc = await context.OpenAsync(urlAddress);
            string docTitle = doc.Title;
            var title = doc.QuerySelector(".sc-afe43def-1").Text();
            
            return title;           
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
