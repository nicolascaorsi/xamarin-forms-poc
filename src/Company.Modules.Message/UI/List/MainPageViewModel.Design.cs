using System;
using System.Collections.ObjectModel;
using Bogus;
using Company.Modules.Message.Database;

namespace Company.Modules.Message.UI.ViewModels
{
    public class MainPageViewModel_Design : IListPageViewModel
    {

        public static IListPageViewModel Instance => new MainPageViewModel_Design();

        public MainPageViewModel_Design()
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Avatar, f => f.Internet.Avatar());

            var messageFaker = new Faker<Database.Message>()
                .RuleFor(m => m.Subject, f => f.Lorem.Text())
                .RuleFor(m => m.Body, f => f.Lorem.Paragraphs())
                .RuleFor(m => m.ReadAt, f => f.Random.Bool() ? new DateTimeOffset(f.Date.Past()) : (DateTimeOffset?) null)
                .RuleFor(m => m.SendAt, f => new DateTimeOffset(f.Date.Past()))
                .RuleFor(m => m.IsFavorite, f => f.Random.Bool())
                .RuleFor(m => m.From, (m) => userFaker.Generate());

            var messages = messageFaker.Generate(20);
            foreach (var message in messages)
            {
                userFaker.Generate(new Faker().Database.Random.Int(1, 20)).ForEach(message.To.Add);
            }

            Messages = new ObservableCollection<Database.Message>(messages);
        }

        public string Title { get; set; } = "Conversations";

        public ObservableCollection<Database.Message> Messages { get; set; }

        public Database.Message SelectedItem => throw new NotImplementedException();
    }
}