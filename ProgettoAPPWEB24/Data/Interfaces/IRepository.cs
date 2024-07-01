namespace ProgettoAPPWEB24.Data.Interfaces
{
    public interface IRepository<T>
        where T : class, IHasId
    {
        Task Add(T entity);
        Task Delete(int id);
        IAsyncEnumerable<T> GetAll();
        Task<T?> Get(int id);
    }
}
