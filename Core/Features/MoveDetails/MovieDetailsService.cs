using System.Linq;
using System.Threading.Tasks;

namespace TMDb.Core
{
    public interface IMovieDetailsService
    {
        Task<MovieDetailsWrapper> Get(int id);
    }

    public class MovieDetailsService : BaseService, IMovieDetailsService
    {
        public async Task<MovieDetailsWrapper> Get(int id)
        {
            var response = await Api.GetMovieDetailsAsync(ConstantHelper.ApiKey, Language, id);
            var result = new MovieDetailsWrapper
            {
                Genres = string.Join(", ", response.Genres.Select(g => g.Name)),
                Id = response.Id,
                Name = response.Name,
                Overview = response.Overview,
                PosterPath = response.PosterPath.ToImagePath(),
                BackdropPath = response.BackdropPath.ToImagePath(),
                ReleaseDate = response.ReleaseDate,
                Tagline = response.Tagline,
                VoteAverage = response.VoteAverage
            };

            return result;
        }
    }
}
