namespace Hotels.Repository.Interfaces
{
    public interface IUpdateable<T> where T : class
    {
        Task Update(T entity);
    }
}
