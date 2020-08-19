using System.Text;
using System.Threading.Tasks;

namespace Demo.API.Common.Caching
{
    internal class DistributedCacheSerializer
    {
        public static byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            string json = JsonService.SerializeObject(obj);

            return Encoding.UTF8.GetBytes(json);
        }

        public static async Task<T> Deserialize<T>(byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return null;
            }

            string json = Encoding.UTF8.GetString(byteArray);

            return await Task.Factory.StartNew(() => JsonService.DeserializeObject<T>(json)).ConfigureAwait(false);
        }
    }
}
