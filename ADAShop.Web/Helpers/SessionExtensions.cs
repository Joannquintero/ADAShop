using Newtonsoft.Json;

namespace ADAShop.Web.Helpers
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, object value)
        {
            session.SetString(key, ConvertJson.SerializeObjectJson<T>(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            string? value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value)!;
        }
    }
}