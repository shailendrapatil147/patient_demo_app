using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.API.Common
{
    public class JsonService
    {
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static object DeserializeObject(string value)
        {
            return JsonConvert.DeserializeObject(value);
        }

        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static string SerializeObject<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
