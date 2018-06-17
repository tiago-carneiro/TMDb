using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMDb.Core
{
    public interface IUpcomingMovieService
    {
        Task<IEnumerable<MovieItemWrapper>> Get(int page, string query);
    }

    public class UpcomingMovieService : BaseService, IUpcomingMovieService
    {
        IEnumerable<GenresWrapper> _genres;

        public async Task<IEnumerable<MovieItemWrapper>> Get(int page = 1, string query = "")
        {
            await LoadGenres();

            MovieApiResponse response;
            if (!string.IsNullOrEmpty(query))
                response = await Api.GetMovieSearchAsync(ConstantHelper.ApiKey, Language, page, query);
            else
                response = await Api.GetMovieUpcomingAsync(ConstantHelper.ApiKey, Language, page);

            var result = response.Movies.Select(m => new MovieItemWrapper
            {
                Genres = string.Join(", ", _genres.Where(w => m.GenreIds.Contains(w.Id)).Select(g => g.Name)),
                Id = m.Id,
                Name = m.Name,
                PosterPath = m.PosterPath.ToImagePath(),
                BackdropPath = m.BackdropPath.ToImagePath(),
                ReleaseDate = m.ReleaseDate
            });

            return result;
        }

        async Task LoadGenres()
        {
            if (_genres == null)
            {
                var movieGenre = await Api.GetMovieGenreAsync(ConstantHelper.ApiKey, Language);
                _genres = movieGenre.Genres.Select(g => new GenresWrapper { Id = g.Id, Name = g.Name });
            }
        }
    }
}
