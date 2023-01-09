using System.Text.Json;

namespace Flash.Infrastructure
{
    public static class SessionExtensions
    {
        public static T? Get<T>(this ISession session, string key)
        {
            string? json = session.GetString(key);
            T? value = json is null ? default : JsonSerializer.Deserialize<T>(json);
            return value;
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
    }
}
