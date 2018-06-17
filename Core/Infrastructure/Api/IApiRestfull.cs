using Refit;
using System.Threading.Tasks;

namespace TMDb.Core
{
    [Headers("Content-Type: application/json")]
    public interface IApiRestfull
    {
        [Get("/3/movie/upcoming?api_key={apiKey}&language={lan}&page={p}")]
        Task<MovieApiResponse> GetMovieUpcomingAsync(string apiKey, string lan, int p);

        [Get("/3/movie/{id}?api_key={apiKey}&language={lan}")]
        Task<MovieDetailResponse> GetMovieDetailsAsync(string apiKey, string lan, int id);

        [Get("/3/genre/movie/list?api_key={apiKey}&language={lan}")]
        Task<GenresApiResponse> GetMovieGenreAsync(string apiKey, string lan);

        [Get("/3/search/movie?api_key={apiKey}&language={lan}&page={p}&query={query}")]
        Task<MovieApiResponse> GetMovieSearchAsync(string apiKey, string lan, int p, string query);
    }
}
