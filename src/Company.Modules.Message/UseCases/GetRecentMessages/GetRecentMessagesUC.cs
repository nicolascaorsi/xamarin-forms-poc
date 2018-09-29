using System;
using Realms;

namespace Company.Modules.Message.UseCases
{
    public class GetMessageUC : IGetMessageUC, IDisposable
    {
        readonly Realm realm = Realm.GetInstance();

        public Database.Message Execute(string primaryKey)
        {
            var message = realm.Find<Database.Message>(primaryKey);
            MarkAsRead(message);
            return message;
        }

        public void Dispose() => realm?.Dispose();

        void MarkAsRead(Database.Message message)
        {
            if (!message.ReadAt.HasValue)
            {
                realm.Write(() => message.ReadAt = DateTimeOffset.UtcNow);
            }
        }
    }
}