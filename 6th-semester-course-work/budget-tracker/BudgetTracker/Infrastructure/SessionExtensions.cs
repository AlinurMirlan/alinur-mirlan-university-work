using System.Text.Json;

namespace BudgetTracker.Infrastructure
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T item)
        {
            string jsonItem = JsonSerializer.Serialize(item);
            session.SetString(key, jsonItem);
        }

        public static T? Get<T>(this ISession session, string key)
        {
            string? jsonItem = session.GetString(key);
            if (jsonItem is null)
                return default;

            T? item = JsonSerializer.Deserialize<T>(jsonItem);
            if (item is null)
                return default;

            return item;
        }
    }
}
