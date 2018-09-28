using System;
using System.Linq;
using System.Collections.ObjectModel;
using Realms;
using Company.Modules.Message.Database;
using System.Threading.Tasks;

namespace Company.Modules.Message.UseCases
{
    public class GetMessageUC : IGetMessageUC
    {
        const string Tag = nameof(GetMessageUC);

        public GetMessageUC()
        {
        }

        public Database.Message Execute(string primaryKey)
        {
            Console.WriteLine($"[{Tag}] Called {nameof(Execute)}");

            var realm = Realm.GetInstance();
            var message = realm.Find<Database.Message>(primaryKey);
            if (!message.ReadAt.HasValue)
            {
                realm.Write(() => message.ReadAt = DateTimeOffset.UtcNow);
            }
            return message;
        }
    }
}