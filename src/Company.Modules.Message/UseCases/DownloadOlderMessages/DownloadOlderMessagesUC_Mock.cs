using System;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Realms;
using Company.Modules.Message.Database;

namespace Company.Modules.Message.UseCases
{
    public class DownloadOlderMessagesUC_Mock : IDownloadOlderMessagesUC
    {
        const string Tag = nameof(DownloadOlderMessagesUC_Mock);

        public async Task ExecuteAsync()
        {
            Console.WriteLine($"[{Tag}] Called {nameof(ExecuteAsync)}");

            try
            {
                await Realm.GetInstance().WriteAsync((r) =>
                {
                    try
                    {
                        var oldestMessage = r.All<Database.Message>().OrderByDescending(m => m.SendAt).LastOrDefault();

                        var userFaker = new Faker<User>()
                            .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                            .RuleFor(u => u.Name, f => f.Name.FullName())
                            .RuleFor(u => u.Avatar, f => f.Internet.Avatar());

                        var messageFaker = new Faker<Database.Message>()
                            .RuleFor(m => m.Id, f => Guid.NewGuid().ToString())
                            .RuleFor(m => m.Subject, f => f.Lorem.Text())
                            .RuleFor(m => m.Body, f => f.Lorem.Paragraphs())
                            .RuleFor(m => m.ReadAt, f => f.Random.Bool() ? new DateTimeOffset(f.Date.Past()) : (DateTimeOffset?)null)
                            .RuleFor(m => m.SendAt, f => new DateTimeOffset(f.Date.Past(refDate: oldestMessage != null ? oldestMessage.SendAt.Date : DateTime.Now)))
                            .RuleFor(m => m.IsFavorite, f => f.Random.Bool())
                            .RuleFor(m => m.From, (m) => userFaker.Generate());

                        var messages = messageFaker.Generate(new Faker().Random.Int(1,20));
                        foreach (var message in messages)
                        {
                            userFaker.Generate(new Faker().Random.Int(1, 20)).ForEach(message.To.Add);
                            r.Add(message);
                        }
                        Console.WriteLine($"[{Tag}] Called {nameof(ExecuteAsync)} inserted all");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine($"[{Tag}] Called {nameof(ExecuteAsync)} finished");
        }
    }
}