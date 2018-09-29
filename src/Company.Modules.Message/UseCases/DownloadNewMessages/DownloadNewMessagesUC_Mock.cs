using System;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Realms;
using Refit;
using Company.Modules.Message.Database;

namespace Company.Modules.Message.UseCases
{        
    public class DownloadNewMessagesUC_Mock : IDownloadNewMessagesUC
    {
        const string Tag = nameof(DownloadNewMessagesUC_Mock);

        public async Task ExecuteAsync()
        {
            try
            {
                await Task.Run(async () =>
                {
                    // Fakes IO delay
                    await Task.Delay(new Faker().Random.Int(1000, 3000));
                    await Realm.GetInstance().WriteAsync((r) =>
                    {
                        try
                        {
                            var userFaker = new Faker<User>()
                                .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                                .RuleFor(u => u.Name, f => f.Name.FullName())
                                .RuleFor(u => u.Avatar, f => f.Internet.Avatar());

                            var messageFaker = new Faker<Database.Message>()
                                .RuleFor(m => m.Id, f => Guid.NewGuid().ToString())
                                .RuleFor(m => m.Subject, f => f.Lorem.Text())
                                .RuleFor(m => m.Body, f => f.Lorem.Paragraphs())
                                //.RuleFor(m => m.ReadAt, f => f.Random.Bool() ? new DateTimeOffset(f.Date.Past()) : (DateTimeOffset?)null)
                                .RuleFor(m => m.SendAt, f => new DateTimeOffset(DateTime.Now.AddMinutes(-1)))
                                .RuleFor(m => m.IsFavorite, f => f.Random.Bool())
                                .RuleFor(m => m.From, (m) => userFaker.Generate());
                            for (int i = 0; i < new Faker().Random.Int(1, 30); i++)
                            {
                                var message = messageFaker.Generate();
                                userFaker.Generate(new Faker().Database.Random.Int(1, 20)).ForEach(message.To.Add);
                                r.Add(message);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}