using System;
using System.Collections.Generic;
using Realms;

namespace Company.Modules.Message.Database
{
    public class Message : RealmObject
    {
        [PrimaryKey]
        [Required]
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTimeOffset SendAt { get; set; }
        public DateTimeOffset? ReadAt { get; set; }
        public bool IsFavorite { get; set; }
        public User From { get; set; }
        public IList<User> To { get; }
    }
}
