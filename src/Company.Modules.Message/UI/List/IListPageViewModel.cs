using System.Collections.ObjectModel;

namespace Company.Modules.Message.UI.ViewModels
{
    public interface IListPageViewModel
    {
        string Title { get; }
        Database.Message SelectedItem { get; }
        ObservableCollection<Database.Message> Messages { get; }
    }
}
