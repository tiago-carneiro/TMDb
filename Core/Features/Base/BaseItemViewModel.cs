using Plugin.Connectivity;
using System.Threading.Tasks;

namespace TMDb.Core
{
    public abstract class BaseItemViewModel<T> : BaseDataViewModel<T> where T : class
    {
        T _item;
        public T Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        protected BaseItemViewModel(string title)
            : base(title) { }

        public async override Task InitializeAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await InternetNotConnectedAlert();
                return;
            }

            await base.InitializeAsync();
        }

        protected override async Task SetDataLoadedAsync(T data)
            => Item = data;
    }
}
