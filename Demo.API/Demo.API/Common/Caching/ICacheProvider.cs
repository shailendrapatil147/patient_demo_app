using System;
using System.Threading.Tasks;

namespace Demo.API.Common.Caching
{
    public interface ICacheProvider
    {
        Task<T> GetData<T>(string path, string identifier, Func<Task<T>> actionToFetchFrom) where T : class;

        void Remove(string path, string identifier);
    }
}
