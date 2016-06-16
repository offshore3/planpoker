namespace Shinetech.PlanPoker.Data.Common
{
    public interface ICacheManager
    {
        void Add(string key, object value);
        void Remove(string key);
        T Get<T>(string key);
        bool KeyExist(string key);
    }
}