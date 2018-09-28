using System;
using System.Collections;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace NarcForms.Behaviors
{
    public class InfiniteScrollBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty IsLoadingMoreProperty =
            BindableProperty.Create(
                nameof(IsLoadingMore),
                typeof(bool),
                typeof(InfiniteScrollBehavior),
                default(bool),
                BindingMode.OneWayToSource);

        public static readonly BindableProperty TriggerPercentProperty =
            BindableProperty.Create(
                nameof(TriggerPercent),
                typeof(byte),
                typeof(InfiniteScrollBehavior),
                (byte)80,
                BindingMode.OneWayToSource);

        public static readonly BindableProperty LoadMoreCommandProperty =
            BindableProperty.Create(
                nameof(TriggerPercent),
                typeof(ICommand),
                typeof(InfiniteScrollBehavior),
                null);

        public ICommand LoadMoreCommand
        {
            get => (ICommand)GetValue(LoadMoreCommandProperty);
            private set => SetValue(LoadMoreCommandProperty, value);
        }

        public byte TriggerPercent
        {
            get => (byte)GetValue(TriggerPercentProperty);
            private set => SetValue(TriggerPercentProperty, value);
        }

        public bool IsLoadingMore
        {
            get => (bool)GetValue(IsLoadingMoreProperty);
            private set => SetValue(IsLoadingMoreProperty, value);
        }

        int ItemsCount
        {
            get
            {
                if (listView.ItemsSource is IList list)
                {
                    return list.Count;
                }
                else
                {
                    return listView.ItemsSource.OfType<Object>().Count();
                }
            }
        }
        ListView listView;

        protected override void OnAttachedTo(ListView listView)
        {
            base.OnAttachedTo(listView);

            this.listView = listView;

            //SetBinding(ItemsSourceProperty, new Binding(ListView.ItemsSourceProperty.PropertyName, source: listView));
            listView.BindingContextChanged += ListView_BindingContextChanged;
            listView.ItemAppearing += ListView_ItemAppearing;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            listView.BindingContextChanged -= ListView_BindingContextChanged;
            listView.ItemAppearing += ListView_ItemAppearing;
        }

        void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var canLoadMoreItems = LoadMoreCommand != null && LoadMoreCommand.CanExecute(null);
            if (canLoadMoreItems)
            {
                object lastItem;
                if (listView.ItemsSource is IList list)
                {
                    lastItem = list[list.Count - 1];
                }
                else
                {
                    lastItem = listView.ItemsSource.OfType<Object>().Last();
                }

                if (e.Item == lastItem)
                {
                    try
                    {
                        LoadMoreCommand?.Execute(null);
                    }
                    finally
                    {
                        IsLoadingMore = false;
                    }
                }
            }
        }


        void ListView_BindingContextChanged(object sender, EventArgs e)
        {
            base.OnBindingContextChanged();

            if (!(sender is BindableObject listView))
                return;

            BindingContext = listView.BindingContext;
        }

    }
}