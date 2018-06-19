using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace TMDb.Core
{
    public class InfiniteScrollBehavior : Behavior<ListView>
    {
        ListView _listView;

        public static readonly BindableProperty LoadMoreCommandProperty =
            BindableProperty.Create(nameof(LoadMoreCommand), typeof(ICommand), typeof(InfiniteScrollBehavior), null);

        public ICommand LoadMoreCommand
        {
            get => (ICommand)GetValue(LoadMoreCommandProperty);
            set => SetValue(LoadMoreCommandProperty, value);
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            _listView = bindable;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
            bindable.ItemAppearing += InfiniteListView_ItemAppearing;
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
            => OnBindingContextChanged();

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = _listView.BindingContext;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
            bindable.ItemAppearing -= InfiniteListView_ItemAppearing;
        }

        void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (_listView.ItemsSource is IList items &&
                LoadMoreCommand?.CanExecute(null) == true &&
                items[items.Count - 1] == e.Item)
                LoadMoreCommand.Execute(null);
        }
    }
}
