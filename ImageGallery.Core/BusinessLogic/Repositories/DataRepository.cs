using System.Collections.Generic;

namespace ImageGallery.Core.BusinessLogic.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly Dictionary<DataType, object> _data = new Dictionary<DataType, object>();

        public T Get<T>(DataType key)
        {
            if (_data.ContainsKey(key))
            {
                return (T)_data[key];
            }

            return default(T);
        }

        public void Set<T>(DataType key, T value)
        {
            if (_data.ContainsKey(key))
            {
                _data[key] = value;
            }
            else
            {
                _data.Add(key, value);
            }
        }

        public void Delete(DataType key)
        {
            _data.Remove(key);
        }

        public bool InStock(DataType key)
        {
            return _data.ContainsKey(key);
        }
    }
}
