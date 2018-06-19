using System.Threading.Tasks;

namespace TMDb.Core
{
    public class MovieDetailsViewModel : BaseItemViewModel<MovieDetailsWrapper>
    {
        readonly IMovieDetailsService _movieDetailsService;
        MovieDetailsParameters _parameters;

        public MovieDetailsViewModel(IMovieDetailsService movieDetailsService,
                                     IDialogService dialogService)
                                        : base(Resource.MovieDetailsList)
        {
            _movieDetailsService = movieDetailsService;
            _dialogService = dialogService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            _parameters = navigationData as MovieDetailsParameters;
            await base.InitializeAsync();
        }

        protected override async Task<MovieDetailsWrapper> GetDataAsync()
            => await _movieDetailsService.Get(_parameters.Id);
    }
}
