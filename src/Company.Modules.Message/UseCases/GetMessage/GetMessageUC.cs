using System;
using System.Linq;
using System.Collections.ObjectModel;
using Realms;
using Company.Modules.Message.Database;
using System.Threading.Tasks;

namespace Company.Modules.Message.UseCases
{
    public class GetRecentMessagesUC : IGetRecentMessagesUC, IDisposable
    {
        const string Tag = nameof(GetRecentMessagesUC);
        readonly IDownloadNewMessagesUC downloadRecentUC;
        IDisposable messageSubscription;
        ObservableCollection<Database.Message> messages;

        public GetRecentMessagesUC(IDownloadNewMessagesUC downloadRecentUC)
        {
            this.downloadRecentUC = downloadRecentUC;
        }
            
        public ObservableCollection<Database.Message> Execute() 
        {
            Task.Run(downloadRecentUC.ExecuteAsync);

            Console.WriteLine($"[{Tag}] Called {nameof(Execute)}");

             if (messages == null)
            {
                messages = new ObservableCollection<Database.Message>();
                messageSubscription = Realm.GetInstance()
                                           .All<Database.Message>()
                                           .OrderByDescending(m => m.SendAt)
                                           .SubscribeForNotifications((IRealmCollection<Database.Message> sender, ChangeSet changes, Exception error) =>
                {
                    var isFirstResponse = changes == null;
                    if (isFirstResponse)
                    {
                        foreach (var message in sender)
                        {
                            messages.Add(message);
                        }
                    }
                    else if (changes != null)
                    {
                        foreach (var i in changes.InsertedIndices)
                        {
                            messages.Insert(i, sender[i]);
                        }
                        foreach (var i in changes.ModifiedIndices)
                        {
                            messages[i] = sender[i];
                        }
                    }
                });

            }
            return messages;
        }

        public void Dispose()
        {
            messageSubscription?.Dispose();
        }
    }
}