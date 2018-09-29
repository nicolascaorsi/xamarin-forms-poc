using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Company.Modules.Message.Database;
using Company.Modules.Message.UseCases;
using System.Collections.Generic;

namespace Company.Modules.Message.UI.ViewModels
{
    public class ListPageViewModel : ViewModelBase, IListPageViewModel
    {
        readonly IDownloadOlderMessagesUC downloadOlderUC;
        readonly IDownloadNewMessagesUC downloadNewMessagesUC;

        public ReadOnlyObservableCollection<Database.Message> Messages { get;  set; }

        public ICommand LoadLatestCommand => new DelegateCommand(async () => await OnLoadLatestAsync(), () => !IsLoadingLatest);

        public ICommand LoadOlderCommand => new DelegateCommand(async () => await OnLoadOlderAsync(), () => !IsLoadingOlder);

        public ICommand ItemTappedCommand => new DelegateCommand<Database.Message>(ItemTapped);

        public bool IsLoadingLatest { get; set; }

        public bool IsLoadingOlder { get; set; }

        public Database.Message SelectedItem { get; set; }

        public ListPageViewModel(IDownloadOlderMessagesUC downloadOlderUC,
                                 IDownloadNewMessagesUC downloadNewMessagesUC,
                                 IGetRecentMessagesUC getRecentMessagesUC,
                                 INavigationService navigationService) : base(navigationService)
        {
            Title = "Messages";
            this.downloadOlderUC = downloadOlderUC;
            this.downloadNewMessagesUC = downloadNewMessagesUC;
            Messages = getRecentMessagesUC.Execute();
        }

        async Task OnLoadLatestAsync()
        {
            IsLoadingLatest = true;
            try
            {
                await downloadNewMessagesUC.ExecuteAsync();
            }
            finally
            {
                IsLoadingLatest = false;
            }
        }

        async Task OnLoadOlderAsync()
        {
            IsLoadingOlder = true;
            try
            {
                await downloadOlderUC.ExecuteAsync();
            }
            finally
            {
                IsLoadingOlder = false;
            }
        }

        async void ItemTapped(Database.Message message)
        {
            await NavigationService.NavigateAsync($"{MessageModule.DetailPageUri}?id={message.Id}");
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            SelectedItem = null;
        }
    }
}