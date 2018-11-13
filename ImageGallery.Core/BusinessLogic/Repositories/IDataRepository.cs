namespace ImageGallery.Core.BusinessLogic.Repositories
{
    public interface IDataRepository
    {
        T Get<T>(DataType key);
        void Set<T>(DataType key, T value);
        void Delete(DataType key);
        bool InStock(DataType key);
    }
}
