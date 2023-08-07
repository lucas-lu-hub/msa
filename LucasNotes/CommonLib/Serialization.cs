using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DynamicModel.Core.Common.Extensions
{
    public static class Serialization
    {
        public static string ToJsonString(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            //BinaryFormatter binaryFormatter = new BinaryFormatter();
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    binaryFormatter.Serialize(memoryStream, obj);
            //    return memoryStream.ToArray();
            //}
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public static T ToObject<T>(this string str) where T : class
        {
            if (str == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
