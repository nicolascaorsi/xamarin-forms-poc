using System.ComponentModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace Company.Modules.Message.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        public virtual void OnNavigatedFrom(NavigationParameters parameters) { }

        public virtual void OnNavigatedTo(NavigationParameters parameters) { }

        public virtual void OnNavigatingTo(NavigationParameters parameters) { }

        public virtual void Destroy() { }
    }
}
