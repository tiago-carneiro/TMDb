using System.Collections.Generic;
using System.Threading.Tasks;

namespace TMDb.Core
{
    public class MovieListViewModel : BasePaginatedListViewModel<MovieItemWrapper>
    {
        readonly IUpcomingMovieService _upcomingMovieService;
        readonly INavigationService _navigationService;

        string _query;
        public string Query
        {
            get => _query;
            set => SetProperty(ref _query, value);
        }

        public MovieListViewModel(IUpcomingMovieService upcomingMovieService,
                                  INavigationService navigationService,
                                  IDialogService dialogService)
                                : base(Resource.MovieListTitle)
        {
            _upcomingMovieService = upcomingMovieService;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        protected override async Task<IEnumerable<MovieItemWrapper>> GetDataAsync()
            => await _upcomingMovieService.Get(Page, Query);

        protected override async Task ExecuteItemClickCommand(MovieItemWrapper item)
            => await _navigationService.NavigateToAsync<MovieDetailsViewModel>(new MovieDetailsParameters(item.Id));
    }
}
