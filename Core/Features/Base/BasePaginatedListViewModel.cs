using Plugin.Connectivity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TMDb.Core
{
    public abstract class BasePaginatedListViewModel<T> : BaseDataViewModel<IEnumerable<T>> where T : class
    {
        protected virtual async Task ExecuteItemClickCommand(T item) { }

        protected int Page { get; private set; }
        bool canLoad = true;

        public ICommand ItemClickCommand { get; }
        public ICommand LoadMoreCommand { get; }

        public ObservableCollection<T> Items { get; }

        protected BasePaginatedListViewModel(string title) : base(title)
        {
            Items = new ObservableCollection<T>();
            ItemClickCommand = new Command<T>(async (item) => await ExecuteItemClickCommand(item));
            LoadMoreCommand = new Command(async () => await LoadMoreItemsAsync(), () => canLoad);
            Page = 1;
        }

        public override async Task InitializeAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await InternetNotConnectedAlert();
                await InitializeAsync();
                return;
            }
            await base.InitializeAsync();
        }

        async Task LoadMoreItemsAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await InternetNotConnectedAlert();
                return;
            }

            await base.InitializeAsync();
        }

        protected override async Task SetDataLoadedAsync(IEnumerable<T> data)
        {
            Items.AddRange(data);
            Page++;
            canLoad = data.Any();
        }
    }
}
