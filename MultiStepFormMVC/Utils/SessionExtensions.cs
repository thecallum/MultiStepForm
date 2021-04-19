using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MultiStepFormMVC.Utils
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            string output = JsonConvert.SerializeObject(value);

            session.SetString(key, output);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value == null) return default;

            var output = JsonConvert.DeserializeObject<T>(value);

            return output;

        }
    }


}