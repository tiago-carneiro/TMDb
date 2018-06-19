using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TMDb.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            PrepareContainer();
            ConfigureMap();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await InitializeAsyc();
        }

        void PrepareContainer()
        {
            ViewModelLocator.Instance.RegisterSingleton<INavigationService, NavigationService>();
            ViewModelLocator.Instance.RegisterSingleton<IDialogService, DialogService>();
            ViewModelLocator.Instance.Register<IUpcomingMovieService, UpcomingMovieService>();
            ViewModelLocator.Instance.Register<IMovieDetailsService, MovieDetailsService>();

            ViewModelLocator.Instance.Register<MovieListViewModel>();
            ViewModelLocator.Instance.Register<MovieDetailsViewModel>();

            ViewModelLocator.Instance.Build();
        }

        void ConfigureMap()
        {
            NavigationService.ConfigureMap<MovieListViewModel, MovieListPage>();
            NavigationService.ConfigureMap<MovieDetailsViewModel, MovieDetailsPage>();
        }

        async Task InitializeAsyc()
           => await ViewModelLocator.Instance.Resolve<INavigationService>().NavigateToAsync<MovieListViewModel>();
    }
}
