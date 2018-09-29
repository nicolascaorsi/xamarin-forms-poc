using System;
using System.Linq;
using System.Collections.ObjectModel;
using Realms;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using System.Collections.Generic;

namespace Company.Modules.Message.UseCases
{
    public class GetRecentMessagesUC : IGetRecentMessagesUC, IDisposable
    {
        readonly IDownloadNewMessagesUC downloadRecentUC;
        readonly IDisposable messageSubscription;
        readonly ObservableCollection<Database.Message> messages;

        public GetRecentMessagesUC(IDownloadNewMessagesUC downloadRecentUC)
        {
            this.downloadRecentUC = downloadRecentUC;
            this.messages = new ObservableCollection<Database.Message>();
            this.messageSubscription = Realm.GetInstance()
                .All<Database.Message>()
                .OrderByDescending(m => m.SendAt)
                .SubscribeForNotificationsOn(messages);
        }

        public ReadOnlyObservableCollection<Database.Message> Execute()
        {
            Task.Run(downloadRecentUC.ExecuteAsync);
            return new ReadOnlyObservableCollection<Database.Message>(messages);
        }

        public void Dispose() => messageSubscription?.Dispose();
    }
}