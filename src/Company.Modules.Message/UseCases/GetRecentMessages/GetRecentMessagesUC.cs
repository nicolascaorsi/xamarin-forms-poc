using System;
using System.Linq;
using System.Collections.ObjectModel;
using Realms;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using System.Collections.Generic;
using Company.Modules.Message.Extensions;
using Company.Modules.Message.Database;

namespace Company.Modules.Message.UseCases
{
    public class GetRecentMessagesUC : IGetRecentMessagesUC, IDisposable
    {
        readonly IDownloadNewMessagesUC downloadRecentUC;
        readonly IDisposable messageSubscription;
        readonly ReadOnlyObservableCollection<Database.Message> readOnlyMessages;
        readonly ObservableCollection<Database.Message> messages;

        public GetRecentMessagesUC(IDownloadNewMessagesUC downloadRecentUC)
        {
            this.downloadRecentUC = downloadRecentUC;
            this.messages = new ObservableCollection<Database.Message>();
            this.messageSubscription = Realm.GetInstance()
                .All<Database.Message>()
                .OrderByDescending(m => m.SendAt)
                .SubscribeForNotificationsOn(messages);
            this.readOnlyMessages = messages.ToReadOnly();
        }

        public ReadOnlyObservableCollection<Database.Message> Execute()
        {
            StartDownloadRecentMessages();
            return readOnlyMessages;
        }

        private void StartDownloadRecentMessages()
        {
            Task.Run(downloadRecentUC.ExecuteAsync);
        }

        public void Dispose() => messageSubscription?.Dispose();
    }
}