using Newtonsoft.Json;

namespace eCommerce.Helper.ExtensionMethod
{
    public static class JsonExtension
    {
        public static string ToJson<TSource>(this TSource data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        public static TResult FromJson<TResult>(this string json)
        {
            return JsonConvert.DeserializeObject<TResult>(json);
        }
    }
}