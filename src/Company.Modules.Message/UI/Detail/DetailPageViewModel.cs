using System;
using System.ComponentModel;
using Prism.Navigation;
using Company.Modules.Message.Database;
using Company.Modules.Message.UseCases;
using Company.Modules.Message.UI.ViewModels;
using Xamarin.Forms;

namespace Company.Modules.Message.UI.Detail
{
    public class DetailPageViewModel : ViewModelBase
    {
        readonly IGetMessageUC getMessageUC;
        public Database.Message Message { get; private set; }

        public DetailPageViewModel(INavigationService navigationService, IGetMessageUC getMessageUC) :base(navigationService)
        {
            this.getMessageUC = getMessageUC;
            PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
                Console.WriteLine("Changed" + e.PropertyName);
            };
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var messageId = parameters["id"]?.ToString();
            Message = getMessageUC.Execute(messageId);
            Title = Message.Subject;
        }
    }
}

