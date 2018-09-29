using System.Collections.ObjectModel;

namespace Company.Modules.Message.UI.ViewModels
{
    public interface IListPageViewModel
    {
        string Title { get; }
        Database.Message SelectedItem { get; }
        ReadOnlyObservableCollection<Database.Message> Messages { get; }
    }
}
