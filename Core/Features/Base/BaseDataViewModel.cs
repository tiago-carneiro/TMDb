using System.Threading.Tasks;

namespace TMDb.Core
{
    public abstract class BaseDataViewModel<T> : BaseViewModel where T : class
    {
        bool _dataLoaded;
        public bool DataLoaded
        {
            get { return _dataLoaded; }
            private set { SetProperty(ref _dataLoaded, value); }
        }

        protected BaseDataViewModel(string title) : base(title)
            => DataLoaded = false;

        public async override Task InitializeAsync(object navigationData)
           => await InitializeAsync();

        public async override Task InitializeAsync()
            => await LoadDataAsync();

        protected abstract Task<T> GetDataAsync();

        protected abstract Task SetDataLoadedAsync(T data);

        protected virtual async Task OnDataLoadedAsync()
            => DataLoaded = true;

        protected virtual async Task OnDataLoadErrorAsync(string msg)
        {
            var result = await _dialogService.Display(Resource.DefaultDialogTitle, msg, Resource.TryAgain, Resource.DefaultGoBack);
            if (result)
                await LoadDataAsync();
        }

        async Task LoadDataAsync()
        {
            var result = await GetDataAsync().WithBusyIndicator(this);

            if (result?.Success != true || result?.Data == null)
            {
                await OnDataLoadErrorAsync(result?.Message ?? Resource.DefaultLoadError);
                return;
            }
            await SetDataLoadedAsync(result.Data);
            await OnDataLoadedAsync();
        }

        protected async Task DisplayAlert(string msg)
            => await _dialogService.Display(Resource.DefaultDialogTitle, msg, Resource.DefaultGoBack);

        protected async Task InternetNotConnectedAlert()
            => await DisplayAlert(Resource.InternetNotConnected);
    }
}
