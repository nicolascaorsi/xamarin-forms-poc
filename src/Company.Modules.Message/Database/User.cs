using System;
using Realms;

namespace Company.Modules.Message.Database
{

    public class User : RealmObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }

        public User() { }

        public User(string id, string name, string avatar)
        {
            Id = id;
            Name = name;
            Avatar = avatar;
        }
    }
}