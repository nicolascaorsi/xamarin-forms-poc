using System;
using System.Collections.ObjectModel;
using System.Linq;
using Company.Modules.Message.Database;
using Realms;
using Xamarin.Forms.Internals;

namespace Realms
{
    public static class CollectionNotificationsExtensions
    {
        public static IDisposable SubscribeForNotificationsOn<T>(this IQueryable<T> results, ObservableCollection<T> cachedMessages) where T : RealmObject
        {
            return results.SubscribeForNotifications((sender, changes, error) => HandleNotificationCallbackDelegate(cachedMessages, results, sender, changes, error));
        }

        private static void HandleNotificationCallbackDelegate<T>(ObservableCollection<T> messages, IQueryable<T> results, IRealmCollection<T> realmMessages, ChangeSet changes, Exception error) where T : RealmObject
        {
            var isFirstResponse = changes == null;
            if (isFirstResponse)
            {
                realmMessages.ForEach(messages.Add);
            }
            else
            {
                changes?.InsertedIndices.ForEach(i => messages.Insert(i, realmMessages[i]));
                changes?.ModifiedIndices.ForEach(i => messages[i] = realmMessages[i]);
            }
        }

    }
}